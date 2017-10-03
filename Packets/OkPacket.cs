using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer.Packets
{
    [Serializable()]
    class OkPacket : Packet
    {
        public OkPacket() : base(PacketType.OK)
        {
        }
    }
}
