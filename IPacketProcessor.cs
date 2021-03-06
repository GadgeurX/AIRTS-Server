﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer
{
    public delegate void ProcessFunction(Packet p_Packet, Player p_Player);
    public interface IPacketProcessor
    {
        Dictionary<PacketType, ProcessFunction> Processor();
    }
}
