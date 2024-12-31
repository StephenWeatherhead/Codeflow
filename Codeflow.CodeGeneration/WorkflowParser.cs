using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace Codeflow.CodeGeneration
{
    public class WorkflowParser
    {
        public static CfXamlType GetCfXamlType(XamlType type)
        {
            var cfType = new CfXamlType(type.Name);
            cfType.IsArray = type.IsArray;
            cfType.IsGeneric = type.IsGeneric;
            cfType.ItemType = type.ItemType == null ? null : GetCfXamlType(type.ItemType);
            if(type.TypeArguments != null && type.TypeArguments.Any())
            {
                cfType.TypeArguments = type.TypeArguments.Select(t => GetCfXamlType(t)).ToList();
            }
            return cfType;
        }
        public static CfXamlWorkflow Parse(string workflowString)
        {
            var workflow = new CfXamlWorkflow();
            using (XamlXmlReader xamlReader = new XamlXmlReader(new StringReader(workflowString)))
            {
                CfXamlObject? currentObject = null;
                CfXamlMember? currentMember = null;
                Stack<CfXamlObject> objectStack = new Stack<CfXamlObject>();
                Stack<CfXamlMember> memberStack = new Stack<CfXamlMember>();
                List<NamespaceDeclaration> namespaces = new List<NamespaceDeclaration>();
                while (xamlReader.Read())
                {
                    switch (xamlReader.NodeType)
                    {
                        case XamlNodeType.NamespaceDeclaration:
                            if (xamlReader.Namespace != null)
                            {
                                namespaces.Add(xamlReader.Namespace);
                            }
                            break;
                        case XamlNodeType.StartObject:
                            currentObject = new CfXamlObject(GetCfXamlType(xamlReader.Type));
                            break;
                        case XamlNodeType.EndObject:
                            if (currentMember != null)
                            {
                                if(currentObject == null)
                                {
                                    throw new ArgumentNullException(nameof(currentObject));
                                }
                                currentMember.Content.Add(currentObject);
                                currentObject = null;
                            }
                            break;
                        case XamlNodeType.StartMember:
                            // push member and object on to stack
                            if (currentObject == null)
                            {
                                throw new ArgumentNullException(nameof(currentObject));
                            }
                            objectStack.Push(currentObject);
                            currentObject = null;
                            if (currentMember != null)
                            {
                                memberStack.Push(currentMember);
                                currentMember = null;
                            }
                            currentMember = new CfXamlMember(xamlReader.Member.Name);
                            break;
                        case XamlNodeType.EndMember:
                            currentObject = objectStack.Pop();
                            if (currentMember == null)
                                throw new ArgumentNullException(nameof(currentMember));
                            currentObject.Members.Add(currentMember);
                            currentMember = null;
                            if (memberStack.Any())
                            {
                                currentMember = memberStack.Pop();
                            }
                            break;
                        case XamlNodeType.Value:
                            if (currentMember == null)
                                throw new ArgumentNullException(nameof(currentMember));
                            currentMember.Value = xamlReader.Value.ToString();
                            break;
                    }
                }
                if (currentObject == null)
                    throw new Exception("The workflow object returned null from parsing.");
                // set class
                workflow.Class = currentObject.Members.First(m => m.Name == "Class").Value;
            }
            return workflow;
        }
    }
}
