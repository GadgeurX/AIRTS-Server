using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer.Packets
{
    [Serializable]
    public class AnimPacket : Packet
    {
        int m_Id;
        string m_Anim;
        double m_Speed;
        public AnimPacket(int p_Id, string p_Anim, double p_Speed) : base(PacketType.ANIM)
        {
            m_Anim = p_Anim;
            m_Speed = p_Speed;
            m_Id = p_Id;
        }

        public string Anim
        {
            get
            {
                return m_Anim;
            }

            set
            {
                m_Anim = value;
            }
        }

        public double Speed
        {
            get
            {
                return m_Speed;
            }

            set
            {
                m_Speed = value;
            }
        }

        public int Id
        {
            get
            {
                return m_Id;
            }

            set
            {
                m_Id = value;
            }
        }
    }
}
