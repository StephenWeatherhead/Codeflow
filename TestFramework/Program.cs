using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.Activities.XamlIntegration;
using System.IO;

namespace TestFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xamlString = "<Activity \r\n x:Class=\"WorkflowConsoleApplication1.HelloWorld\"\r\n xmlns=\"http://schemas.microsoft.com/netfx/2009/xaml/activities\"\r\n xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\r\n  <Sequence>\r\n    <WriteLine Text=\"Hello World!\" />\r\n  </Sequence>\r\n</Activity>";
            Activity workflow = ActivityXamlServices.Load(new StringReader(xamlString));
            Console.WriteLine(workflow.DisplayName);
            DynamicActivity dynamicCast = (DynamicActivity)workflow;
            Activity childActivity = dynamicCast.Implementation();
            Console.ReadLine();
        }
    }
}
