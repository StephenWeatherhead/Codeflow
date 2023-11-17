using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Activities
{


    public interface IInvokeMethodBuilder
    {
        IInvokeMethodBuilder DisplayName(string p_Name);
        IInvokeMethodBuilder In<TValue>(TValue p_Value);
        IInvokeMethodBuilder In<TValue>(Expression<Func<ActivityContext, TValue>> p_Value);
        IInvokeMethodBuilder In<TValue>(Activity<TValue> p_Value);
        IInvokeMethodBuilder In<TValue>(DelegateArgument p_Value);
        IInvokeMethodBuilder In<TValue>(Variable p_Value);
    }

    internal class InvokeMethodBuilder : IInvokeMethodBuilder, ICfActivityWrapper
    {
        public InvokeMethodBuilder(InvokeMethod p_Activity)
        {
            m_Activity = p_Activity;
        }
        private InvokeMethod m_Activity;

        public IInvokeMethodBuilder DisplayName(string p_Name)
        {
            m_Activity.DisplayName = p_Name;
            return this;
        }

        public IInvokeMethodBuilder In<TValue>(TValue p_Value)
        {
            m_Activity.Parameters.Add(new InArgument<TValue>(p_Value));
            return this;
        }

        public IInvokeMethodBuilder In<TValue>(Expression<Func<ActivityContext, TValue>> p_Value)
        {
            m_Activity.Parameters.Add(new InArgument<TValue>(p_Value));
            return this;
        }

        public IInvokeMethodBuilder In<TValue>(Activity<TValue> p_Value)
        {
            m_Activity.Parameters.Add(new InArgument<TValue>(p_Value));
            return this;
        }

        public IInvokeMethodBuilder In<TValue>(DelegateArgument p_Value)
        {
            m_Activity.Parameters.Add(new InArgument<TValue>(p_Value));
            return this;
        }

        public IInvokeMethodBuilder In<TValue>(Variable p_Value)
        {
            m_Activity.Parameters.Add(new InArgument<TValue>(p_Value));
            return this;
        }

        public Activity GetActivity()
        {
            return m_Activity;
        }
    }
}
