using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AnimalEditor.Model.Animals;

namespace AnimalEditor.Model
{
    public static class ReflexionAnalyzer
    {
        private static bool IsRequiredSubclass(Type type, List<Type> animalSuperClasses)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return animalSuperClasses.Any(currentAnimalSuperclass => type.BaseType == currentAnimalSuperclass);
        }

        private static Type GetAnimalClass()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type == typeof(Animal))
                        return type;
                }
            }
            throw new Exception("There is no Animal class.");
        }

        private static List<Type> GetAnimalSuperClasses(Type animalType)
        {
            var animalSuperclasses = new List<Type>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                animalSuperclasses.AddRange(assembly.GetTypes().Where(type => type.IsAbstract && type.BaseType == animalType));
            }
            return animalSuperclasses;
        }
        public static List<Type> GetConcreteClasses()
        {
            var concreteClasses = new List<Type>();
            var animalSuperClasses = GetAnimalSuperClasses(GetAnimalClass());
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!type.IsAbstract && IsRequiredSubclass(type, animalSuperClasses))
                        concreteClasses.Add(type);
                }
            }

            return concreteClasses;
        }

        public static List<string> GetConcreteAnimalNames()
        {
            var types = GetConcreteClasses();
            var names = from type in types
                        select type.Name;
            return names.ToList();
        }

        public static Type GetTypeByString(string name)
        {
            return Type.GetType("AnimalEditor.Model.Animals." + name);
        }

    }
}
