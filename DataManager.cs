using MongoDB.Bson;
using MongoDB.Driver;
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
        String m_DataBase;
        MongoClient m_Connection;
        IMongoDatabase m_AirtsDb;
        IMongoCollection<BsonDocument> m_UsersCollection;
        bool isConnect = false;

        public DataManager(String p_Ip, String p_DataBase)
        {
            m_Ip = p_Ip;
            m_DataBase = p_DataBase;
        }

        public void Init()
        {
            string connectionString;
            connectionString = "mongodb://" + m_Ip + ":27017";
            try
            {
                m_Connection = new MongoClient(connectionString);
                Console.WriteLine("[INFO] Database connecting...");
                m_AirtsDb = m_Connection.GetDatabase(m_DataBase);
                m_UsersCollection = m_AirtsDb.GetCollection<BsonDocument>("Users");
                Console.WriteLine("[INFO] Database connected");
                isConnect = true;
            }
            catch (Exception)
            {
                isConnect = false;
                Console.WriteLine("[ERROR] Error when database connection");
            }
        }

        public void Update()
        {
            if (!isConnect)
                Init();
        }

        public Boolean LoginPlayer(String p_Email, String p_Mdp)
        {
            bool isUser = false;
            try
            {
                var filter = new BsonDocument
                {
                    { "email", p_Email },
                    { "pwd", p_Mdp }
                };
                var l_Result = m_UsersCollection.Find(filter).ToList();
                isUser = l_Result.Count > 0;
            }
            catch (Exception)
            {
                isConnect = false;
            }
            return isUser;
        }

        public PlayerData LoadPlayer(String p_Email, String p_Mdp)
        {
            PlayerData player = null;
            try
            {
                var filter = new BsonDocument
                {
                    { "email", p_Email },
                    { "pwd", p_Mdp }
                };
                var l_Result = m_UsersCollection.Find(filter).ToList();
                if (l_Result.Count > 0)
                {
                    var dataReader = l_Result[0];
                    player = new PlayerData((int)dataReader["id"], (String)dataReader["email"], (String)dataReader["login"], (String)dataReader["pwd"]);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("[ERROR] " + e.Message);
                isConnect = false;
            }
            return player;
        }
    }
}
