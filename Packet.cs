using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AiRTServer
{
    public enum PacketType
    {
        OK,
        ERROR,
        LOGIN,
        ENTITY,
        SELECTION,
        ACTION,
        ANIM,
        DIE
    }

    [Serializable()]
    public class Packet
    {
        public PacketType Type { get; protected set; }

        public Packet(PacketType Type)
        {
            this.Type = Type;
        }

        public static void SendAsync(Packet paquet, Player p_Player)
        {
            Task.Run(() =>{
                lock (p_Player)
                {
                    try
                    {
                        NetworkStream stream = new NetworkStream(p_Player.SocketClient);
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(stream, paquet);
                        stream.Flush();
                    }
                    catch (Exception e)
                    {
                        if (p_Player.Data != null)
                            Console.WriteLine("[INFO] " + p_Player.Data.Login + " disconnected");
                        else
                            Console.WriteLine("[INFO] Client disconnected");
                        Game.Instance.PlayerManager.removePlayer(p_Player);
                    }
                }
            });
        }

        public static void SendAsync(Packet paquet, Socket p_Socket)
        {
            Task.Run(() => {
                lock (p_Socket)
                {
                    try
                    {
                        NetworkStream stream = new NetworkStream(p_Socket);
                        BinaryFormatter bf = new BinaryFormatter();
                        bf.Serialize(stream, paquet);
                        stream.Flush();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Connection lost");
                    }
                }
            });
        }

        public static Packet Receive(Socket p_Socket)
        {
            Packet p = null;
            try
            {
                NetworkStream stream = new NetworkStream(p_Socket);

                BinaryFormatter bf = new BinaryFormatter();
                bf.Binder = new VersionDeserializationBinder();

                p = (Packet)bf.Deserialize(stream);
            }
            catch (Exception)
            {
                return null;
            }
            return p;
        }
    }

    public sealed class VersionDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            String currentAssembly = Assembly.GetExecutingAssembly().FullName;

            // In this case we are always using the current assembly
            assemblyName = currentAssembly;

            // Get the type using the typeName and assemblyName
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
                typeName, assemblyName));

            return typeToDeserialize;
        }
    }
}
