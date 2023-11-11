using System;
using System.Activities.Expressions;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Activities
{
    public interface IWorkflowBuilder
    {
        void WriteLine(string p_Text);
        void WriteLine(Expression<Func<ActivityContext, string>> p_Text);
        void WriteLine(Activity<string> p_Text);
        void WriteLine(DelegateArgument p_Text);
        void WriteLine(Variable p_Text);

        Variable<TVariable> Variable<TVariable>(string p_Name);
        Variable<TVariable> Variable<TVariable>(string p_Name, TVariable p_DefaultValue);
        Variable<TVariable> Variable<TVariable>(string p_Name, Expression<Func<ActivityContext, TVariable>> p_DefaultValue);

        void Assign<TValue>(Activity<Location<TValue>> p_To, TValue p_Value);
        void Assign<TValue>(Activity<Location<TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        void Assign<TValue>(Activity<Location<TValue>> p_To, Activity<TValue> p_Value);
        void Assign<TValue>(Activity<Location<TValue>> p_To, DelegateArgument p_Value);
        void Assign<TValue>(Activity<Location<TValue>> p_To, Variable p_Value);

        void Assign<TValue>(DelegateArgument p_To, TValue p_Value);
        void Assign<TValue>(DelegateArgument p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        void Assign<TValue>(DelegateArgument p_To, Activity<TValue> p_Value);
        void Assign<TValue>(DelegateArgument p_To, DelegateArgument p_Value);
        void Assign<TValue>(DelegateArgument p_To, Variable p_Value);

        void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, TValue p_Value);
        void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Activity<TValue> p_Value);
        void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, DelegateArgument p_Value);
        void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Variable p_Value);

        void Assign<TValue>(Variable p_To, TValue p_Value);
        void Assign<TValue>(Variable p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        void Assign<TValue>(Variable p_To, Activity<TValue> p_Value);
        void Assign<TValue>(Variable p_To, DelegateArgument p_Value);
        void Assign<TValue>(Variable p_To, Variable p_Value);
    }

    internal class WorkflowBuilder : IWorkflowBuilder
    {
        public WorkflowBuilder()
        {
            m_Activity = new Sequence();
        }
        private Sequence m_Activity;
        public Activity GetActivity()
        {
            return m_Activity;
        }

        public void WriteLine(string p_Text)
        {
            m_Activity.Activities.Add(new WriteLine { Text = new InArgument<string>(p_Text) });
        }

        public void WriteLine(Expression<Func<ActivityContext, string>> p_Text)
        {
            m_Activity.Activities.Add(new WriteLine { Text = new InArgument<string>(p_Text) });
        }

        public void WriteLine(Activity<string> p_Text)
        {
            m_Activity.Activities.Add(new WriteLine { Text = new InArgument<string>(p_Text) });
        }

        public void WriteLine(DelegateArgument p_Text)
        {
            m_Activity.Activities.Add(new WriteLine { Text = new InArgument<string>(p_Text) });
        }

        public void WriteLine(Variable p_Text)
        {
            m_Activity.Activities.Add(new WriteLine { Text = new InArgument<string>(p_Text) });
        }

        public Variable<TVariable> Variable<TVariable>(string p_Name)
        {
            var l_Variable = new Variable<TVariable>(p_Name);
            m_Activity.Variables.Add(l_Variable);
            return l_Variable;
        }

        public Variable<TVariable> Variable<TVariable>(string p_Name, TVariable p_DefaultValue)
        {
            var l_Variable = new Variable<TVariable>(p_Name, p_DefaultValue);
            m_Activity.Variables.Add(l_Variable);
            return l_Variable;
        }

        public Variable<TVariable> Variable<TVariable>(string p_Name, Expression<Func<ActivityContext, TVariable>> p_DefaultValue)
        {
            var l_Variable = new Variable<TVariable>(p_Name, p_DefaultValue);
            m_Activity.Variables.Add(l_Variable);
            return l_Variable;
        }

        public void Assign<TValue>(Activity<Location<TValue>> p_To, TValue p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Activity<Location<TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            m_Activity.Activities.Add(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Activity<Location<TValue>> p_To, Activity<TValue> p_Value)
        {
            m_Activity.Activities.Add(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Activity<Location<TValue>> p_To, DelegateArgument p_Value)
        {
            m_Activity.Activities.Add(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Activity<Location<TValue>> p_To, Variable p_Value)
        {
            m_Activity.Activities.Add(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(DelegateArgument p_To, TValue p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(DelegateArgument p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(DelegateArgument p_To, Activity<TValue> p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(DelegateArgument p_To, DelegateArgument p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(DelegateArgument p_To, Variable p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, TValue p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Activity<TValue> p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, DelegateArgument p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Variable p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Variable p_To, TValue p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Variable p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Variable p_To, Activity<TValue> p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Variable p_To, DelegateArgument p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }

        public void Assign<TValue>(Variable p_To, Variable p_Value)
        {
            m_Activity.Activities.Add(new Assign 
            { 
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
        }
    }
}
