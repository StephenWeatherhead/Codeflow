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
    }
}
