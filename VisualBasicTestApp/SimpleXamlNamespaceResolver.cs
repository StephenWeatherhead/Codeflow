using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using System.Xml.Linq;

namespace VisualBasicProofOfConcept
{
    internal class SimpleXamlNamespaceResolver(List<NamespaceDeclaration> namespaces) : IXamlNamespaceResolver
    {
        public string GetNamespace(string prefix)
        {
            return namespaces.FirstOrDefault(space =>
            string.Equals(space.Prefix, prefix, StringComparison.InvariantCultureIgnoreCase))
                .Namespace;
        }

        public IEnumerable<NamespaceDeclaration> GetNamespacePrefixes()
        {
            return namespaces;
        }
    }
}
