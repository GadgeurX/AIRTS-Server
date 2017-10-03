using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer
{
    public class DataManager
    {
        String m_Ip;
        String m_User;
        String m_Mdp;
        String m_DataBase;
        MySqlConnection m_SqlConnection;
        bool isConnect = false;

        public DataManager(String p_Ip, String p_User, String p_Mdp, String p_DataBase)
        {
            m_Ip = p_Ip;
            m_User = p_User;
            m_Mdp = p_Mdp;
            m_DataBase = p_DataBase;
        }

        public void init()
        {
            string connectionString;
            connectionString = "SERVER=" + m_Ip + ";" + "DATABASE=" +
            m_DataBase + ";" + "UID=" + m_User + ";" + "PASSWORD=" + m_Mdp + ";";

            m_SqlConnection = new MySqlConnection(connectionString);
            try
            {
                Console.WriteLine("[INFO] Database connecting...");
                m_SqlConnection.Open();
                Console.WriteLine("[INFO] Database connected");
                isConnect = true;
            }
            catch (MySqlException)
            {
                isConnect = false;
                Console.WriteLine("[ERROR] Error when database connection");
            }
        }

        public void update()
        {
            if (!isConnect)
                init();
        }

        public Boolean LoginPlayer(String p_Email, String p_Mdp)
        {
            string query = "SELECT id FROM Player WHERE email='" + p_Email + "' AND mdp='" + p_Mdp + "';";

            Boolean isUser = false;
            MySqlCommand cmd = new MySqlCommand(query, m_SqlConnection);
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();
                isUser = dataReader.Read();
                dataReader.Close();
            }
            catch (Exception)
            {
                isConnect = false;
            }
            return isUser;
        }

        public PlayerData LoadPlayer(String p_Email, String p_Mdp)
        {
            string query = "SELECT * FROM Player WHERE email='" + p_Email + "' AND mdp='" + p_Mdp + "';";

            MySqlCommand cmd = new MySqlCommand(query, m_SqlConnection);
            PlayerData player = null;
            try
            {
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataReader.Read();
                player = new PlayerData((int)dataReader["id"], (String)dataReader["email"], (String)dataReader["login"], (String)dataReader["mdp"]);
                dataReader.Close();
            }
            catch(Exception)
            {
                isConnect = false;
            }
            return player;
        }
    }
}
