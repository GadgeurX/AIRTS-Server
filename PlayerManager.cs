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

        public void sendToAll(Packet p_Packet)
        {
            foreach (Player l_Player in this.getConnectedPlayer())
            {
                Packet.SendAsync(p_Packet, l_Player);
            }
        }

        public void addPlayer(Player p_Player)
        {
            lock (m_Players)
            {
                m_Players.Add(p_Player);
            }
        }

        public void removePlayer(Player p_Player)
        {
            lock (m_Players)
            {
                m_Players.Remove(p_Player);
            }
        }

        public HashSet<Player> getPlayers()
        {
            return m_Players;
        }

        public Player getPlayer(Socket p_Socket)
        {
            foreach (Player l_Player in m_Players)
            {
                if (l_Player.SocketClient == p_Socket)
                    return l_Player;
            }
            return null;
        }

        public Player getPlayerById(int p_Id)
        {
            foreach (Player l_Player in getConnectedPlayer())
            {
                if (l_Player.Data.Id == p_Id)
                    return l_Player;
            }
            return null;
        }

        public List<Player> getConnectedPlayer()
        {
            return (from Player in m_Players
                    where (Player.Data) != null
                    select Player).ToList();
        }
    }
}
