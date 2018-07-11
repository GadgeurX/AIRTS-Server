using AiRTServer.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AiRTServer
{
    public abstract class Entity
    {
        public enum EntityAction
        {
            MOVE,
            ATTACK,
            DEFAULT,
            IDLE
        }

        int m_Id;
        string m_ObjectName;
        double m_BuildTime, m_Life, m_MaxLife;
        private int m_Player;
        Vector3D m_Position;
        int m_Range;
        double m_Angle;
        Vector3D m_Size;
        double m_BaseDamage;
        double m_BaseArmor;
        Resource m_Cost, m_SellValue;
        EntityAction m_Action;
        Entity m_Cible;
        public delegate void ActionFunction(int p_AlphaMs, Entity p_Cible);
        Dictionary<EntityAction, ActionFunction> m_ActionDefinition;
        double m_Ki;
        Stopwatch m_Watch;

        public Entity(int p_Player, Vector3D p_Position)
        {
            m_Player = p_Player;
            m_Position = p_Position;
            m_Angle = 180.0;
            m_Watch = new Stopwatch();
            m_ActionDefinition = new Dictionary<EntityAction, ActionFunction>();
            m_Life = 1;
        }

        public Entity clone(int p_Player, Vector3D p_Position)
        {
            Entity l_Copy = (Entity)this.MemberwiseClone();
            l_Copy.Player = p_Player;
            l_Copy.Position = p_Position;
            return l_Copy;
        }

        public void addActionDefinition(EntityAction p_Action, ActionFunction p_Func)
        {
            m_ActionDefinition.Add(p_Action, p_Func);
        }

        public void update()
        {
            m_Watch.Stop();
            if (m_Action != EntityAction.IDLE)
                if (m_ActionDefinition.ContainsKey(m_Action))
                    m_ActionDefinition[m_Action]((int)m_Watch.ElapsedMilliseconds, m_Cible);
            m_Watch.Reset();
            m_Watch.Start();
        }

        public virtual void closeAction()
        {
            m_Action = EntityAction.IDLE;
            m_Cible = null;
        }

        public void onTakeDamage(Entity p_Entity, double p_Damage)
        {

        }

        public Vector3D Position
        {
            get
            {
                return m_Position;
            }

            set
            {
                lock (this)
                    m_Position = value;
            }
        }

        public double Life
        {
            get
            {
                return m_Life;
            }

            set
            {
                lock (this)
                    m_Life = value;
            }
        }

        public string ObjectName
        {
            get
            {
                return m_ObjectName;
            }

            set
            {
                lock (this)
                    m_ObjectName = value;
            }
        }

        public Resource Cost
        {
            get
            {
                return m_Cost;
            }

            set
            {
                lock (this)
                    m_Cost = value;
            }
        }

        public Resource SellValue
        {
            get
            {
                return m_SellValue;
            }

            set
            {
                lock (this)
                    m_SellValue = value;
            }
        }

        public double BuildTime
        {
            get
            {
                return m_BuildTime;
            }

            set
            {
                lock (this)
                    m_BuildTime = value;
            }
        }

        public double MaxLife
        {
            get
            {
                return m_MaxLife;
            }

            set
            {
                lock(this)
                    m_MaxLife = value;
            }
        }

        public double BaseDamage
        {
            get
            {
                return m_BaseDamage;
            }

            set
            {
                lock(this)
                    m_BaseDamage = value;
            }
        }

        public double BaseArmor
        {
            get
            {
                return m_BaseArmor;
            }

            set
            {
                lock(this)
                    m_BaseArmor = value;
            }
        }

        public Vector3D Size
        {
            get
            {
                return m_Size;
            }

            set
            {
                lock(this)
                    m_Size = value;
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
                lock(this)
                    m_Id = value;
            }
        }

        public EntityAction Action
        {
            get
            {
                return m_Action;
            }

            set
            {
                m_Action = value;
            }
        }

        public Entity Cible
        {
            get
            {
                return m_Cible;
            }

            set
            {
                if (m_Cible != null)
                    lock (this)
                        m_Cible = value;
                else
                    m_Cible = value;
            }
        }

        public double Ki
        {
            get
            {
                return m_Ki;
            }

            set
            {
                lock(this)
                m_Ki = value;
            }
        }

        public int Player
        {
            get
            {
                return m_Player;
            }

            set
            {
                m_Player = value;
            }
        }

        public double Angle
        {
            get
            {
                return m_Angle;
            }

            set
            {
                lock(this)
                m_Angle = value;
            }
        }

        public int Range
        {
            get
            {
                return m_Range;
            }

            set
            {
                m_Range = value;
            }
        }
    }
}
