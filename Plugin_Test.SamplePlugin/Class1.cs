using Plugin_Test.PluginBase;

namespace Plugin_Test.SamplePlugin;
public class Plugin : IPlugin
{
    void IPlugin.OnCall()
    {
        Console.WriteLine("Hello world!");
    }

    void IPlugin.OnImport()
    {
        Console.WriteLine("This is just sample");
    }
}
