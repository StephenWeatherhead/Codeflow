using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    internal class CfArgument
    {
        public string Name { get; set; }
        public CfArgumentDirection Direction { get; set; }
        public CfXamlType ArgumentType { get; set; }
        public bool IsRequired { get; set; }
        public string Value { get; set; }
    }
}
