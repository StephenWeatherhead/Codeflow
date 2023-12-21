using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Activities
{
    public class VariableTests
    {
        [Fact]
        public void VariableWithNoDefaultValue()
        {
            // arrange
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            l_Builder.Variable<string>("Test");

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            Variable l_Variable = l_Sequence.Variables[0];

            // assert
            Assert.Single(l_Sequence.Variables);
            Assert.Equal("Test", l_Variable.Name);
            Assert.Equal(typeof(string), l_Variable.Type);
        }

        [Fact]
        public void VariableWithDefaultValue()
        {
            // arrange
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            l_Builder.Variable<string>("Test", "hello");

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            Variable l_Variable = l_Sequence.Variables[0];
            string l_DefaultValue = (string)WorkflowInvoker.Invoke(l_Variable.Default)["Result"];

            // assert
            Assert.Single(l_Sequence.Variables);
            Assert.Equal("Test", l_Variable.Name);
            Assert.Equal("hello", l_DefaultValue);
            Assert.Equal(typeof(string), l_Variable.Type);
        }

        [Fact]
        public void VariableWithDefaultExpression()
        {
            // arrange
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            l_Builder.Variable<string>("Test", e => "Hello".ToUpper());

            // act
            Sequence l_Sequence = (Sequence)l_Builder.GetActivity();
            Variable l_Variable = l_Sequence.Variables[0];
            string l_DefaultValue = (string)WorkflowInvoker.Invoke(l_Variable.Default)["Result"];

            // assert
            Assert.Single(l_Sequence.Variables);
            Assert.Equal("Test", l_Variable.Name);
            Assert.Equal("HELLO", l_DefaultValue);
            Assert.Equal(typeof(string), l_Variable.Type);
        }
    }
}
