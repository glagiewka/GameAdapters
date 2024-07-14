using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using GameAdapters.Adapters.AssettoCorsa.Models;

namespace GameAdapters.Adapters.AssettoCorsa;

[SupportedOSPlatform("windows")]
public abstract class SharedMemoryReader
{
    public static Static? ReadStatic()
    {
        return ReadFile<Static>("Local\\acpmf_static");
    }

    public static Physics? ReadPhysics()
    {
        return ReadFile<Physics>("Local\\acpmf_physics");
    }

    private static T? ReadFile<T>(string fileName) where T : struct
    {
        try
        {
            using var file = MemoryMappedFile.OpenExisting(fileName);
            using var stream = file.CreateViewStream();
            using var reader = new BinaryReader(stream);
            var size = Marshal.SizeOf(typeof(T));
            var bytes = reader.ReadBytes(size);
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var data = (T?)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            return data;
        }
        catch (FileNotFoundException)
        {
            // Silently fail when game isn't running 
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}