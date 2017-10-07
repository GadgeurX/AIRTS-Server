using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer
{
    class NetManager
    {
        Socket m_SocketServer;
        List<Socket> m_ReadClients;
        PlayerManager m_PlayerManager;
        PacketProcessor m_PacketProcessor;

        public NetManager(PacketProcessor p_PacketProcessor)
        {
            m_PlayerManager = Game.Instance.PlayerManager;
            m_PacketProcessor = p_PacketProcessor;
        }

        public void init(int p_Port)
        {
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, p_Port);

            m_SocketServer = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            m_SocketServer.Bind(localEndPoint);
            m_SocketServer.Listen(p_Port);
            Console.WriteLine("[INFO] Network listen to " + localEndPoint.Address.ToString() + ":" + p_Port);
        }

        public void update()
        {
            m_ReadClients = new List<Socket>();
            m_ReadClients.Add(m_SocketServer);
            foreach (Player l_Player in m_PlayerManager.getPlayers())
            {
                m_ReadClients.Add(l_Player.SocketClient);
            }

            Socket.Select(m_ReadClients, null, null, 5000000);

            foreach (Socket l_Socket in m_ReadClients)
            {
                if (l_Socket == m_SocketServer)
                    newConnection();
                else
                {
                    Packet l_Packet = Packet.Receive(l_Socket);
                    if (l_Packet == null)
                    {
                        if (m_PlayerManager.getPlayer(l_Socket).Data != null)
                            Console.WriteLine("[INFO] " + m_PlayerManager.getPlayer(l_Socket).Data.Login + " disconnected");
                        else
                            Console.WriteLine("[INFO] Client disconnected");
                        m_PlayerManager.removePlayer(m_PlayerManager.getPlayer(l_Socket));
                    }
                    else
                        Task.Run(() => { m_PacketProcessor.Processor()[l_Packet.Type](l_Packet, m_PlayerManager.getPlayer(l_Socket)); });
                }
            }
        }

        private void newConnection()
        {
            Console.WriteLine("[INFO] New client connection");
            m_PlayerManager.addPlayer(new Player(m_SocketServer.Accept()));
        }

    }
}
