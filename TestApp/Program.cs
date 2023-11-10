using System.Activities;
namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WorkflowInvoker.Invoke(new HelloWorldActivity());
            WorkflowInvoker.Invoke(new HelloWorldCodeflow());
            WorkflowInvoker.Invoke(new HelloWorldActivity());
            Console.ReadKey();
        }
    }
}