using System.Runtime.Serialization.Formatters.Binary;

AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);


Console.WriteLine("Hello, World!");

var formatter = new BinaryFormatter();
