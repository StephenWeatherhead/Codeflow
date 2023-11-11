using System.Activities;
namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WorkflowInvoker.Invoke(new HelloWorldActivity());
            WorkflowInvoker.Invoke(new HelloWorldCodeflow(), new Dictionary<string, object>()
            {
                { nameof(HelloWorldCodeflow.Name), "Stephen"}
            });
            Console.ReadKey();
        }
    }
}