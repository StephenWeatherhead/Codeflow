using Microsoft.PowerFx.Core.Public.Values;
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
        ICfActivityBuilder WriteLine(string p_Text);
        ICfActivityBuilder WriteLine(Expression<Func<ActivityContext, string>> p_Text);
        ICfActivityBuilder WriteLine(Activity<string> p_Text);
        ICfActivityBuilder WriteLine(DelegateArgument p_Text);
        ICfActivityBuilder WriteLine(Variable<string> p_Text);

        Variable<TVariable> Variable<TVariable>(string p_Name);
        Variable<TVariable> Variable<TVariable>(string p_Name, TVariable p_DefaultValue);
        Variable<TVariable> Variable<TVariable>(string p_Name, Expression<Func<ActivityContext, TVariable>> p_DefaultValue);

        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, TValue p_Value);
        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Activity<TValue> p_Value);
        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, DelegateArgument p_Value);
        ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Variable<TValue> p_Value);

        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, TValue p_Value);
        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Activity<TValue> p_Value);
        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, DelegateArgument p_Value);
        ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Variable<TValue> p_Value);

        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, TValue p_Value);
        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Activity<TValue> p_Value);
        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, DelegateArgument p_Value);
        ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Variable<TValue> p_Value);

        ICfActivityBuilder Assign<TValue>(Variable p_To, TValue p_Value);
        ICfActivityBuilder Assign<TValue>(Variable p_To, Expression<Func<ActivityContext, TValue>> p_Value);
        ICfActivityBuilder Assign<TValue>(Variable p_To, Activity<TValue> p_Value);
        ICfActivityBuilder Assign<TValue>(Variable p_To, DelegateArgument p_Value);
        ICfActivityBuilder Assign<TValue>(Variable p_To, Variable<TValue> p_Value);
        
        IInvokeMethodBuilder InvokeMethod<TTarget>(TTarget p_Target, string p_MethodName, params Argument[] p_Arguments);
        IInvokeMethodBuilder InvokeMethod<TTarget>(Expression<Func<ActivityContext, TTarget>> p_Target, string p_MethodName, params Argument[] p_Arguments);
        IInvokeMethodBuilder InvokeMethod<TTarget>(Activity<TTarget> p_Target, string p_MethodName, params Argument[] p_Arguments);
        IInvokeMethodBuilder InvokeMethod<TTarget>(DelegateArgument p_Target, string p_MethodName, params Argument[] p_Arguments);
        IInvokeMethodBuilder InvokeMethod<TTarget>(Variable<TTarget> p_Target, string p_MethodName, params Argument[] p_Arguments);

        /// <summary>
        /// Used for invoking members of a type (i.e. static methods)
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="p_MethodName"></param>
        /// <param name="p_Arguments"></param>
        /// <returns></returns>
        IInvokeMethodBuilder InvokeMethod(Type p_TargetType, string p_MethodName, params Argument[] p_Arguments);

        ICfActivityBuilder InvokeDelegate(ActivityDelegate p_Delegate, params CfDelegateArgument[] p_DelegateArguments);
    }

    public interface ICfActivityBuilder
    {
        ICfActivityBuilder DisplayName(string p_Name);
    }

    internal interface ICfActivityWrapper
    {
        public Activity GetActivity();
    }

    internal class SimpleActivityWrapper : ICfActivityWrapper, ICfActivityBuilder
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
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder WriteLine(Expression<Func<ActivityContext, string>> p_Text)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new WriteLine { Text = new InArgument<string>(p_Text) });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder WriteLine(Activity<string> p_Text)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new WriteLine { Text = new InArgument<string>(p_Text) });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder WriteLine(DelegateArgument p_Text)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new WriteLine { Text = new InArgument<string>(p_Text) });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder WriteLine(Variable<string> p_Text)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new WriteLine { Text = new InArgument<string>(p_Text) });
            return (ICfActivityBuilder)m_StepBuilder;
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
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Activity<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, DelegateArgument p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Activity<Location<TValue>> p_To, Variable<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, TValue p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Activity<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, DelegateArgument p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(DelegateArgument p_To, Variable<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, TValue p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Activity<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, DelegateArgument p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Expression<Func<ActivityContext, TValue>> p_To, Variable<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, TValue p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, Expression<Func<ActivityContext, TValue>> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, Activity<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, DelegateArgument p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder Assign<TValue>(Variable p_To, Variable<TValue> p_Value)
        {
            FinishLastStep();
            m_StepBuilder = new SimpleActivityWrapper(new Assign
            {
                To = new OutArgument<TValue>(p_To),
                Value = new InArgument<TValue>(p_Value)
            });
            return (ICfActivityBuilder)m_StepBuilder;
        }

        public IInvokeMethodBuilder InvokeMethod<TTarget>(TTarget p_Target, string p_MethodName, params Argument[] p_Arguments)
        {
            FinishLastStep();
            var l_Activity = new InvokeMethod
            {
                TargetObject = new InArgument<TTarget>(p_Target),
                MethodName = p_MethodName
            };
            l_Activity.Parameters.AddRange(p_Arguments);
            m_StepBuilder = new InvokeMethodBuilder(l_Activity);
            return (IInvokeMethodBuilder)m_StepBuilder;
        }

        public IInvokeMethodBuilder InvokeMethod(Type p_TargetType, string p_MethodName, params Argument[] p_Arguments)
        {
            FinishLastStep();
            var l_Activity = new InvokeMethod
            {
                TargetType = p_TargetType,
                MethodName = p_MethodName
            };
            l_Activity.Parameters.AddRange(p_Arguments);
            m_StepBuilder = new InvokeMethodBuilder(l_Activity);
            return (IInvokeMethodBuilder)m_StepBuilder;
        }

        public IInvokeMethodBuilder InvokeMethod<TTarget>(Expression<Func<ActivityContext, TTarget>> p_Target, string p_MethodName, params Argument[] p_Arguments)
        {
            FinishLastStep();
            var l_Activity = new InvokeMethod
            {
                TargetObject = new InArgument<TTarget>(p_Target),
                MethodName = p_MethodName
            };
            l_Activity.Parameters.AddRange(p_Arguments);
            m_StepBuilder = new InvokeMethodBuilder(l_Activity);
            return (IInvokeMethodBuilder)m_StepBuilder;
        }

        public IInvokeMethodBuilder InvokeMethod<TTarget>(Activity<TTarget> p_Target, string p_MethodName, params Argument[] p_Arguments)
        {
            FinishLastStep();
            var l_Activity = new InvokeMethod
            {
                TargetObject = new InArgument<TTarget>(p_Target),
                MethodName = p_MethodName
            };
            l_Activity.Parameters.AddRange(p_Arguments);
            m_StepBuilder = new InvokeMethodBuilder(l_Activity);
            return (IInvokeMethodBuilder)m_StepBuilder;
        }

        public IInvokeMethodBuilder InvokeMethod<TTarget>(DelegateArgument p_Target, string p_MethodName, params Argument[] p_Arguments)
        {
            FinishLastStep();
            var l_Activity = new InvokeMethod
            {
                TargetObject = new InArgument<TTarget>(p_Target),
                MethodName = p_MethodName
            };
            l_Activity.Parameters.AddRange(p_Arguments);
            m_StepBuilder = new InvokeMethodBuilder(l_Activity);
            return (IInvokeMethodBuilder)m_StepBuilder;
        }

        public IInvokeMethodBuilder InvokeMethod<TTarget>(Variable<TTarget> p_Target, string p_MethodName, params Argument[] p_Arguments)
        {
            FinishLastStep();
            var l_Activity = new InvokeMethod
            {
                TargetObject = new InArgument<TTarget>(p_Target),
                MethodName = p_MethodName
            };
            l_Activity.Parameters.AddRange(p_Arguments);
            m_StepBuilder = new InvokeMethodBuilder(l_Activity);
            return (IInvokeMethodBuilder)m_StepBuilder;
        }

        public ICfActivityBuilder InvokeDelegate(ActivityDelegate p_Delegate, params CfDelegateArgument[] p_DelegateArguments)
        {
            FinishLastStep();
            InvokeDelegate l_InvokeDelegate = new InvokeDelegate();
            l_InvokeDelegate.Delegate = p_Delegate;
            foreach (var l_Argument in p_DelegateArguments)
            {
                l_InvokeDelegate.DelegateArguments.Add(l_Argument.Name, l_Argument.Argument);
            }
            m_StepBuilder = new SimpleActivityWrapper(l_InvokeDelegate);
            return (ICfActivityBuilder)m_StepBuilder;
        }
    }
}
