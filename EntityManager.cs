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
            this.addEntity(new World(new Vector3D(0, 0, 0)));
            this.addEntity(new BarbarianWorker(0, -1, new Vector3D(0, 0, 0)));
            this.addEntity(new BarbarianWorker(6, 6, new Vector3D(1, 0, 0)));
        }

        public Entity getEntity(int p_Id)
        {
            var l_Entity = from l_Ent in m_Entities
                           where l_Ent.Id == p_Id
                           select l_Ent;
            return l_Entity.Single<Entity>();
        }

        public void addEntity(Entity p_Entity)
        {
            m_Entities.Add(p_Entity);
            p_Entity.Id = m_LastId++;
        }

        public void removeEntity(Entity p_Entity)
        {
            m_Entities.Remove(p_Entity);
        }

        public void update()
        {
            foreach (Entity l_Entity in m_Entities)
            {
                if (l_Entity.Life <= 0)
                {
                    this.removeEntity(l_Entity);
                    Game.Instance.PlayerManager.sendToAll(new DiePacket(l_Entity.Id));
                }
                l_Entity.update();
                if (l_Entity.Id != 0)
                {
                    Packet l_Packet = new EntityPacket(l_Entity.Id, l_Entity.Position.X, l_Entity.Position.Z, l_Entity.GetType().Name, l_Entity.Angle, l_Entity.Size.X, l_Entity.Size.Z);
                    foreach (Player l_Player in Game.Instance.PlayerManager.getConnectedPlayer())
                    {
                        ((EntityPacket)l_Packet).Hostile = l_Player.isHostile(l_Entity.Player);
                        Packet.SendAsync(l_Packet, l_Player);
                    }
                }
            }
        }

    }
}
