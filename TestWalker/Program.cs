using System.IO;
using System.Text;
using System.Xaml;

namespace TestWalker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xamlString = "<Activity \r\n x:Class=\"WorkflowConsoleApplication1.HelloWorld\"\r\n xmlns=\"http://schemas.microsoft.com/netfx/2009/xaml/activities\"\r\n xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">\r\n  <Sequence>\r\n    <WriteLine Text=\"Hello World!\" />\r\n  </Sequence>\r\n</Activity>";
            using (XamlXmlReader xmlReader = new XamlXmlReader(new StringReader(xamlString)))
            {
                int spaces = 0;
                Stack<string> objectStack = new Stack<string>();
                Stack<string> memberStack = new Stack<string>();
                while (xmlReader.Read())
                {
                    switch (xmlReader.NodeType)
                    {
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
                            Console.WriteLine(GetSpaces(spaces) + xmlReader.Value.ToString());
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
    }
}
