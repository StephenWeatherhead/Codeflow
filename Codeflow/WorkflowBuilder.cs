﻿using System;
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
        ICfActivityBuilder WriteLine(string p_Text);
        ICfActivityBuilder WriteLine(Expression<Func<ActivityContext, string>> p_Text);
        ICfActivityBuilder WriteLine(Activity<string> p_Text);
        ICfActivityBuilder WriteLine(DelegateArgument p_Text);
        ICfActivityBuilder WriteLine(Variable p_Text);

        Variable<TVariable> Variable<TVariable>(string p_Name);
        Variable<TVariable> Variable<TVariable>(string p_Name, TVariable p_DefaultValue);
        Variable<TVariable> Variable<TVariable>(string p_Name, Expression<Func<ActivityContext, TVariable>> p_DefaultValue);

        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, TValue p_Value);
        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Activity<TValue> p_Value);
        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, DelegateArgument p_Value);
        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Variable p_Value);

        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, TValue p_Value);
        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Activity<TValue> p_Value);
        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, DelegateArgument p_Value);
        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Variable p_Value);

        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, TValue p_Value);
        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Activity<TValue> p_Value);
        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, DelegateArgument p_Value);
        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Variable p_Value);

        ICfActivityBuilder Assign<TValue>(Variable p_To, TValue p_Value);
        ICfActivityBuilder Assign<TValue>(Variable p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        ICfActivityBuilder Assign<TValue>(Variable p_To, Activity<TValue> p_Value);
        ICfActivityBuilder Assign<TValue>(Variable p_To, DelegateArgument p_Value);
        ICfActivityBuilder Assign<TValue>(Variable p_To, Variable p_Value);
    }

    public interface ICfActivityBuilder
    {
        ICfActivityBuilder DisplayName(string p_Name);
    }

    internal interface ICfActivityWrapper : ICfActivityBuilder
    {
        public Activity GetActivity();
    }

    internal class SimpleActivityWrapper : ICfActivityWrapper
    {
        public SimpleActivityWrapper(Activity p_Activity)
        {
            m_Activity = p_Activity;
        }
        private Activity m_Activity;

        public Activity GetActivity()
        {
            return m_Activity;
        }

        public ICfActivityBuilder DisplayName(string p_Name)
        {
            m_Activity.DisplayName = p_Name;
            return this;
        }
    }

    internal class WorkflowBuilder : IWorkflowBuilder
    {
        public WorkflowBuilder()
        {
            m_Activity = new Sequence();
        }
        private Sequence m_Activity;

        private ICfActivityWrapper? m_StepBuilder;

        public Activity GetActivity()
        {
            FinishLastStep();
            return m_Activity;
        }

        private void FinishLastStep()
        {
            if(m_StepBuilder != null)
            {
                m_Activity.Activities.Add(m_StepBuilder.GetActivity());
                m_StepBuilder = null;
            }
        }

        public ICfActivityBuilder WriteLine(string p_Text)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new WriteLine { Text = new InArgument<string>(p_Text) });
            return m_StepBuilder;
        }

        public ICfActivityBuilder WriteLine(Expression<Func<ActivityContext, string>> p_Text)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new WriteLine { Text = new InArgument<string>(p_Text) });
            return m_StepBuilder;
        }

        public ICfActivityBuilder WriteLine(Activity<string> p_Text)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new WriteLine { Text = new InArgument<string>(p_Text) });
            return m_StepBuilder;
        }

        public ICfActivityBuilder WriteLine(DelegateArgument p_Text)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new WriteLine { Text = new InArgument<string>(p_Text) });
            return m_StepBuilder;
        }

        public ICfActivityBuilder WriteLine(Variable p_Text)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new WriteLine { Text = new InArgument<string>(p_Text) });
            return m_StepBuilder;
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

        public ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, TValue p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Activity<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, DelegateArgument p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Variable p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, TValue p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Activity<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, DelegateArgument p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Variable p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, TValue p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Activity<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, DelegateArgument p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Variable p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, TValue p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, Activity<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, DelegateArgument p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, Variable p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return m_StepBuilder;
        }
    }
}
