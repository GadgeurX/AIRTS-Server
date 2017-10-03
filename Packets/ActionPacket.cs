using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer.Packets
{
    [Serializable()]
    public class ActionPacket : Packet
    {
        String m_Action;
        int m_Id;
        double m_posX;
        double m_posY;

        public ActionPacket(String p_Action, int p_Id, double pos_X, double pos_Y) : base(PacketType.ACTION)
        {
            m_Action = p_Action;
            m_Id = p_Id;
            m_posX = pos_X;
            m_posY = pos_Y;
        }

        public string Action
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

        public double Pos_X
        {
            get
            {
                return m_posX;
            }

            set
            {
                m_posX = value;
            }
        }

        public double Pos_Y
        {
            get
            {
                return m_posY;
            }

            set
            {
                m_posY = value;
            }
        }
    }
}
