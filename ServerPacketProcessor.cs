using AiRTServer.Entities;
using AiRTServer.Entities.Units;
using AiRTServer.Packets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AiRTServer
{
    public class ServerPacketProcessor : PacketProcessor
    {
        Dictionary<PacketType, ProcessFunction> m_Processor;

        public Dictionary<PacketType, ProcessFunction> Processor(){
                return m_Processor;
        }

        public ServerPacketProcessor()
        {
            m_Processor = new Dictionary<PacketType, ProcessFunction>();

            m_Processor[PacketType.LOGIN] = loginProcess;
            m_Processor[PacketType.SELECTION] = selectionProcess;
            m_Processor[PacketType.ACTION] = actionProcess;
        }

        private void loginProcess(Packet p_Packet, Player p_Player)
        {
            LoginPacket l_Packet = (LoginPacket)p_Packet;
            if (Game.Instance.DataManager.LoginPlayer(l_Packet.Email, l_Packet.Mdp))
            {
                p_Player.Data = Game.Instance.DataManager.LoadPlayer(l_Packet.Email, l_Packet.Mdp);
                Console.WriteLine("[INFO] " + p_Player.Data.Login + " is connected");
                Game.Instance.EntityManager.addEntity(new BarbarianWorker(0, p_Player.Data.Id, new Vector3D(0,0,0)));
                Packet.SendAsync(new OkPacket(), p_Player);
            }
            else
            {
                Console.WriteLine("[Warning] Login Error");
                Packet.SendAsync(new ErrorPacket(), p_Player);
            }
        }

        private void selectionProcess(Packet p_Packet, Player p_Player)
        {
            SelectionPacket l_Packet = (SelectionPacket)p_Packet;
            p_Player.Selection.Clear();
            Console.WriteLine("[INFO] " + p_Player.Data.Login + " selection:");
            foreach (int l_Id in l_Packet.Selection)
            {
                Entity m_Entity = Game.Instance.EntityManager.getEntity(l_Id);
                Console.WriteLine("  -  " + m_Entity.Id);
                if (p_Player.isHostile(m_Entity.Player) == HOSTILITY.MY)
                    p_Player.Selection.Add(m_Entity);
            }
        }

        private void actionProcess(Packet p_Packet, Player p_Player)
        {
            ActionPacket l_Packet = (ActionPacket)p_Packet;
            foreach (Entity l_Entity in p_Player.Selection)
            {
                l_Entity.Action = l_Packet.Action;
                if (l_Packet.Id == 0)
                    l_Entity.Cible = new WorldPosition(new Vector3D(l_Packet.Pos_X, 0, l_Packet.Pos_Y));
                else
                    l_Entity.Cible = Game.Instance.EntityManager.getEntity(l_Packet.Id);
            }
        }
    }
}
