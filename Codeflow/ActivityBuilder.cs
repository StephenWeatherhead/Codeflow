using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Activities
{
    public interface IActivityBuilder<TActivity>
    {
        IActivityBuilder<TActivity> In<TArgument>(Expression<Func<TActivity, InArgument<TArgument>>> p_Argument, TArgument p_Value);
    }

    internal class ActivityBuilder<TActivity> : ActivityBuilder, IActivityBuilder<TActivity>
    {
        public ActivityBuilder() : base(typeof(TActivity)) { }
        public IActivityBuilder<TActivity> In<TArgument>(Expression<Func<TActivity, InArgument<TArgument>>> p_Argument, TArgument p_Value)
        {
            return this;
        }
    }
    internal class ActivityBuilder
    {
        public ActivityBuilder(Type p_Type)
        {
            m_Type = p_Type;
        }
        private Type m_Type;
        private List<object> m_Arguments;
        public Activity GetActivity()
        {
            var l_Instance = Activator.CreateInstance(m_Type);
            
            
            return (Activity)l_Instance;
        }
    }
}
