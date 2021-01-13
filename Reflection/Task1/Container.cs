using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Task1.CustomExceptions;
using Task1.DoNotChange;

namespace Task1
{
    public class Container
    {
        private readonly IDictionary<Type, Type> registredTypes;

        public Container()
        {
            this.registredTypes = new Dictionary<Type, Type>();
        }
        public void AddAssembly(Assembly assembly)
        {
            var types = assembly.ExportedTypes;
            foreach (var type in types)
            {
                var constructorImportAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();
                var hasImportedProperties = GetPropertiesRequiredInitialization(type).Any();

                if (constructorImportAttribute != null || hasImportedProperties)
                {
                    registredTypes.Add(type, type);
                }

                var exportAttributes = type.GetCustomAttributes<ExportAttribute>();
                foreach (var exportAttribute in exportAttributes)
                {
                    registredTypes.Add(exportAttribute.Contract ?? type, type);
                }
            }
        }

        public void AddType(Type type)
        {
            registredTypes.Add(type, type);
        }

        public void AddType(Type type, Type baseType)
        {
            registredTypes.Add(baseType, type);
        }

        public T Get<T>()
        {
            var type = typeof(T);
            var instance = (T)CreateInstance(type);
            return instance;
        }

        private object CreateInstance(Type type)
        {
            if (!registredTypes.ContainsKey(type))
            {
                throw new DIContainerException($"Container is not consists {type.FullName}.");
            }

            var dependentType = registredTypes[type];
            var constructor = GetFirstPublicConstructor(dependentType);
            var instance = CreateInstanceFromConstructor(dependentType, constructor);

            if (dependentType.GetCustomAttribute<ImportConstructorAttribute>() != null)
            {
                return instance;
            }

            ResolveProperties(dependentType, instance);
            return instance;
        }

        private ConstructorInfo GetFirstPublicConstructor(Type dependedType)
        {
            var constructors = dependedType.GetConstructors();
            if (constructors.Length == 0)
            {
                throw new DIContainerException($"There are no public constructor for type {dependedType.FullName}");
            }

            return constructors.First();
        }

        private object CreateInstanceFromConstructor(Type type, ConstructorInfo constructor)
        {
            var constructorParameters = constructor.GetParameters();
            var parametersInstances = new List<object>();

            foreach (var parameter in constructorParameters)
            {
                var parameterInstance = CreateInstance(parameter.ParameterType);
                parametersInstances.Add(parameterInstance);
            }

            object instance = Activator.CreateInstance(type, parametersInstances.ToArray());
            return instance;
        }

        private void ResolveProperties(Type type, object instance)
        {
            var propertiesToResolve = GetPropertiesRequiredInitialization(type);
            foreach (var property in propertiesToResolve)
            {
                var resolvedProperty = CreateInstance(property.PropertyType);
                property.SetValue(instance, resolvedProperty);
            }
        }

        private static IEnumerable<PropertyInfo> GetPropertiesRequiredInitialization(Type type)
        {
            var allProperties = type.GetProperties();
            var propertiesNeedToBeInitialized = allProperties.Where(p => p.GetCustomAttribute<ImportAttribute>() != null);
            return propertiesNeedToBeInitialized;
        }
    }
}