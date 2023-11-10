using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Activities
{
    public interface IWorkflowBuilder
    {
        IActivityBuilder<TActivity> Root<TActivity>();
    }

    internal class WorkflowBuilder : IWorkflowBuilder
    {
        public Activity GetActivity()
        {
            if(m_ActivityBuilder == null)
            {
                throw new InvalidOperationException("Root has not been set.");
            }
            return m_ActivityBuilder.GetActivity();
        }

        private ActivityBuilder? m_ActivityBuilder;

        public IActivityBuilder<TActivity> Root<TActivity>()
        {
            if(m_ActivityBuilder == null)
            {
                m_ActivityBuilder = new ActivityBuilder<TActivity>();
            }
            return (IActivityBuilder<TActivity>)m_ActivityBuilder;
        }
    }
}
