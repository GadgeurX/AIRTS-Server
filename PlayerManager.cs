using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer
{
    public class PlayerManager
    {
        HashSet<Player> m_Players;

        public PlayerManager()
        {
            m_Players = new HashSet<Player>();
        }

        public void SendToAll(Packet p_Packet)
        {
            foreach (Player l_Player in this.GetConnectedPlayer())
            {
                Packet.SendAsync(p_Packet, l_Player);
            }
        }

        public void AddPlayer(Player p_Player)
        {
            lock (m_Players)
            {
                m_Players.Add(p_Player);
            }
        }

        public void RemovePlayer(Player p_Player)
        {
            lock (m_Players)
            {
                m_Players.Remove(p_Player);
            }
        }

        public HashSet<Player> GetPlayers()
        {
            return m_Players;
        }

        public Player GetPlayer(Socket p_Socket)
        {
            foreach (Player l_Player in m_Players)
            {
                if (l_Player.SocketClient == p_Socket)
                    return l_Player;
            }
            return null;
        }

        public Player GetPlayerById(int p_Id)
        {
            foreach (Player l_Player in GetConnectedPlayer())
            {
                if (l_Player.Data.Id == p_Id)
                    return l_Player;
            }
            return null;
        }

        public List<Player> GetConnectedPlayer()
        {
            return (from Player in m_Players
                    where (Player.Data) != null
                    select Player).ToList();
        }
    }
}
