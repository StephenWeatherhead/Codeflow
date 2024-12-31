using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    public class CfXamlMember
    {
        public CfXamlMember(string name)
        {
            Name = name;
            Content = new List<CfXamlObject>();
        }
        public string Name { get; set; }
        public string? Value { get; set; }
        public List<CfXamlObject> Content { get; set; }
    }
}
