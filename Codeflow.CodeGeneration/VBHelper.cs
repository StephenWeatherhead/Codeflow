using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codeflow.CodeGeneration
{
    public class VBHelper
    {
        public static string GenerateVisualBasicMethod(string name, CfXamlType returnType, Dictionary<string, CfXamlType> parameters, string expression)
        {
            return $"Public Shared Function {name}({string.Join(", ", parameters.Select(p => $"{p.Key} As {GetVisualBasicTypeName(p.Value)}"))}) As {GetVisualBasicTypeName(returnType)}\r\n    Return {expression}\r\nEnd Function";
        }

        public static string GetVisualBasicTypeName(CfXamlType type)
        {
            if (type.IsArray)
            {
                if(type.ItemType == null)
                {
                    throw new ArgumentNullException(nameof(type.ItemType));
                }
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
