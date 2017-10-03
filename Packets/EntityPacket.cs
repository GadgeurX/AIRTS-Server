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
        double m_PositionY;
        String m_Type;
        double m_Angle;
        double m_SizeX;
        double m_SizeY;
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

        public double PositionY
        {
            get
            {
                return m_PositionY;
            }

            set
            {
                m_PositionY = value;
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

        public double SizeY
        {
            get
            {
                return m_SizeY;
            }

            set
            {
                m_SizeY = value;
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

        public EntityPacket(int p_Id, double p_PositionX, double p_PositionY, String p_Type, double p_Angle, double p_SizeX, double p_SizeY) : base(PacketType.ENTITY)
        {
            m_Id = p_Id;
            m_PositionX = p_PositionX;
            m_PositionY = p_PositionY;
            m_Type = p_Type;
            m_Angle = p_Angle;
            m_SizeX = p_SizeX;
            m_SizeY = p_SizeY;
        }
    }
}
