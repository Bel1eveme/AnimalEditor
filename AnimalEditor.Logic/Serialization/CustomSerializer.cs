using System.Reflection;
using System.Text;
using System.Windows.Forms.VisualStyles;
using AnimalEditor.Model.Animals;
using AnimalEditor.Model.Serialization.SerializableAnimals;

namespace AnimalEditor.Model.Serialization
{
    internal class CustomSerializer : ISerializer
    {
        private readonly char _specialChar = '\'';

        private readonly string _specialCharString = "'";
        public MemoryStream Serialize(List<Animal> animals)
        {
            var serializableList = SerializeManager.ListToSerializableList(animals);
            var animalsString = new StringBuilder();

            animalsString.Append('{');
            foreach (var serializableObject in serializableList)
            {
                SerializeObject(serializableObject, animalsString);
            }
            animalsString.Append('}');

            return new MemoryStream(Encoding.UTF8.GetBytes(animalsString.ToString()));
        }

        public void Serialize(List<Animal> animals, string fileName)
        {
            var serializableList = SerializeManager.ListToSerializableList(animals);
            var animalsString = new StringBuilder();

            animalsString.Append('{');
            foreach (var serializableObject in serializableList)
            {
                SerializeObject(serializableObject, animalsString);
            }
            animalsString.Append('}');

            File.WriteAllText(fileName, animalsString.ToString());
        }

        private void SerializeObject(object obj, StringBuilder stringBuilder)
        {
            stringBuilder.Append(obj.GetType().Name + '[');

            foreach (var property in obj.GetType().GetProperties())
            {
                var value = property.GetValue(obj);

                if (value is null) continue;

                stringBuilder.Append("'" + property.Name + "':");
                var type = property.PropertyType;
                if (value is string stringValue)
                {
                    stringBuilder.Append(_specialChar + stringValue.Replace(_specialCharString, 
                                        _specialCharString + _specialCharString) + _specialChar);
                }
                if (type.IsClass)
                {
                    SerializeObject(value, stringBuilder);
                }
                else
                {
                    stringBuilder.Append(_specialCharString + value + _specialCharString);
                }

                stringBuilder.Append(';');
            }

            stringBuilder.Append(']');
        }

        public List<Animal> Deserialize(string fileName)
        {
            var serializableAnimals = new List<SerializableAnimal>();
            var reader = new StreamReader(fileName);

            if (reader.Read() != '{')
            {
                throw new Exception("Wrong file format.");
            }

            while (reader.Peek() != '}')
            {
                serializableAnimals.Add((SerializableAnimal)DeserializeObject(reader));
            }
            reader.Close();

            return SerializeManager.SerializableListToList(serializableAnimals);
        }

        public List<Animal> Deserialize(MemoryStream serializedStream)
        {
            var serializableAnimals = new List<SerializableAnimal>();
            var reader = new StreamReader(serializedStream);

            if (reader.Read() != '{')
            {
                throw new Exception("Wrong file format.");
            }

            while (reader.Peek() != '}')
            {
                serializableAnimals.Add((SerializableAnimal)DeserializeObject(reader));
            }
            reader.Close();

            return SerializeManager.SerializableListToList(serializableAnimals);
        }

        private object DeserializeObject(StreamReader reader)
        {
            var className = string.Empty;
            while (reader.Peek() != '[')
            {
                if (reader.Peek() == -1)
                {
                    throw new Exception("Wrong file format.");
                }
                className += (char)reader.Read();
            }

            var type = GetTypeByString(className);
            var instance = GetNewInstanceByType(type);

            if (reader.Read() != '[')
            {
                throw new Exception("Wrong file format.");
            }
            while (reader.Peek() != ']')
            {
                var propertyName = ReadValue(reader);
                var property = GetProperty(type, propertyName);

                if (reader.Read() != ':')
                {
                    throw new Exception("Wrong file format.");
                }

                object value;
                var nextChar = (char)reader.Peek();
                if (nextChar == _specialChar)
                {
                    value = ReadValue(reader);
                }
                else
                {
                    value = DeserializeObject(reader);
                }

                SetProperty(instance, property, value);

                if (reader.Read() != ';')
                {
                    throw new Exception("Wrong file format.");
                }
            }
            reader.Read();
            return instance;
        }
        private string ReadValue(StreamReader reader)
        {
            var valuesString = string.Empty;
            int readChar;
            reader.Read();

            while ((readChar = reader.Read()) != _specialChar || reader.Peek() == _specialChar)
            {
                if (readChar == -1)
                {
                    throw new Exception("Wrong file format.");
                }
                if (readChar == _specialChar)
                {
                    reader.Read();
                }
                valuesString += (char)readChar;
            }
            return valuesString;
        }

        private void SetProperty(object obj, PropertyInfo property, object value)
        {
            var type = property.PropertyType;

            if (type.IsClass)
            {
                property.SetValue(obj, value);
                return;
            }

            if (value is not string valueString)
            {
                throw new Exception("Wrong property type.");
            }

            if (type == typeof(string))
            {
                property.SetValue(obj, valueString);
                return;
            }
            if (type == typeof(int))
            {
                property.SetValue(obj, Convert.ToInt32(valueString));
                return;
            }
            if (type == typeof(bool))
            {
                property.SetValue(obj, Convert.ToBoolean(valueString));
                return;
            }
            if (type == typeof(DateTime))
            {
                property.SetValue(obj, DateTime.Parse(valueString));
                return;
            }
            if (type.IsEnum)
            {
                property.SetValue(obj, Enum.Parse(type, valueString));
                return;
            }
            
            throw new Exception("Wrong property type.");
        }

        public static Type GetTypeByString(string name)
        {
            var type = Type.GetType(typeof(CustomSerializer).Namespace + ".SerializableAnimals." + name);

            if (type == null)
            {
                throw new Exception("Wrong file format.");
            }
            return type;
        }

        public static object GetNewInstanceByType(Type type)
        {
            var instance = Activator.CreateInstance(type);

            if (instance == null)
            {
                throw new Exception("Wrong file format.");
            }
            return instance;
        }

        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
            var property = type.GetProperty(propertyName);

            if (property == null)
            {
                throw new Exception("Wrong file format.");
            }
            return property;
        }

        public string GetExtension()
        {
            return ".an";
        }
    }
}
