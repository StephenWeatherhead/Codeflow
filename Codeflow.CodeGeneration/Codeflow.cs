using System.Xaml.Schema;
using System.Xaml;
namespace Codeflow.CodeGeneration
{
    public class CodeflowUtils
    {
        public static string GenerateVisualBasicMethod(string name, XamlType returnType, Dictionary<string, XamlType> parameters, string expression)
        {
            return $"Public Shared Function {name}({string.Join(", ", parameters.Select(p => $"{p.Key} As {GetVisualBasicTypeName(p.Value)}"))}) As {GetVisualBasicTypeName(returnType)}\r\n    Return {expression}\r\nEnd Function";
        }

        public static string GetVisualBasicTypeName(XamlType type)
        {
            if(type.IsArray)
            {
                return $"{GetVisualBasicTypeName(type.ItemType)}()";
            }
            else if (type.IsGeneric)
            {
                string baseTypeName = type.Name;
                return baseTypeName + "(Of " + string.Join(", ", type.TypeArguments.Select(t => GetVisualBasicTypeName(t))) + ")";
            }
            else
            {
                return type.Name;
            }
        }
    }
}
