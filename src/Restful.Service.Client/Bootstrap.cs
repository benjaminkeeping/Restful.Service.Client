using System;
using System.Linq;
using System.Reflection;

namespace Restful.Service.Client
{
    public static class Bootstrap
    {
        public static void RegisterClients(Assembly[] assemblyToFindTypes, Action<Type, Type> registerTypeForContainer)
        {
            var allTypes = assemblyToFindTypes.SelectMany(a => a.GetTypes());
            var allServiceClientInterfaces =
                allTypes.Where(x => typeof(IServiceClient).IsAssignableFrom(x) && x.IsInterface && x != typeof(IServiceClient)).ToList();
            foreach (var @interface in allServiceClientInterfaces)
            {
                registerTypeForContainer(@interface, allTypes.Where(x => @interface.IsAssignableFrom(x) && x.IsClass).FirstOrDefault());
            }
        }
    }
}