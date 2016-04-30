using UnityEngine;
using ExitGames.Client.Photon;
using System.IO;

public class PhotonCustomTypes : MonoBehaviour
{
    void Awake()
    {
        PhotonPeer.RegisterType(typeof(Color), (byte)'C', SerializeColor, DeserializeColor);
    }
    
    private static readonly byte[] memColor = new byte[4 * 4];
    private static short SerializeColor(MemoryStream outStream, object customobject)
    {
        Color color = (Color)customobject;
        lock (memColor)
        {
            byte[] bytes = memColor;
            int index = 0;
            Protocol.Serialize(color.r, bytes, ref index);
            Protocol.Serialize(color.g, bytes, ref index);
            Protocol.Serialize(color.b, bytes, ref index);
            Protocol.Serialize(color.a, bytes, ref index);
            outStream.Write(bytes, 0, 4 * 4);
        }

        return 4 * 4;
    }

    private static object DeserializeColor(MemoryStream inStream, short length)
    {
        Color color = new Color();
        lock (memColor)
        {
            inStream.Read(memColor, 0, 4 * 4);
            int index = 0;
            Protocol.Deserialize(out color.r, memColor, ref index);
            Protocol.Deserialize(out color.g, memColor, ref index);
            Protocol.Deserialize(out color.b, memColor, ref index);
            Protocol.Deserialize(out color.a, memColor, ref index);
        }

        return color;
    }
}
