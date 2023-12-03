using System.Activities.Statements;
using System.Activities.Expressions;

namespace System.Activities
{
    public class WriteLineTests
    {
        [Fact]
        public void WriteLineWithLiteralArgument()
        {
            // arrange
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            l_Builder.WriteLine("Test");

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            WriteLine l_WriteLine = (WriteLine)l_Sequence.Activities.First();

            // assert
            Assert.Single(l_Sequence.Activities);
            Assert.NotNull(l_WriteLine.Text);
        }
        [Fact]
        public void WriteLineWithExpression()
        {
            // arrange
            Variable<string> l_MyVariable = new Variable<string>("MyVariable");
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            l_Builder.WriteLine(e => l_MyVariable.Get(e));

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            WriteLine l_WriteLine = (WriteLine)l_Sequence.Activities.First();

            // assert
            Assert.Single(l_Sequence.Activities);
            Assert.NotNull(l_WriteLine.Text);
        }
        [Fact]
        public void WriteLineWithStringActivity()
        {
            // arrange
            Literal<string> l_String = new Literal<string>("Hello world");
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            l_Builder.WriteLine(l_String);

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            WriteLine l_WriteLine = (WriteLine)l_Sequence.Activities.First();

            // assert
            Assert.Single(l_Sequence.Activities);
            Assert.NotNull(l_WriteLine.Text);
        }
        [Fact]
        public void WriteLineWithDelegateArgument()
        {
            // arrange
            DelegateInArgument<string> l_String = new DelegateInArgument<string>("l_String");
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            l_Builder.WriteLine(l_String);

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            WriteLine l_WriteLine = (WriteLine)l_Sequence.Activities.First();

            // assert
            Assert.Single(l_Sequence.Activities);
            Assert.NotNull(l_WriteLine.Text);
        }
        [Fact]
        public void WriteLineWithVariable()
        {
            // arrange
            Variable<string> l_MyVariable = new Variable<string>("MyVariable");
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            l_Builder.WriteLine(l_MyVariable);

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            WriteLine l_WriteLine = (WriteLine)l_Sequence.Activities.First();

            // assert
            Assert.Single(l_Sequence.Activities);
            Assert.NotNull(l_WriteLine.Text);
        }
    }
}