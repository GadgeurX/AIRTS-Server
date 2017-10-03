using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer
{
    public class PlayerData
    {
        int m_Id = -1;
        String m_Email;
        String m_Login;
        String m_Mdp;

        public PlayerData(int p_Id, String p_Email, String p_Login, String p_Mdp)
        {
            m_Id = p_Id;
            m_Email = p_Email;
            m_Login = p_Login;
            m_Mdp = p_Mdp;
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

        public string Email
        {
            get
            {
                return m_Email;
            }

            set
            {
                m_Email = value;
            }
        }

        public string Login
        {
            get
            {
                return m_Login;
            }

            set
            {
                m_Login = value;
            }
        }

        public string Mdp
        {
            get
            {
                return m_Mdp;
            }

            set
            {
                m_Mdp = value;
            }
        }
    }
}
