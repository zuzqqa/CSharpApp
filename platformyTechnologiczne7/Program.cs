using System.Runtime.Serialization.Formatters.Binary;

AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", true);

// command line arguments

var arguments = Environment.GetCommandLineArgs();

Console.WriteLine("GetCommandLineArgs: {0}", string.Join(", ", arguments));

/*var formatter = new BinaryFormatter();*/
