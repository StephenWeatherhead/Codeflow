using System.Activities;
using System.Activities.XamlIntegration;
using System.Text;

namespace TestWalker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xamlString = "<Activity \r\n x:Class=\"WorkflowConsoleApplication1.HelloWorld\"\r\n xmlns=\"http://schemas.microsoft.com/netfx/2009/xaml/activities\"\r\n xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\r\n  <Sequence>\r\n    <WriteLine Text=\"Hello World!\" />\r\n  </Sequence>\r\n</Activity>";
            Activity workflow = ActivityXamlServices.Load(new StringReader(xamlString));
            Console.WriteLine(workflow.DisplayName);
            Console.ReadLine();
        }
    }
}
