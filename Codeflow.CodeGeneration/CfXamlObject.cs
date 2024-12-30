using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    internal class CfXamlObject
    {
        public CfXamlType XamlType { get; set; }
        public List<CfXamlMember> Members { get; set; }
    }
}
