using AiRTServer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer
{
    public class Resource
    {
        Energie m_Energie;
        Metal m_Metal;
        Gold m_Gold;

        public Resource()
        {
            m_Energie = new Energie();
            m_Metal = new Metal();
            m_Gold = new Gold();
        }

        public Energie Energie
        {
            get
            {
                return m_Energie;
            }

            set
            {
                lock (this)
                    m_Energie = value;
            }
        }

        public Metal Metal
        {
            get
            {
                return m_Metal;
            }

            set
            {
                lock (this)
                    m_Metal = value;
            }
        }

        public Gold Gold
        {
            get
            {
                return m_Gold;
            }

            set
            {
                m_Gold = value;
            }
        }
    }
}
