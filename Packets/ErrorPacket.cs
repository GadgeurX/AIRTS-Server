using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer.Packets
{
    [Serializable()]
    public class ErrorPacket : Packet
    {
        public ErrorPacket() : base(PacketType.ERROR)
        {
        }
    }
}
