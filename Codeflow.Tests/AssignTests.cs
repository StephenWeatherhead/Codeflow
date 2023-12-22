using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Activities
{
    public class AssignTests
    {
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
    }
}
