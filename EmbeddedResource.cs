using System;
using System.Reflection;

namespace BLRPC
{
    public static class EmbeddedResource
    {
        public static byte[] GetResourceBytes(String filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var resource in assembly.GetManifestResourceNames())
            {
                if (resource.Contains(filename))
                {
                    using (var resFilestream = assembly.GetManifestResourceStream(resource))
                    {
                        if (resFilestream == null) return null;
                        var ba = new byte[resFilestream.Length];
                        resFilestream.Read(ba, 0, ba.Length);
                        return ba;
                    }
                }
            }
            return null;
        }
    }
}