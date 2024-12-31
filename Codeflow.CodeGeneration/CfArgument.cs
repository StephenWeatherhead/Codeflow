using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    public class CfArgument
    {
        public CfArgument(string name, CfXamlType type)
        {
            Name = name;
            ArgumentType = type;
        }
        public string Name { get; set; }
        public CfArgumentDirection Direction { get; set; }
        public CfXamlType ArgumentType { get; set; }
        public bool IsRequired { get; set; }
        public string? Value { get; set; }
    }
}
