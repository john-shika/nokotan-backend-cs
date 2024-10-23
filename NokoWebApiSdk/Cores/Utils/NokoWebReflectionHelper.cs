using System.Reflection;

namespace NokoWebApiSdk.Cores.Utils;

public class NokoWebReflectionHelper
{
    public readonly Type BaseType;

    public NokoWebReflectionHelper(Type baseType)
    {
        BaseType = baseType;
    }
    
    public MethodInfo? GetMethod(string mName, IDictionary<Type, Type>? gData, params Type[] pTypes)
    {
        gData ??= new Dictionary<Type, Type>();
        
        var gSize = gData.Count;
        var isGenericMethod = gSize > 0;
            
        var method = BaseType
            .GetMethods()
            .FirstOrDefault(m =>
            {
                var gArgs = m.GetGenericArguments();
                var parameters = m.GetParameters();
                    
                if (m.Name != mName) return false;
                if (m.IsGenericMethodDefinition != isGenericMethod) return false;

                if (gArgs.Length != gSize) return false;
                if (parameters.Length != pTypes.Length) return false;

                for (var i = 1; i < gSize; i++)
                {
                    var gArg = gArgs[i].BaseType;
                    var gKey = gData.ElementAt(i).Key;
                    if (!gKey.IsAssignableFrom(gArg)) return false;
                }
                    
                for (var i = 1; i < pTypes.Length; i++)
                {
                    var pType = parameters[i].ParameterType;
                    if (!pTypes[i].IsAssignableFrom(pType)) return false;
                }

                return true;
            })!;

        var gValues = gData.Values.ToArray();
        return isGenericMethod ? method.MakeGenericMethod(gValues) : method;
    }
}