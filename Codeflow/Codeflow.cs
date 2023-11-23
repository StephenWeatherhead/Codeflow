using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Activities
{
    
    public abstract class Codeflow : Activity
    {
        public Codeflow()
        {
            Implementation = GetImplementation;
        }
        private Activity GetImplementation()
        {
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            Build(l_Builder);
            return l_Builder.GetActivity();
        }
        protected abstract void Build(IWorkflowBuilder p_Builder);

        protected InArgument<TValue> Lt<TValue>(TValue p_Literal)
        {
            return new InArgument<TValue>(p_Literal);
        }

        protected InArgument<TValue> In<TValue>(Expression<Func<ActivityContext, TValue>> p_Value)
        {
            return new InArgument<TValue>(p_Value);
        }

        protected InArgument<TValue> In<TValue>(Activity<TValue> p_Value)
        {
            return new InArgument<TValue>(p_Value);
        }

        protected InArgument<TValue> In<TValue>(DelegateArgument p_Value)
        {
            return new InArgument<TValue>(p_Value);
        }

        protected InArgument<TValue> In<TValue>(Variable p_Value)
        {
            return new InArgument<TValue>(p_Value);
        }

        protected OutArgument<TValue> Out<TValue>(Activity<Location<TValue>> p_Value)
        {
            return new OutArgument<TValue>(p_Value);
        }

        protected OutArgument<TValue> Out<TValue>(DelegateArgument p_Value)
        {
            return new OutArgument<TValue>(p_Value);
        }

        protected OutArgument<TValue> Out<TValue>(Expression<Func<ActivityContext, TValue>> p_Value)
        {
            return new OutArgument<TValue>(p_Value);
        }

        protected OutArgument<TValue> Out<TValue>(Variable p_Value)
        {
            return new OutArgument<TValue>(p_Value);
        }
    }

    public abstract class Codeflow<T> : Activity<T>
    {
        public Codeflow()
        {
            Implementation = GetImplementation;
        }
        private Activity GetImplementation()
        {
            WorkflowBuilder l_Builder = new WorkflowBuilder();
            Build(l_Builder);
            return l_Builder.GetActivity();
        }
        protected abstract void Build(IWorkflowBuilder p_Builder);

        protected InArgument<TValue> Lt<TValue>(TValue p_Literal)
        {
            return new InArgument<TValue>(p_Literal);
        }

        protected InArgument<TValue> In<TValue>(Expression<Func<ActivityContext, TValue>> p_Value)
        {
            return new InArgument<TValue>(p_Value);
        }

        protected InArgument<TValue> In<TValue>(Activity<TValue> p_Value)
        {
            return new InArgument<TValue>(p_Value);
        }

        protected InArgument<TValue> In<TValue>(DelegateArgument p_Value)
        {
            return new InArgument<TValue>(p_Value);
        }

        protected InArgument<TValue> In<TValue>(Variable p_Value)
        {
            return new InArgument<TValue>(p_Value);
        }

        protected OutArgument<TValue> Out<TValue>(Activity<Location<TValue>> p_Value)
        {
            return new OutArgument<TValue>(p_Value);
        }

        protected OutArgument<TValue> Out<TValue>(DelegateArgument p_Value)
        {
            return new OutArgument<TValue>(p_Value);
        }

        protected OutArgument<TValue> Out<TValue>(Expression<Func<ActivityContext, TValue>> p_Value)
        {
            return new OutArgument<TValue>(p_Value);
        }

        protected OutArgument<TValue> Out<TValue>(Variable p_Value)
        {
            return new OutArgument<TValue>(p_Value);
        }
    }
}
