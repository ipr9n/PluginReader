using System;
using System.IO;
using System.Linq;
using System.Reflection;
using PluginReader.Interface;

namespace PluginReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            const string pathToFind = "c:\\";
            const string methodToInvoke = "GetBiography";

            FindAllDlls(pathToFind, out string[] dllPaths);

            if (dllPaths != default)
            {
                Console.WriteLine($"Count of .dll files: {dllPaths.Length}");

                foreach (var dll in dllPaths)
                {
                    Console.WriteLine($"File: {dll}");

                    Assembly asm = Assembly.LoadFrom(dll);
                    Type[] allTypes = asm.GetTypes();

                    foreach (var type in allTypes)
                    {
                        var inter = type.GetInterface("IInfoReadable");

                        if (inter != null)
                        {
                            if (CompareInterfaces(inter.GetMembers()))
                            {
                                InvokeMethod(methodToInvoke, type);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Different interfaces with the same name");
                                Console.ForegroundColor = ConsoleColor.Red;
                                InvokeMethod(methodToInvoke,type);
                            }
                        }
                        else
                            Console.WriteLine($"Type: {type} don't have this interface");
                    }
                    Console.WriteLine("\n\n\n");
                }
            }
        }

        private static void InvokeMethod(string methodName, Type type)
        {
            Console.WriteLine($"Try to invoke method: {methodName} from type {type}");

            var method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            object obj = Activator.CreateInstance(type);

            object GetBiography = method?.Invoke(obj, null);

            Console.WriteLine(GetBiography);
        }

        private static void FindAllDlls(string path, out string[] dll)
        {
            try
            {
                string[] dirs = Directory.GetFiles(path, "*.dll");
                dll = dirs;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e.ToString()}");
                dll = default;
            }
        }

      static bool CompareInterfaces(MemberInfo[] memberInfo)
        {
            Type myInterface = typeof(IInfoReadable);
            var myMembers = myInterface.GetMembers();
            int countMembers = 0;

            foreach (var myMember in myMembers)
            {
                countMembers += memberInfo.Count(t => t.Name == myMember.Name);
            }

            if (countMembers == memberInfo.Length) return true;
            else return false;
        }
    }
}
