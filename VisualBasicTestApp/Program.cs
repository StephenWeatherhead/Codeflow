using VisualBasicTestLibrary;
using Codeflow.CodeGeneration;
using System.Xaml;

namespace VisualBasicTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // demonstrates that a visual basic library can be called from C#
            MyTestClass.PrintMessage("HELLO WORLD!!!");
            // print static method
            //Console.WriteLine("Now let's print the method based on the expression: ");
            //CodeflowUtils.GenerateVisualBasicMethod("GetTotalPayMessage", typeof(string), )

            // run 
            //Console.WriteLine("And running that method results in:");
            //Console.WriteLine(MyTestClass.GetTotalPayMessage("Stephen", 100.50));
            XamlType myType = new XamlType(typeof(Dictionary<string, List<string[]>>), new XamlSchemaContext());
            // generic type argument
            Console.WriteLine(CodeflowUtils.GetVisualBasicTypeName(myType));
            Console.ReadLine();
        }
    }
}
