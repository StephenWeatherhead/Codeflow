﻿using Microsoft.CSharp.Activities;
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
        public InArgument<string> Name { get; set; }
        protected override void Build(IWorkflowBuilder p_Builder)
        {
            var l_MyVar = p_Builder.Variable<int>("l_MyVar");
            var l_MyVarString = p_Builder.Variable<string>("l_MyVarString");

            p_Builder.Assign<int>(l_MyVar, new RandomInt())
                .DisplayName("Assign Random Integer");

            p_Builder.WriteLine(env => $"Hello {Name.Get(env)}! And MyVar is :");

            p_Builder.Assign(l_MyVarString, (env) => l_MyVar.Get(env).ToString());
            p_Builder.WriteLine(l_MyVarString);

            p_Builder.InvokeMethod("Test", nameof(string.ToUpper), In((env) => CultureInfo.CurrentCulture))
                .Result<string>(l_MyVarString);

            p_Builder.InvokeMethod(typeof(Console), nameof(Console.WriteLine), In<string>(l_MyVarString));
            p_Builder.InvokeMethod(e => (new MyTestObject()), nameof(MyTestObject.WriteMessage));
        }
    }

    public class MyTestObject
    {
        public void WriteMessage()
        {
            Console.WriteLine("Hello there! This is a message from MyTestObject");
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
