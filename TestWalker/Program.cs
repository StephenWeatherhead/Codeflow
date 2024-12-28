using Codeflow.TestResources;
using System.IO;
using System.Text;
using System.Xaml;
using System.Xaml.Schema;

namespace TestWalker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xamlString = WorkflowTestResources.PrintTotalPay;
            using (XamlXmlReader xmlReader = new XamlXmlReader(new StringReader(xamlString)))
            {
                int spaces = 0;
                Stack<string> objectStack = new Stack<string>();
                Stack<string> memberStack = new Stack<string>();
                List<NamespaceDeclaration> namespaces = new List<NamespaceDeclaration>();
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XamlNodeType.NamespaceDeclaration:
                            if(xmlReader.Namespace != null)
                            {
                                namespaces.Add(xmlReader.Namespace);
                            }
                            break;
                        case XamlNodeType.StartObject:
                            Console.WriteLine(GetSpaces(spaces) + "<" + xmlReader.Type.Name + ">");
                            objectStack.Push(xmlReader.Type.Name);
                            spaces++;
                            break;
                        case XamlNodeType.StartMember:
                            string memberName = xmlReader.Member == XamlLanguage.UnknownContent ? "Content" : xmlReader.Member.Name;
                            Console.WriteLine(GetSpaces(spaces) + "<" + objectStack.Peek() + "." + memberName + ">");
                            memberStack.Push(memberName);
                            break;
                        case XamlNodeType.Value:
                            Console.WriteLine(GetSpaces(spaces) + xmlReader.Value?.ToString());
                            if(memberStack.Peek() == "Type")
                            {
                                XamlTypeName xamlTypeName = XamlTypeName.Parse((string)xmlReader.Value, new DummyResolver(namespaces));
                                XamlType xamlType = GetXamlType(xamlTypeName, xmlReader.SchemaContext);
                                //XamlType xamlType = new XamlType(xamlTypeName.Namespace, xamlTypeName.Name, xamlTypeName.TypeArguments.Select(tn => new XamlType(tn.Namespace, tn.Name, typeArguments)), xmlReader.SchemaContext);
                            }
                            break;
                        case XamlNodeType.GetObject:
                            break;
                        case XamlNodeType.EndMember:
                            string endMember = memberStack.Pop();
                            Console.WriteLine(GetSpaces(spaces) + "</" + objectStack.Peek() + "." + endMember + ">");
                            break;
                        case XamlNodeType.EndObject:
                            spaces--;
                            string endObject = objectStack.Pop();
                            Console.WriteLine(GetSpaces(spaces) + $"</{endObject}>");
                            break;
                    }
                }
            }
            Console.ReadLine();
        }
        private static string GetSpaces(int spaces)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < spaces; i++)
            {
                sb.Append("    ");
            }
            return sb.ToString();
        }

        private static XamlType GetXamlType(XamlTypeName xamlTypeName, XamlSchemaContext context)
        {
            return new XamlType(xamlTypeName.Namespace, xamlTypeName.Name, 
                xamlTypeName.TypeArguments.Select(tn => GetXamlType(tn, context)).ToList(), 
                context);
        }

    }

    internal class DummyResolver(List<NamespaceDeclaration> namespaces) : IXamlNamespaceResolver
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
