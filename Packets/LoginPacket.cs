using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer.Packets
{
    [Serializable()]
    class LoginPacket : Packet
    {
        String m_Email;
        String m_Mdp;

        public LoginPacket(String p_Email, String p_Mdp) : base(PacketType.LOGIN)
        {
            m_Email = p_Email;
            m_Mdp = p_Mdp;
        }

        public string Email
        {
            get
            {
                return m_Email;
            }

            set
            {
                m_Email = value;
            }
        }

        public string Mdp
        {
            get
            {
                return m_Mdp;
            }

            set
            {
                m_Mdp = value;
            }
        }
    }
}
