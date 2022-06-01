using System.Reflection;
using Plugin_Test.PluginBase;

Console.WriteLine("Welcome to Plugin Test!\nWait until application scan for plugins...");

if (!Directory.Exists("Plugins"))
    Directory.CreateDirectory("Plugins");

string[] files = Directory.GetFiles("Plugins", "*.dll");

if (files.Length == 0)
{
    Console.WriteLine("No plugins installed! If you want to create own plugin read \"plugin_doc.txt\"");
    Console.ReadKey();
    Environment.Exit(0);
}


Dictionary<string, Type> plugins = new();
Dictionary<int, string> pluginsIndexes = new();

for (int i = 0; i < files.Length; i++)
{
    string s = files[i];
    try
    {
        

        Assembly plugAssembly = Assembly.LoadFile(Path.GetFullPath(s));

        Console.WriteLine($"Founded plugin \"{plugAssembly.GetName().Name}\".\n" +
            $"Scanning its classes...");

        Type[] classes = plugAssembly.GetExportedTypes()
            .Where(t => t.IsAssignableTo(typeof(IPlugin)))
            .ToArray();

        if (classes.Length > 0)
        {
            foreach (Type t in classes)
            {
                plugins.Add(plugAssembly.GetName()!.Name, t);
                pluginsIndexes.Add(i, plugins.Keys.ToArray()[i]);
            }
            

            Console.WriteLine(
                $"Imported \"{plugAssembly.GetName().Name}\" which have {classes.Length} plugin classes");
        }
        else
        {
            Console.WriteLine(
                "Seems, like this plugin, is not have class, which implements IPlugin interface.\n" +
                "Please check documentation how to code plugin (if you code it)");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"Cant import \"{Path.GetFileNameWithoutExtension(s)}\":\n" +
            $"{ex.Message}");
    }
}
Console.WriteLine($"Imported {plugins.Count} plugins");
while (true) 
{

    for (int i = 1; i < plugins.Count + 1; i++)
    {
        Console.WriteLine($"{i}: {plugins.Keys.ToArray()[i - 1]}");
    }
    Console.Write("Enter plugin number to execute it: ");
    string input = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int index))
    {
        if (index >= plugins.Count + 1 || index <= 0)
        {
            Console.WriteLine("Invalid plugin number");
            continue;
        }
        else
        {
            Console.Clear();
            IPlugin? plug = (IPlugin?) Activator.CreateInstance(plugins[pluginsIndexes[index - 1]]);
            plug?.OnCall();
            Console.WriteLine();
        }
    }
    else
    {
        Console.WriteLine("Entered number is not valid or it is empty");
        continue;
    }
}
