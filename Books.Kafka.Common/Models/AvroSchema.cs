namespace Books.Common
{
    public class AvroSchema<T>
    {
        public string type => "record";
        public string name => typeof(T).Name;
        public Field[] fields =>
            typeof(T)
                .GetProperties()
                .Select(p => new Field
                {
                    name = p.Name,
                    type = MapCSharpTypeToAvroType(p.PropertyType),
                })
                .ToArray();

        private string MapCSharpTypeToAvroType(Type type)
        {
            if (type == typeof(string))
                return "string";
            if (type == typeof(int))
                return "int";
            if (type == typeof(long))
                return "long";
            if (type == typeof(float))
                return "float";
            if (type == typeof(double))
                return "double";
            if (type == typeof(bool))
                return "boolean";
            if (type == typeof(DateTime))
                return "long";
            if (type == typeof(Guid))
                return "string";

            return "string";
        }

        private Type MapAvroTypeToCSharp(string avroType)
        {
            return avroType switch
            {
                "string" => typeof(string),
                "int" => typeof(int),
                "long" => typeof(long),
                "float" => typeof(float),
                "double" => typeof(double),
                "boolean" => typeof(bool),
                _ => typeof(string),
            };
        }

        public class Field
        {
            public string name { get; set; }
            public string type { get; set; }
        }
    }
}
