using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Activities
{
    public class AssignTests
    {
        [Fact]
        public void AssignVariableWithValue()
        {
            // arrange
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            Variable l_Variable = l_Builder.Variable<string>("Test");
            l_Builder.Assign<string>(l_Variable, "Test");

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            Assign l_Assign = (Assign)l_Sequence.Activities.First();

            // assert
            Assert.NotNull(l_Assign.To);
            Assert.NotNull(l_Assign.Value);
        }

        [Fact]
        public void AssignVariableWithExpression()
        {
            // arrange
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            Variable l_Variable = l_Builder.Variable<int>("Test");
            l_Builder.Assign<int>(l_Variable, e => Random.Shared.Next());

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            Assign l_Assign = (Assign)l_Sequence.Activities.First();

            // assert
            Assert.NotNull(l_Assign.To);
            Assert.NotNull(l_Assign.Value);
        }

        [Fact]
        public void AssignVariableWithActivity()
        {
            // arrange
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            Variable l_Variable = l_Builder.Variable<int>("Test");
            l_Builder.Assign<int>(l_Variable, new Literal<int>(123));

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            Assign l_Assign = (Assign)l_Sequence.Activities.First();

            // assert
            Assert.NotNull(l_Assign.To);
            Assert.NotNull(l_Assign.Value);
        }

        [Fact]
        public void AssignVariableWithDelegateArgument()
        {
            // arrange
            DelegateInArgument<string> l_String = new DelegateInArgument<string>("l_String");
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            Variable l_Variable = l_Builder.Variable<string>("Test");
            l_Builder.Assign<string>(l_Variable, l_String);

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            Assign l_Assign = (Assign)l_Sequence.Activities.First();

            // assert
            Assert.NotNull(l_Assign.To);
            Assert.NotNull(l_Assign.Value);
        }
    }
}
