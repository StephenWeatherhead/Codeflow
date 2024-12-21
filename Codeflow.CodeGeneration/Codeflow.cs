using System.Xaml.Schema;
using System.Xaml;
namespace Codeflow.CodeGeneration
{
    public class CodeflowUtils
    {
        public static string GenerateVisualBasicMethod(string name, XamlType returnType, Dictionary<string, XamlType> parameters, string expression)
        {
            throw new NotImplementedException();
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
