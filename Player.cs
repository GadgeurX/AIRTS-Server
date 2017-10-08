using AiRTServer.Entities.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AiRTServer
{
    public enum HOSTILITY{ ALLY, ENEMY, MY, NEUTRAL};

    public class Player
    {
        Socket m_SocketClient;
        PlayerData m_Data;
        List<Entity> m_Selection;

        public Player(Socket p_Socket)
        {
            m_SocketClient = p_Socket;
            m_Data = null;
            m_Selection = new List<Entity>();
        }

        public Player(PlayerData p_Data)
        {
            m_Data = p_Data;
            m_Selection = new List<Entity>();
        }

        public HOSTILITY IsHostile(int p_Player)
        {
            if (p_Player == -1)
                return HOSTILITY.NEUTRAL;
            if (p_Player == this.Data.Id)
                return HOSTILITY.MY;
            return HOSTILITY.ENEMY;
        }

        public Socket SocketClient
        {
            get
            {
                return m_SocketClient;
            }

            set
            {
                lock (m_SocketClient)
                    m_SocketClient = value;
            }
        }

        public PlayerData Data
        {
            get
            {
                return m_Data;
            }

            set
            {
                if (m_Data != null)
                    lock(m_Data)
                        m_Data = value;
                else
                    m_Data = value;
            }
        }

        public List<Entity> Selection
        {
            get
            {
                return m_Selection;
            }

            set
            {
                if (m_Selection != null)
                    lock (m_Selection)
                        m_Selection = value;
                else
                    m_Selection = value;
            }
        }
    }
}
