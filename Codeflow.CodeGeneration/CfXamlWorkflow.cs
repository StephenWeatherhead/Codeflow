using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    internal class CfXamlWorkflow
    {
        public string Class { get; set; }
        public List<CfArgument> Arguments { get; set; }
        public CfXamlObject? Content { get; set; }
    }
}
