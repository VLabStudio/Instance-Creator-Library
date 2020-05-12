using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace InstanceCreatorLibrary
{
    public class InstanceCreator
    {
        public static List<dynamic> Assemblies = new List<dynamic>();
        public static List<dynamic> Instances = new List<dynamic>();

        [DllExport]
        public static void LoadAssemblies()
        {
            Directory.CreateDirectory("Assemblies");

            string[] files = Directory.GetFiles("Assemblies", "*.dll", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                Assembly assembly = AppDomain.CurrentDomain.Load(File.ReadAllBytes(file));
                Assemblies.Add(new { assemblyName = assembly.GetName().Name, assembly });
            }
        }

        [DllExport]
        public static string GetAssemblyTypes(string assemblyName)
        {
            dynamic assembly = Assemblies.Find(item => item.assemblyName == assemblyName);
            Type[] assemblyTypes = assembly.assembly.GetTypes();
            return string.Join(",\n", assemblyTypes.Select(type => type.FullName));
        }

        [DllExport]
        public static void CreateInstance(string assemblyName, string instanceType, string instanceName, string parametersString = null)
        {
            dynamic assembly = Assemblies.Find(item => item.assemblyName == assemblyName);

            Type type = assembly.assembly.GetType(instanceType);
            dynamic instance = null;

            if (parametersString != null)
            {
                string[] parameters = parametersString.Split(',');
                List<object> parameterObjects = new List<object>();

                foreach (var parameter in parameters)
                {
                    string[] typeAndValue = parameter.Trim().Split(' ');
                    Type valueType = Type.GetType(typeAndValue[0]);
                    var value = Convert.ChangeType(typeAndValue[1], valueType);
                    parameterObjects.Add(value);
                }

                instance = Activator.CreateInstance(type, parameterObjects.ToArray());
            }
            else
            {
                instance = Activator.CreateInstance(type);
            }

            Instances.Add(new { instanceName, instance });
        }

        [DllExport]
        public static int GetFieldInt(string instanceName, string valueName)
        {
            return GetField<int>(instanceName, valueName);
        }

        [DllExport]
        public static string GetFieldString(string instanceName, string valueName)
        {
            return GetField<string>(instanceName, valueName);
        }

        [DllExport]
        public static float GetFieldFloat(string instanceName, string valueName)
        {
            return GetField<float>(instanceName, valueName);
        }

        [DllExport]
        public static bool GetFieldBool(string instanceName, string valueName)
        {
            return GetField<bool>(instanceName, valueName);
        }

        [DllExport]
        public static byte GetFieldByte(string instanceName, string valueName)
        {
            return GetField<byte>(instanceName, valueName);
        }

        [DllExport]
        public static short GetFieldShort(string instanceName, string valueName)
        {
            return GetField<short>(instanceName, valueName);
        }

        [DllExport]
        public static double GetFieldDouble(string instanceName, string valueName)
        {
            return GetField<double>(instanceName, valueName);
        }

        [DllExport]
        public static char GetFieldChar(string instanceName, string valueName)
        {
            return GetField<char>(instanceName, valueName);
        }

        [DllExport]
        public static long GetFieldLong(string instanceName, string valueName)
        {
            return GetField<long>(instanceName, valueName);
        }

        private static T GetField<T>(string instanceName, string valueName)
        {
            dynamic instance = Instances.Find(item => item.instanceName == instanceName);
            return (T)instance.instance.GetType().GetField(valueName).GetValue(instance.instance);
        }

        [DllExport]
        public static int GetPropertyInt(string instanceName, string propertyName)
        {
            return GetProperty<int>(instanceName, propertyName);
        }

        [DllExport]
        public static string GetPropertyString(string instanceName, string propertyName)
        {
            return GetProperty<string>(instanceName, propertyName);
        }

        [DllExport]
        public static float GetPropertyFloat(string instanceName, string propertyName)
        {
            return GetProperty<float>(instanceName, propertyName);
        }

        [DllExport]
        public static bool GetPropertyBool(string instanceName, string propertyName)
        {
            return GetProperty<bool>(instanceName, propertyName);
        }

        [DllExport]
        public static byte GetPropertyByte(string instanceName, string propertyName)
        {
            return GetProperty<byte>(instanceName, propertyName);
        }

        [DllExport]
        public static short GetPropertyShort(string instanceName, string propertyName)
        {
            return GetProperty<short>(instanceName, propertyName);
        }

        [DllExport]
        public static long GetPropertyLong(string instanceName, string propertyName)
        {
            return GetProperty<long>(instanceName, propertyName);
        }

        [DllExport]
        public static double GetPropertyDouble(string instanceName, string propertyName)
        {
            return GetProperty<double>(instanceName, propertyName);
        }

        [DllExport]
        public static char GetPropertyChar(string instanceName, string propertyName)
        {
            return GetProperty<char>(instanceName, propertyName);
        }

        private static T GetProperty<T>(string instanceName, string propertyName)
        {
            dynamic instance = Instances.Find(item => item.instanceName == instanceName);
            return (T)instance.instance.GetType().GetProperty(propertyName).GetValue(instance.instance, null);
        }

        [DllExport]
        public static void SetFieldInt(string instanceName, string valueName, int value)
        {
            SetField<int>(instanceName, valueName, value);
        }

        [DllExport]
        public static void SetFieldString(string instanceName, string valueName, string value)
        {
            SetField<string>(instanceName, valueName, value);
        }

        [DllExport]
        public static void SetFieldFloat(string instanceName, string valueName, float value)
        {
            SetField<float>(instanceName, valueName, value);
        }

        [DllExport]
        public static void SetFieldBool(string instanceName, string valueName, bool value)
        {
            SetField<bool>(instanceName, valueName, value);
        }

        [DllExport]
        public static void SetFieldByte(string instanceName, string valueName, byte value)
        {
            SetField<byte>(instanceName, valueName, value);
        }

        [DllExport]
        public static void SetFieldShort(string instanceName, string valueName, short value)
        {
            SetField<short>(instanceName, valueName, value);
        }

        [DllExport]
        public static void SetFieldLong(string instanceName, string valueName, long value)
        {
            SetField<long>(instanceName, valueName, value);
        }

        [DllExport]
        public static void SetFieldDouble(string instanceName, string valueName, double value)
        {
            SetField<double>(instanceName, valueName, value);
        }
        [DllExport]
        public static void SetFieldChar(string instanceName, string valueName, char value)
        {
            SetField<char>(instanceName, valueName, value);
        }

        private static void SetField<T>(string instanceName, string valueName, T value)
        {
            dynamic instance = Instances.Find(item => item.instanceName == instanceName);
            instance.instance.GetType().GetField(valueName).SetValue(instance.instance, value);
        }

        [DllExport]
        public static void SetPropertyInt(string instanceName, string propertyName, int value)
        {
            SetProperty<int>(instanceName, propertyName, value);
        }

        [DllExport]
        public static void SetPropertyString(string instanceName, string propertyName, string value)
        {
            SetProperty<string>(instanceName, propertyName, value);
        }

        [DllExport]
        public static void SetPropertyFloat(string instanceName, string propertyName, float value)
        {
            SetProperty<float>(instanceName, propertyName, value);
        }

        [DllExport]
        public static void SetPropertyBool(string instanceName, string propertyName, bool value)
        {
            SetProperty<bool>(instanceName, propertyName, value);
        }

        [DllExport]
        public static void SetPropertyByte(string instanceName, string propertyName, byte value)
        {
            SetProperty<byte>(instanceName, propertyName, value);
        }

        [DllExport]
        public static void SetPropertyShort(string instanceName, string propertyName, short value)
        {
            SetProperty<short>(instanceName, propertyName, value);
        }

        [DllExport]
        public static void SetPropertyLong(string instanceName, string propertyName, long value)
        {
            SetProperty<long>(instanceName, propertyName, value);
        }

        [DllExport]
        public static void SetPropertyDouble(string instanceName, string propertyName, double value)
        {
            SetProperty<double>(instanceName, propertyName, value);
        }
        [DllExport]
        public static void SetPropertyChar(string instanceName, string propertyName, char value)
        {
            SetProperty<char>(instanceName, propertyName, value);
        }

        private static void SetProperty<T>(string instanceName, string propertyName, T value)
        {
            dynamic instance = Instances.Find(item => item.instanceName == instanceName);
            instance.instance.GetType().GetProperty(propertyName).SetValue(instance.instance, value, null);
        }

        [DllExport]
        public static int InvokeInt(string instanceName, string methodName, string parametersString = null)
        {
            if (parametersString != null)
                return Invoke<int>(instanceName, methodName, parametersString);

            return Invoke<int>(instanceName, methodName);
        }

        [DllExport]
        public static byte InvokeByte(string instanceName, string methodName, string parametersString = null)
        {
            if (parametersString != null)
                return Invoke<byte>(instanceName, methodName, parametersString);

            return Invoke<byte>(instanceName, methodName);
        }

        [DllExport]
        public static int InvokeShort(string instanceName, string methodName, string parametersString = null)
        {
            if (parametersString != null)
                return Invoke<short>(instanceName, methodName, parametersString);

            return Invoke<short>(instanceName, methodName);
        }

        [DllExport]
        public static long InvokeLong(string instanceName, string methodName, string parametersString = null)
        {
            if (parametersString != null)
                return Invoke<long>(instanceName, methodName, parametersString);

            return Invoke<long>(instanceName, methodName);
        }

        [DllExport]
        public static float InvokeFloat(string instanceName, string methodName, string parametersString = null)
        {
            if (parametersString != null)
                return Invoke<float>(instanceName, methodName, parametersString);

            return Invoke<float>(instanceName, methodName);
        }

        [DllExport]
        public static double InvokeDouble(string instanceName, string methodName, string parametersString = null)
        {
            if (parametersString != null)
                return Invoke<double>(instanceName, methodName, parametersString);

            return Invoke<double>(instanceName, methodName);
        }

        [DllExport]
        public static char InvokeChar(string instanceName, string methodName, string parametersString = null)
        {
            if (parametersString != null)
                return Invoke<char>(instanceName, methodName, parametersString);

            return Invoke<char>(instanceName, methodName);
        }
        [DllExport]
        public static string InvokeString(string instanceName, string methodName, string parametersString = null)
        {
            if (parametersString != null)
                return Invoke<string>(instanceName, methodName, parametersString);

            return Invoke<string>(instanceName, methodName);
        }

        [DllExport]
        public static bool InvokeBool(string instanceName, string methodName, string parametersString = null)
        {
            if (parametersString != null)
                return Invoke<bool>(instanceName, methodName, parametersString);

            return Invoke<bool>(instanceName, methodName);
        }

        private static T Invoke<T>(string instanceName, string methodName, string parametersString)
        {
            string[] parameters = parametersString.Split(',');
            List<object> parameterObjects = new List<object>();

            foreach (var parameter in parameters)
            {
                string[] typeAndValue = parameter.Trim().Split(' ');
                Type type = Type.GetType(typeAndValue[0]);
                var value = Convert.ChangeType(typeAndValue[1], type);
                parameterObjects.Add(value);
            }

            return Invoke<T>(instanceName, methodName, parameterObjects.ToArray());
        }

        private static T Invoke<T>(string instanceName, string methodName, object[] parameters = null)
        {
            dynamic instance = Instances.Find(item => item.instanceName == instanceName);
            return (T)instance.instance.GetType().GetMethod(methodName).Invoke(instance.instance, parameters);
        }

    }
}
