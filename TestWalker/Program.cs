﻿using System.IO;
using System.Text;
using System.Xaml;
using System.Xaml.Schema;

namespace TestWalker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xamlString = "<Activity mc:Ignorable=\"sap sap2010 sads\" x:Class=\"FrameworkConsoleTestApp.Activity1\" this:Activity1.hoursWorked=\"7.5\" this:Activity1.payRate=\"10\" this:Activity1.employeeName=\"Jeffrey\" this:Activity1.myList=\"[New List(Of String)]\" this:Activity1.myDictionary=\"[New Dictionary(Of String, Int32)]\"\r\n xmlns=\"http://schemas.microsoft.com/netfx/2009/xaml/activities\"\r\n xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"\r\n xmlns:sads=\"http://schemas.microsoft.com/netfx/2010/xaml/activities/debugger\"\r\n xmlns:sap=\"http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation\"\r\n xmlns:sap2010=\"http://schemas.microsoft.com/netfx/2010/xaml/activities/presentation\"\r\n xmlns:scg=\"clr-namespace:System.Collections.Generic;assembly=mscorlib\"\r\n xmlns:sco=\"clr-namespace:System.Collections.ObjectModel;assembly=mscorlib\"\r\n xmlns:this=\"clr-namespace:FrameworkConsoleTestApp\"\r\n xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\r\n  <x:Members>\r\n    <x:Property Name=\"hoursWorked\" Type=\"InArgument(x:Double)\" />\r\n    <x:Property Name=\"payRate\" Type=\"InArgument(x:Double)\" />\r\n    <x:Property Name=\"employeeName\" Type=\"InArgument(x:String)\" />\r\n    <x:Property Name=\"myList\" Type=\"InArgument(scg:List(x:String))\" />\r\n    <x:Property Name=\"myDictionary\" Type=\"InArgument(scg:Dictionary(x:String, x:Int32))\" />\r\n  </x:Members>\r\n  <sap2010:WorkflowViewState.IdRef>FrameworkConsoleTestApp.Activity1_1</sap2010:WorkflowViewState.IdRef>\r\n  <TextExpression.NamespacesForImplementation>\r\n    <sco:Collection x:TypeArguments=\"x:String\">\r\n      <x:String>System</x:String>\r\n      <x:String>System.Collections.Generic</x:String>\r\n      <x:String>System.Data</x:String>\r\n      <x:String>System.Linq</x:String>\r\n      <x:String>System.Text</x:String>\r\n      <x:String>System.Activities</x:String>\r\n    </sco:Collection>\r\n  </TextExpression.NamespacesForImplementation>\r\n  <TextExpression.ReferencesForImplementation>\r\n    <sco:Collection x:TypeArguments=\"AssemblyReference\">\r\n      <AssemblyReference>mscorlib</AssemblyReference>\r\n      <AssemblyReference>System</AssemblyReference>\r\n      <AssemblyReference>System.Core</AssemblyReference>\r\n      <AssemblyReference>System.Data</AssemblyReference>\r\n      <AssemblyReference>System.ServiceModel</AssemblyReference>\r\n      <AssemblyReference>System.Xml</AssemblyReference>\r\n      <AssemblyReference>System.Activities</AssemblyReference>\r\n    </sco:Collection>\r\n  </TextExpression.ReferencesForImplementation>\r\n  <Sequence sap2010:WorkflowViewState.IdRef=\"Sequence_1\">\r\n    <Sequence.Variables>\r\n      <Variable x:TypeArguments=\"x:Double\" Name=\"totalPay\" />\r\n    </Sequence.Variables>\r\n    <Assign sap2010:WorkflowViewState.IdRef=\"Assign_1\">\r\n      <Assign.To>\r\n        <OutArgument x:TypeArguments=\"x:Double\">[totalPay]</OutArgument>\r\n      </Assign.To>\r\n      <Assign.Value>\r\n        <InArgument x:TypeArguments=\"x:Double\">[hoursWorked * payRate]</InArgument>\r\n      </Assign.Value>\r\n    </Assign>\r\n    <WriteLine sap2010:WorkflowViewState.IdRef=\"WriteLine_1\" Text=\"[employeeName + &quot; earned £&quot; + totalPay.ToString() + &quot; in total.&quot;]\" />\r\n    <sads:DebugSymbol.Symbol>d0tDOlxVc2Vyc1xzdGVwaFxzb3VyY2VccmVwb3NcQ29kZWZsb3dcRnJhbWV3b3JrQ29uc29sZVRlc3RBcHBcQWN0aXZpdHkxLnhhbWwLAYQCAacCAQYBcgF2AQUB0AEB5gEBBAGwAQG4AQEDAY8BAZIBAQIoAzYOAgEBLAUzDgIBBjQFNJ0BAgECMTAxRwIBCS4xLjsCAQc0QzSaAQIBAw==</sads:DebugSymbol.Symbol>\r\n  </Sequence>\r\n  <sap2010:WorkflowViewState.ViewStateManager>\r\n    <sap2010:ViewStateManager>\r\n      <sap2010:ViewStateData Id=\"Assign_1\" sap:VirtualizedContainerService.HintSize=\"242,60\" />\r\n      <sap2010:ViewStateData Id=\"WriteLine_1\" sap:VirtualizedContainerService.HintSize=\"242,61\" />\r\n      <sap2010:ViewStateData Id=\"Sequence_1\" sap:VirtualizedContainerService.HintSize=\"264,285\">\r\n        <sap:WorkflowViewStateService.ViewState>\r\n          <scg:Dictionary x:TypeArguments=\"x:String, x:Object\">\r\n            <x:Boolean x:Key=\"IsExpanded\">True</x:Boolean>\r\n          </scg:Dictionary>\r\n        </sap:WorkflowViewStateService.ViewState>\r\n      </sap2010:ViewStateData>\r\n      <sap2010:ViewStateData Id=\"FrameworkConsoleTestApp.Activity1_1\" sap:VirtualizedContainerService.HintSize=\"304,365\" />\r\n    </sap2010:ViewStateManager>\r\n  </sap2010:WorkflowViewState.ViewStateManager>\r\n</Activity>";
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
