using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SignalAlgorithmEditor.Services
{
    public class PluginLoader
    {
        public Dictionary<string, IOperation> LoadOperations(string pluginDirectory)
        {
            var operations = new Dictionary<string, IOperation>();

            var dllFiles = Directory.GetFiles(pluginDirectory, "*.dll");

            foreach(string dllPath in dllFiles)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dllPath);

                    var operationTypes = assembly.GetTypes()
                        .Where(t => typeof(IOperation).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach(Type type in operationTypes)
                    {
                        IOperation operation = (IOperation)Activator.CreateInstance(type)!;

                        string opeartionName = type.Name;

                        operations[opeartionName] = operation;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки плагина из {dllPath}: {ex.Message}");
                }
            }
            return operations;
        }
    }
}
