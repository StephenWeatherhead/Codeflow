using ReflectionMagic;
using System.Activities.Statements;

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
    }
}