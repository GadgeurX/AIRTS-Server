using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer.Resources
{
    public class Metal
    {
        int m_Amount;

        public Metal()
        {
            m_Amount = 0;
        }

        public Metal(int p_Amount)
        {
            m_Amount = p_Amount;
        }

        public int Amount
        {
            get
            {
                return m_Amount;
            }

            set
            {
                m_Amount = value;
            }
        }
    }
}
