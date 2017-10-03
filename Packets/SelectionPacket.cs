using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer.Packets
{
    [Serializable()]
    public class SelectionPacket : Packet
    {

        List<int> m_Selection;
        public SelectionPacket(List<int> p_Selection) : base(PacketType.SELECTION)
        {
            m_Selection = p_Selection;
        }

        public List<int> Selection
        {
            get
            {
                return m_Selection;
            }

            set
            {
                m_Selection = value;
            }
        }
    }
}
