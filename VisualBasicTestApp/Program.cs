using VisualBasicTestLibrary;
using Codeflow.CodeGeneration;
using System.Xaml;

namespace VisualBasicTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // let's print a vb method
            var integerType = new XamlType(typeof(int), new XamlSchemaContext());
            string method = CodeflowUtils.GenerateVisualBasicMethod("CalculateSum", integerType, 
                new Dictionary<string, XamlType>
                {
                    { "numberA", integerType },
                    { "numberB", integerType }
                }, "numberA + numberB");
            Console.WriteLine(method);
            // now let's use the generated method
            Console.WriteLine("Using the above method 3 + 4 = " + MyTestClass.CalculateSum(3, 4));
            Console.ReadLine();
        }
    }
}
