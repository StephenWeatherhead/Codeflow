using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    internal class CfXamlType
    {
        public bool IsArray { get; set; }
        public bool IsGeneric { get; set; }
        public List<CfXamlType> TypeArguments { get; set; }
    }
}
