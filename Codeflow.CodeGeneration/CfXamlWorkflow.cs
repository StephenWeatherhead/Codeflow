using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    public class CfXamlWorkflow
    {
        public CfXamlWorkflow()
        {
            Arguments = new List<CfArgument>();
        }
        public string? Class { get; set; }
        public List<CfArgument> Arguments { get; set; }
        public CfXamlObject? Content { get; set; }
    }
}
