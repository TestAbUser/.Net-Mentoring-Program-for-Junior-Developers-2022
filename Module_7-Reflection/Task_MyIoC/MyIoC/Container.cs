using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;

namespace MyIoC
{
    public class Container
    {
        private readonly List<Type> typesCatalog = new List<Type>();
        string[] files;

        /// <summary>
        /// Extracts certain types from a given assembly and adds them to the type catalog.
        /// </summary>
        /// <param name="assembly"></param>
        public void AddAssembly(Assembly assembly)
        {
            // get paths to all the libraries of the assembly
            files = Directory.GetFiles(Path.GetDirectoryName(assembly.Location), "*.dll", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                // load an assembly into the application domain
                var asm = Assembly.LoadFrom(file);

                // get a list of types with certain attributes
                List<Type> foundTypes = asm.GetTypes()
                    .Where(x => x.IsClass && (x.GetCustomAttributes<ExportAttribute>() != null
                || x.GetCustomAttributes<ImportAttribute>() != null
                || x.GetCustomAttributes<ImportConstructorAttribute>() != null)).ToList();
                if (foundTypes.Count != 0)
                {
                    typesCatalog.AddRange(foundTypes);
                }
            }
        }

        // adds a type to the types catalog if the type or its properties are marked with certain attributes 
        public void AddType(Type type)
        {
            IEnumerable<PropertyInfo> propertyAttrs = type.GetProperties()
                .Where(x => x.GetCustomAttributes<ImportAttribute>().Count() != 0);
            IEnumerable<object> attrs = type.GetCustomAttributes(true)
                .Where(a => a is ImportConstructorAttribute || a is ImportAttribute || a is ExportAttribute);
            if (type.IsClass && (attrs.Count() != 0) || propertyAttrs.Count() != 0)
            {
                typesCatalog.Add(type);
            }
        }


        /// <summary>
        /// Adds a type to the types catalog if the type is marked with certain attributes and 
        /// is derived from the base type.
        /// </summary>
        /// <param name="type">Type to add to the catalog.</param>
        /// <param name="baseType">Base type from which the current type derives. </param>
        public void AddType(Type type, Type baseType)
        {
            IEnumerable<object> attrs = type.GetCustomAttributes(true)
                .Where(a => a is ImportConstructorAttribute || a is ImportAttribute || a is ExportAttribute);
            if (type.IsClass && attrs.Count() != 0 && baseType.IsAssignableFrom(type))
            {
                typesCatalog.Add(type);
            }
        }

        /// <summary>
        /// Creates an instance of a given type.
        /// </summary>
        /// <param name="type"> type variable</param>
        /// <returns> an object</returns>
        public object CreateInstance(Type type)
        {
            object classInstance = default;
            ConstructorInfo defaultCtor = default;
            ParameterInfo[] defaultParams;
            object[] parameters = default;

            // for each type existing in the catalog list
            foreach (var exportType in typesCatalog)
            {
                // if the type either exists in the catalog or is the parent type for a type in the catalog
                if (exportType == type || type.IsAssignableFrom(exportType))
                {
                    defaultCtor = exportType.GetConstructors()[0]; // Get the type's first constructor.
                    defaultParams = defaultCtor.GetParameters(); // Get parameters for the constructor.
                    parameters = defaultParams.Select(param =>
                    CreateInstance(param.ParameterType)).ToArray(); // Initialize the parameters.
                    classInstance = defaultCtor.Invoke(parameters); // Create an instance of the type.
                    PropertyInfo[] props = type.GetProperties();

                    foreach (PropertyInfo prop in props)
                    {
                        object[] propAttrs = prop.GetCustomAttributes(true);
                        foreach (var attr in propAttrs)
                        {
                            if (attr is ImportAttribute)
                            {
                                // initialize each property with ImportAttribute
                                var initializedProp = CreateInstance(prop.PropertyType);
                                prop.SetValue(classInstance, initializedProp);
                            }
                        }
                    }
                }
            }
            return classInstance;
        }

        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }

        public void Sample()
        {
            var container = new Container();
            container.AddAssembly(Assembly.GetExecutingAssembly());

            container.AddType(typeof(CustomerBLL));
            container.AddType(typeof(Logger));
            container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

            var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
            var customerBLL2 = container.CreateInstance<CustomerBLL2>();
        }
    }
}
