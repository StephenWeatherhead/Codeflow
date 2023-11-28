using Microsoft.CSharp.Activities;
using Microsoft.VisualBasic.Activities;
using System;
using System.Activities;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class HelloWorldActivity : Activity
    {
        public HelloWorldActivity()
        {
            Implementation = () => new WriteLine
            {
                Text = new InArgument<string>("Hello world!")
            };
        }
    }

    public class HelloWorldCodeflow : Codeflow
    {
        public HelloWorldCodeflow()
        {
            MyActivityAction = new ActivityAction<string>();
            MyActivityAction.Argument = new DelegateInArgument<string>("Text");
            MyActivityAction.Handler = new WriteLine() { Text = MyActivityAction.Argument };
        }
        public InArgument<string> Name { get; set; }
        public ActivityAction<string> MyActivityAction { get; set; }
        protected override void Build(IWorkflowBuilder p_Builder)
        {
            var l_MyVar = p_Builder.Variable<int>("l_MyVar");
            var l_MyVarString = p_Builder.Variable<string>("l_MyVarString");

            p_Builder.Assign<int>(l_MyVar, new RandomInt())
                .DisplayName("Assign Random Integer");

            p_Builder.WriteLine(e => $"Hello {Name.Get(e)}! And MyVar is :");
            p_Builder.Delay(TimeSpan.FromSeconds(1));
            p_Builder.WriteLine("1");
            p_Builder.Delay(TimeSpan.FromSeconds(1));
            p_Builder.WriteLine("2");
            p_Builder.Delay(TimeSpan.FromSeconds(1));
            p_Builder.WriteLine("3");
            p_Builder.Delay(TimeSpan.FromSeconds(1));
            p_Builder.WriteLine("4");
            p_Builder.Delay(TimeSpan.FromSeconds(1));
            p_Builder.WriteLine("5");
            p_Builder.Delay(TimeSpan.FromMilliseconds(500));
            p_Builder.Assign(l_MyVarString, e => l_MyVar.Get(e).ToString());
            p_Builder.WriteLine(l_MyVarString);

            p_Builder.InvokeMethod("Test", nameof(string.ToUpper), In(e => CultureInfo.CurrentCulture))
                .Result<string>(l_MyVarString);

            p_Builder.InvokeMethod(typeof(Console), "WriteLine", In(l_MyVarString));
            p_Builder.InvokeMethod(e => (new MyTestObject()), "WriteMessage", In("Literal"));
            p_Builder.InvokeDelegate(MyActivityAction, Del("Argument", In("This is my Delegate Invoke")));
        }
    }

    public class MyTestObject
    {
        public void WriteMessage(string p_MyString)
        {
            Console.WriteLine($"Hello there! This is a message from MyTestObject. Your string is \"{p_MyString}\"");
        }
    }

    public class RandomInt : Codeflow<int>
    {
        protected override void Build(IWorkflowBuilder p_Builder)
        {
            p_Builder.Assign((env) => Result.Get(env), (env) => Random.Shared.Next());
        }
    }
}
