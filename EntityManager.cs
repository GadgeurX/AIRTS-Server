using AiRTServer.Entities;
using AiRTServer.Entities.Units;
using AiRTServer.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AiRTServer
{
    public class EntityManager
    {
        List<Entity> m_Entities;
        int m_LastId = 0;

        public EntityManager()
        {
            m_Entities = new List<Entity>();
            this.AddEntity(new BarbarianWorker(0, -1, new Vector3D(0, 0, 0)));
            this.AddEntity(new BarbarianWorker(6, 6, new Vector3D(1, 0, 0)));
        }

        public Entity GetEntity(int p_Id)
        {
            var l_Entity = from l_Ent in m_Entities
                           where l_Ent.Id == p_Id
                           select l_Ent;
            return l_Entity.Single<Entity>();
        }

        public void AddEntity(Entity p_Entity)
        {
            m_Entities.Add(p_Entity);
            p_Entity.Id = m_LastId++;
        }

        public void RemoveEntity(Entity p_Entity)
        {
            m_Entities.Remove(p_Entity);
        }

        public void Update()
        {
            for (int i = m_Entities.Count - 1; i >= 0; i--)
            {
                Entity l_Entity = m_Entities[i];
                if (l_Entity.Life <= 0)
                {
                    this.RemoveEntity(l_Entity);
                    Game.Instance.PlayerManager.SendToAll(new DiePacket(l_Entity.Id));
                }
                l_Entity.update();
                if (l_Entity.Id != 0)
                {
                    Packet l_Packet = new EntityPacket(l_Entity);
                    foreach (Player l_Player in Game.Instance.PlayerManager.GetConnectedPlayer())
                    {
                        ((EntityPacket)l_Packet).Hostile = l_Player.IsHostile(l_Entity.Player);
                        Packet.SendAsync(l_Packet, l_Player);
                    }
                }
            }
        }

    }
}
