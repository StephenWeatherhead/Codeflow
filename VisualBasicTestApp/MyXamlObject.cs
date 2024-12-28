using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace VisualBasicProofOfConcept
{
    public class MyXamlObject
    {
        public MyXamlObject()
        {
            Members = new List<MyXamlMember>();
        }
        public XamlType? XamlType { get; set; }
        public List<MyXamlMember> Members { get; set; }

        public override string ToString()
        {
            return XamlType?.Name == null ? "Object" : XamlType.Name;
        }
    }
}
