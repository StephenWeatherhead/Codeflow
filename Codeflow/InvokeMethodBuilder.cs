using Microsoft.PowerFx.Core.Public.Values;
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

        IInvokeMethodBuilder Result<TResult>(Activity<Location<TResult>> p_Result);
        IInvokeMethodBuilder Result<TResult>(DelegateArgument p_Result);
        IInvokeMethodBuilder Result<TResult>(Expression<Func<ActivityContext, TResult>> p_Result);
        IInvokeMethodBuilder Result<TResult>(Variable p_Result);

        IInvokeMethodBuilder GenericTypeArguments(params Type[] p_TypeArguments);

        IInvokeMethodBuilder RunAsync();
        IInvokeMethodBuilder RunAsync(bool p_RunAsync);
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

        public Activity GetActivity()
        {
            return m_Activity;
        }

        public IInvokeMethodBuilder Result<TResult>(Activity<Location<TResult>> p_Result)
        {
            m_Activity.Result = new OutArgument<TResult>(p_Result);
            return this;
        }

        public IInvokeMethodBuilder Result<TResult>(DelegateArgument p_Result)
        {
            m_Activity.Result = new OutArgument<TResult>(p_Result);
            return this;
        }

        public IInvokeMethodBuilder Result<TResult>(Expression<Func<ActivityContext, TResult>> p_Result)
        {
            m_Activity.Result = new OutArgument<TResult>(p_Result);
            return this;
        }

        public IInvokeMethodBuilder Result<TResult>(Variable p_Result)
        {
            m_Activity.Result = new OutArgument<TResult>(p_Result);
            return this;
        }

        public IInvokeMethodBuilder GenericTypeArguments(params Type[] p_TypeArguments)
        {
            m_Activity.GenericTypeArguments.Clear();
            m_Activity.GenericTypeArguments.AddRange(p_TypeArguments);
            return this;
        }

        public IInvokeMethodBuilder RunAsync()
        {
            m_Activity.RunAsynchronously = true;
            return this;
        }

        public IInvokeMethodBuilder RunAsync(bool p_RunAsync)
        {
            m_Activity.RunAsynchronously = p_RunAsync;
            return this;
        }
    }
}
