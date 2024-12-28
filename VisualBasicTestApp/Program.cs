using VisualBasicTestLibrary;
using System.Xaml;
using System.IO;
using System.Xml;
using Codeflow.TestResources;
using System.Xaml.Schema;
using System.Runtime.InteropServices;
using System.Windows.Markup;

namespace VisualBasicProofOfConcept
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xamlString = WorkflowTestResources.PrintTotalPay;
            
            Console.WriteLine("In our VB proof of concept, we're targeting the WriteLine Text property");
            Console.WriteLine("so our return type will be String. The challenge is detecting the ");
            Console.WriteLine("parameter names, parameter types, and the expression itself.");
            Console.WriteLine();
            // parse workflow
            string expression;
            Dictionary<string, XamlType> parameters = new Dictionary<string, XamlType>();
            using (XamlXmlReader xmlReader = new XamlXmlReader(new StringReader(xamlString)))
            {
                MyXamlObject? currentObject = null;
                MyXamlMember? currentMember = null;
                Stack<MyXamlObject> objectStack = new Stack<MyXamlObject>();
                Stack<MyXamlMember> memberStack = new Stack<MyXamlMember>();
                List<NamespaceDeclaration> namespaces = new List<NamespaceDeclaration>();
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
                        case XamlNodeType.NamespaceDeclaration:
                            if (xmlReader.Namespace != null)
                            {
                                namespaces.Add(xmlReader.Namespace);
                            }
                            break;
                        case XamlNodeType.StartObject:
                            currentObject = new MyXamlObject();
                            currentObject.XamlType = xmlReader.Type;
                            break;
                        case XamlNodeType.EndObject:
                            if(currentMember != null)
                            {
                                currentMember.Content.Add(currentObject);
                                currentObject = null;
                            }
                            break;
                        case XamlNodeType.StartMember:
                            // push member and object on to stack
                            objectStack.Push(currentObject);
                            currentObject = null;
                            if (currentMember != null)
                            {
                                memberStack.Push(currentMember);
                                currentMember = null;
                            }
                            currentMember = new MyXamlMember();
                            if(xmlReader.Member == XamlLanguage.UnknownContent)
                            {
                                currentMember.IsContent = true;
                            }
                            else
                            {
                                currentMember.Name = xmlReader.Member.Name;
                            }
                            break;
                        case XamlNodeType.EndMember:
                            currentObject = objectStack.Pop();
                            currentObject.Members.Add(currentMember);
                            currentMember = null;
                            if(memberStack.Any())
                            {
                                currentMember = memberStack.Pop();
                            }
                            break;
                        case XamlNodeType.Value:
                            currentMember.Value = xmlReader.Value.ToString();
                            break;
                    }
                }
                // get expression
                var contentMember = currentObject.Members.First(m => m.IsContent);
                var sequenceActivity = contentMember.Content.First();
                var writeLine = sequenceActivity.Members
                    .First(m => m.IsContent)
                    .Content.First(xo => xo.XamlType.Name == "WriteLine");
                var writeLineText = writeLine.Members.First(m => m.Name == "Text").Value;
                expression = writeLineText.Substring(1, writeLineText.Length - 2);
                // get overall parameters
                Dictionary<string, XamlType> overallParameters = new Dictionary<string, XamlType>();
                var activityProperties = currentObject.Members.First(m => m.Name == "Members").Content;
                foreach(var propertyObject in activityProperties)
                {
                    string propertyName = propertyObject.Members.First(m => m.Name == "Name").Value;
                    string typeString = propertyObject.Members.First(m => m.Name == "Type").Value;
                    XamlType propertyType = GetXamlType(typeString, namespaces, xmlReader.SchemaContext)
                        .TypeArguments.First();
                    overallParameters.Add(propertyName, propertyType);
                }
                // get sequence parameters
                // detect parameters in expression
            }
            Console.WriteLine(VBHelper.GenerateVisualBasicMethod("GetWriteLineExpression", new XamlType(typeof(string), new XamlSchemaContext()), parameters, expression));
            Console.ReadLine();
        }

        private static XamlType GetXamlType(string typeName, List<NamespaceDeclaration> namespaces, XamlSchemaContext context)
        {
            XamlTypeName xamlTypeName = XamlTypeName.Parse(typeName, new SimpleXamlNamespaceResolver(namespaces));
            return GetXamlType(xamlTypeName, context);
        }
        private static XamlType GetXamlType(XamlTypeName xamlTypeName, XamlSchemaContext context)
        {
            return new XamlType(xamlTypeName.Namespace, xamlTypeName.Name,
                xamlTypeName.TypeArguments.Select(tn => GetXamlType(tn, context)).ToList(),
                context);
        }
    }
}
