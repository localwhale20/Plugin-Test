namespace Plugin_Test.PluginBase;

public interface IPlugin
{
    public void OnImport();
    public void OnCall();
} 
public static class Client
{
    public static Version Version { get; } = new(1,0,0);
}
public class Version
{
    public int MajorVersion { get; private set; } = 0;
    public int MinorVersion { get; private set; } = 0;
    public int FixVersion { get; private set; } = 0;

    public string VersionString { get; private set; } = "0.0.0";

    /// <summary>
    /// Initializes instance with version information
    /// </summary>
    /// <param name="major">Major version number</param>
    /// <param name="minor">Minor version number</param>
    /// <param name="fix">Fix version number</param>
    public Version(int major, int minor, int fix)
    {
        MajorVersion = major;
        MinorVersion = minor;
        FixVersion = fix;

        VersionString = $"{MajorVersion}.{MinorVersion}.{FixVersion}";
    }
}