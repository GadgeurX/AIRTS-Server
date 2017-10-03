using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer.Packets
{
    [Serializable()]
    public class DiePacket : Packet
    {
        int m_Id;

        public DiePacket(int p_Id) : base(PacketType.DIE)
        {
            m_Id = p_Id;
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
