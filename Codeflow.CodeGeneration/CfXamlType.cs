using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    public class CfXamlType
    {
        public CfXamlType(string name)
        {
            TypeArguments = new List<CfXamlType>();
            Name = name;
        }
        public string Name { get; set; }
        public bool IsArray { get; set; }
        public bool IsGeneric { get; set; }
        public List<CfXamlType> TypeArguments { get; set; }
        public CfXamlType? ItemType { get; set; }
    }
}
