using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    public class CfXamlObject
    {
        public CfXamlObject(CfXamlType type)
        {
            XamlType = type;
            Members = new List<CfXamlMember>();
        }
        public CfXamlType XamlType { get; set; }
        public List<CfXamlMember> Members { get; set; }
    }
}
