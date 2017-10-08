using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AiRTServer.Packets
{
    [Serializable()]
    public class EntityPacket : Packet
    {
        int m_Id;
        double m_PositionX;
        double m_PositionZ;
        String m_Type;
        double m_Angle;
        double m_SizeX;
        double m_SizeZ;
        HOSTILITY m_Hostile;

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

        public string Type1
        {
            get
            {
                return m_Type;
            }

            set
            {
                m_Type = value;
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
                m_Angle = value;
            }
        }

        public double PositionX
        {
            get
            {
                return m_PositionX;
            }

            set
            {
                m_PositionX = value;
            }
        }

        public double PositionZ
        {
            get
            {
                return m_PositionZ;
            }

            set
            {
                m_PositionZ = value;
            }
        }

        public double SizeX
        {
            get
            {
                return m_SizeX;
            }

            set
            {
                m_SizeX = value;
            }
        }

        public double SizeZ
        {
            get
            {
                return m_SizeZ;
            }

            set
            {
                m_SizeZ = value;
            }
        }

        public HOSTILITY Hostile
        {
            get
            {
                return m_Hostile;
            }

            set
            {
                m_Hostile = value;
            }
        }

        public EntityPacket(Entity p_Entity) : base(PacketType.ENTITY)
        {
            m_Id = p_Entity.Id;
            m_PositionX = p_Entity.Position.X;
            m_PositionZ = p_Entity.Position.Z;
            m_Type = p_Entity.GetType().Name;
            m_Angle = p_Entity.Angle;
            m_SizeX = p_Entity.Size.X;
            m_SizeZ = p_Entity.Size.Z;
        }
    }
}
