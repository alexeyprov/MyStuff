using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany(ThisAssembly.Company)]
[assembly: AssemblyProduct(ThisAssembly.Product)]

[assembly: AssemblyTitle(ThisAssembly.Title)]
[assembly: AssemblyDescription(ThisAssembly.Description)]

[assembly: AssemblyCopyright(ThisAssembly.Copyright)]
[assembly: AssemblyTrademark(ThisAssembly.Trademark)]

[assembly: AssemblyVersion(ThisAssembly.Version)]

[assembly: AssemblyKeyName("")]
[assembly: Guid("E9E19A4A-FC07-41eb-8249-8807C2751BAA")]

sealed class ThisAssembly
{
    ThisAssembly(){}

    public const string Company="未";
    public const string Product="WebBrowserControl";
    public const string Title="WebBrowserControl";
    public const string Description="Internet Explorer WebBrowserControl (wrapper).";
    public const string Copyright="O. Mihailik, december 2003";
    public const string Trademark="WebBrowserControl";
    public const string Version="0.5";
}
