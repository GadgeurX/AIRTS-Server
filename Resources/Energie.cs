using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer.Resources
{
    public class Energie
    {
        int m_Amount;

        public Energie()
        {
            m_Amount = 0;
        }

        public Energie(int p_Amount)
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
