using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace WebServer
{
    public static class TypeExtension
    {
        static Dictionary<string, MethodInfo> _methodMap = new Dictionary<string, MethodInfo>();
        static public MethodInfo? FindMethod(this Type type, string name)
        {
            var asm = type.FullName;

            name = name.ToLower();
            var key = asm + '.' + name;
            MethodInfo? method = null;

            if (_methodMap.TryGetValue(key, out method) == false)
            {
                foreach (var m in type.GetMethods())
                {
                    if (m.Name.ToLower() == name)
                    {
                        _methodMap.Add(key, method = m);
                    }
                }
            }
            return method;
        }

        static Dictionary<string, Type> _controllerMap = new Dictionary<string, Type>();
        static public void LoadTypes(Assembly asm, Action<Type>? callback)
        {
            const string cname = "Controller";
            foreach (var type in asm.GetTypes())
            {
                callback?.Invoke(type);
                if (type.Name.Length > cname.Length && type.Name.Substring(type.Name.Length - cname.Length) == cname)
                {
                    var tname = type.Name.Substring(0, type.Name.Length - cname.Length);
                    _controllerMap.Add(tname.ToLower(), type);
                }
            }
        }
        static public Type? GetControllerType(string name)
        {
            Type? type = null;
            _controllerMap.TryGetValue(name.ToLower(), out type);
            
            return type;
        }
    }
}
