using Codeflow.CodeGeneration;
using Codeflow.TestResources;

namespace SimpleWorkflowProofOfConcept
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is proof-of-concept aims to parse a simple workflow, generate code, and run it.");
            Console.WriteLine();
            // parse workflow
            var workflow = WorkflowParser.Parse(WorkflowTestResources.PrintTotalPay);
            Console.WriteLine("The workflow name is : " + workflow.Class);
            // print workflow c#
            // print workflow vb
            // run compiled workflow
            Console.ReadLine();
        }
    }
}
