using System.Runtime.InteropServices;

namespace GameAdapters.Adapters.AssettoCorsa.Models;

[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
[Serializable]
public struct Static
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public String SMVersion;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
    public String ACVersion;
}