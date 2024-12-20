using VisualBasicTestLibrary;
namespace VisualBasicTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // demonstrates that a visual basic library can be called from C#
            MyTestClass.PrintMessage("HELLO WORLD!!!");
            Console.ReadLine();
        }
    }
}
