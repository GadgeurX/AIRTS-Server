using System;
using System.Configuration;
using System.Threading;

namespace AiRTServer
{
    public class Game
    {
        public Boolean m_Running;
        public Boolean isRunning { get { return m_Running; } }

        NetManager m_Net;
        PlayerManager m_PlayerManager;
        DataManager m_DataManager;
        EntityManager m_EntityManager;
        Thread m_NetworkThread;
        static Game m_Instance;

        public Game()
        {
            m_Instance = this;
            m_PlayerManager = new PlayerManager();
            m_Net = new NetManager();
            var appSettings = ConfigurationManager.AppSettings;
            m_DataManager = new DataManager(appSettings["DBhost"], appSettings["DBuser"], appSettings["DBpassword"], appSettings["DBdb"]);
            m_NetworkThread = new Thread(updateNetwork);
            m_EntityManager = new EntityManager();
        }

        public void init()
        {
            var appSettings = ConfigurationManager.AppSettings;
            m_Net.init(int.Parse(appSettings["port"]));
            m_DataManager.init();
            m_Running = true;
            m_NetworkThread.Start();
            Console.WriteLine("[INFO] Game initialized");
        }

        public void run()
        {
            m_DataManager.update();
            m_EntityManager.update();
            Thread.Sleep(14);
        }

        public void updateNetwork()
        {
            while (m_Running)
            {
                m_Net.update();
            }
        }

        public static Game Instance
        {
            get
            {
                return m_Instance;
            }
        }

        public PlayerManager PlayerManager
        {
            get
            {
                return m_PlayerManager;
            }
        }

        public DataManager DataManager
        {
            get
            {
                return m_DataManager;
            }
        }

        public EntityManager EntityManager
        {
            get
            {
                return m_EntityManager;
            }
        }
    }
}
