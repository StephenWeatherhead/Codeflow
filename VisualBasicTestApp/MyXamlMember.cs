using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualBasicProofOfConcept
{
    public class MyXamlMember
    {
        public MyXamlMember()
        {
            Content = new List<MyXamlObject>();
        }
        public string? Name { get; set; }
        public bool IsContent { get; set; }
        public string? Value { get; set; }
        public List<MyXamlObject> Content { get; set; }

        public override string ToString()
        {
            return IsContent ? "_Content" : Name;
        }
    }
}
