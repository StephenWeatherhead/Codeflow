﻿using VisualBasicTestLibrary;
using Codeflow.CodeGeneration;
using System.Xaml;
using System.IO;
using System.Xml;

namespace VisualBasicTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xamlString = "<Activity mc:Ignorable=\"sap sap2010 sads\" x:Class=\"FrameworkConsoleTestApp.Activity1\" this:Activity1.hoursWorked=\"7.5\" this:Activity1.payRate=\"10\" this:Activity1.employeeName=\"Jeffrey\" this:Activity1.myList=\"[New List(Of String)]\" this:Activity1.myDictionary=\"[New Dictionary(Of String, Int32)]\"\r\n xmlns=\"http://schemas.microsoft.com/netfx/2009/xaml/activities\"\r\n xmlns:mc=\"http://schemas.openxmlformats.org/markup-compatibility/2006\"\r\n xmlns:sads=\"http://schemas.microsoft.com/netfx/2010/xaml/activities/debugger\"\r\n xmlns:sap=\"http://schemas.microsoft.com/netfx/2009/xaml/activities/presentation\"\r\n xmlns:sap2010=\"http://schemas.microsoft.com/netfx/2010/xaml/activities/presentation\"\r\n xmlns:scg=\"clr-namespace:System.Collections.Generic;assembly=mscorlib\"\r\n xmlns:sco=\"clr-namespace:System.Collections.ObjectModel;assembly=mscorlib\"\r\n xmlns:this=\"clr-namespace:FrameworkConsoleTestApp\"\r\n xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\r\n  <x:Members>\r\n    <x:Property Name=\"hoursWorked\" Type=\"InArgument(x:Double)\" />\r\n    <x:Property Name=\"payRate\" Type=\"InArgument(x:Double)\" />\r\n    <x:Property Name=\"employeeName\" Type=\"InArgument(x:String)\" />\r\n    <x:Property Name=\"myList\" Type=\"InArgument(scg:List(x:String))\" />\r\n    <x:Property Name=\"myDictionary\" Type=\"InArgument(scg:Dictionary(x:String, x:Int32))\" />\r\n  </x:Members>\r\n  <sap2010:WorkflowViewState.IdRef>FrameworkConsoleTestApp.Activity1_1</sap2010:WorkflowViewState.IdRef>\r\n  <TextExpression.NamespacesForImplementation>\r\n    <sco:Collection x:TypeArguments=\"x:String\">\r\n      <x:String>System</x:String>\r\n      <x:String>System.Collections.Generic</x:String>\r\n      <x:String>System.Data</x:String>\r\n      <x:String>System.Linq</x:String>\r\n      <x:String>System.Text</x:String>\r\n      <x:String>System.Activities</x:String>\r\n    </sco:Collection>\r\n  </TextExpression.NamespacesForImplementation>\r\n  <TextExpression.ReferencesForImplementation>\r\n    <sco:Collection x:TypeArguments=\"AssemblyReference\">\r\n      <AssemblyReference>mscorlib</AssemblyReference>\r\n      <AssemblyReference>System</AssemblyReference>\r\n      <AssemblyReference>System.Core</AssemblyReference>\r\n      <AssemblyReference>System.Data</AssemblyReference>\r\n      <AssemblyReference>System.ServiceModel</AssemblyReference>\r\n      <AssemblyReference>System.Xml</AssemblyReference>\r\n      <AssemblyReference>System.Activities</AssemblyReference>\r\n    </sco:Collection>\r\n  </TextExpression.ReferencesForImplementation>\r\n  <Sequence sap2010:WorkflowViewState.IdRef=\"Sequence_1\">\r\n    <Sequence.Variables>\r\n      <Variable x:TypeArguments=\"x:Double\" Name=\"totalPay\" />\r\n    </Sequence.Variables>\r\n    <Assign sap2010:WorkflowViewState.IdRef=\"Assign_1\">\r\n      <Assign.To>\r\n        <OutArgument x:TypeArguments=\"x:Double\">[totalPay]</OutArgument>\r\n      </Assign.To>\r\n      <Assign.Value>\r\n        <InArgument x:TypeArguments=\"x:Double\">[hoursWorked * payRate]</InArgument>\r\n      </Assign.Value>\r\n    </Assign>\r\n    <WriteLine sap2010:WorkflowViewState.IdRef=\"WriteLine_1\" Text=\"[employeeName + &quot; earned £&quot; + totalPay.ToString() + &quot; in total.&quot;]\" />\r\n    <sads:DebugSymbol.Symbol>d0tDOlxVc2Vyc1xzdGVwaFxzb3VyY2VccmVwb3NcQ29kZWZsb3dcRnJhbWV3b3JrQ29uc29sZVRlc3RBcHBcQWN0aXZpdHkxLnhhbWwLAYQCAacCAQYBcgF2AQUB0AEB5gEBBAGwAQG4AQEDAY8BAZIBAQIoAzYOAgEBLAUzDgIBBjQFNJ0BAgECMTAxRwIBCS4xLjsCAQc0QzSaAQIBAw==</sads:DebugSymbol.Symbol>\r\n  </Sequence>\r\n  <sap2010:WorkflowViewState.ViewStateManager>\r\n    <sap2010:ViewStateManager>\r\n      <sap2010:ViewStateData Id=\"Assign_1\" sap:VirtualizedContainerService.HintSize=\"242,60\" />\r\n      <sap2010:ViewStateData Id=\"WriteLine_1\" sap:VirtualizedContainerService.HintSize=\"242,61\" />\r\n      <sap2010:ViewStateData Id=\"Sequence_1\" sap:VirtualizedContainerService.HintSize=\"264,285\">\r\n        <sap:WorkflowViewStateService.ViewState>\r\n          <scg:Dictionary x:TypeArguments=\"x:String, x:Object\">\r\n            <x:Boolean x:Key=\"IsExpanded\">True</x:Boolean>\r\n          </scg:Dictionary>\r\n        </sap:WorkflowViewStateService.ViewState>\r\n      </sap2010:ViewStateData>\r\n      <sap2010:ViewStateData Id=\"FrameworkConsoleTestApp.Activity1_1\" sap:VirtualizedContainerService.HintSize=\"304,365\" />\r\n    </sap2010:ViewStateManager>\r\n  </sap2010:WorkflowViewState.ViewStateManager>\r\n</Activity>";
            
            Console.WriteLine("In our VB proof of concept, we're targeting the WriteLine Text property");
            Console.WriteLine("so our return type will be String. The challenge is detecting the ");
            Console.WriteLine("parameter names, parameter types, and the expression itself.");
            using (XamlXmlReader xmlReader = new XamlXmlReader(new StringReader(xamlString)))
            {
                MyXamlObject? currentObject = null;
                MyXamlMember? currentMember = null;
                Stack<MyXamlObject> objectStack = new Stack<MyXamlObject>();
                Stack<MyXamlMember> memberStack = new Stack<MyXamlMember>();
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
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
                var contentMember = currentObject.Members.First(m => m.IsContent);
                var sequenceActivity = contentMember.Content.First();
                var writeLine = sequenceActivity.Members
                    .First(m => m.IsContent)
                    .Content.First(xo => xo.XamlType.Name == "WriteLine");
                var writeLineText = writeLine.Members.First(m => m.Name == "Text").Value;
                Console.WriteLine("The WriteLine expression is : " + writeLineText);
            }
            Console.ReadLine();
        }
    }

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
