using System;
using System.Configuration;
using System.Threading;

namespace AiRTServer
{
    public class Game
    {
        public Boolean m_Running;
        public Boolean IsRunning { get { return m_Running; } }

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
            m_Net = new NetManager(new ServerPacketProcessor());
            var appSettings = ConfigurationManager.AppSettings;
            m_DataManager = new DataManager(appSettings["DBhost"], appSettings["DBdb"]);
            m_NetworkThread = new Thread(UpdateNetwork);
            m_EntityManager = new EntityManager();
        }

        public void Init()
        {
            var appSettings = ConfigurationManager.AppSettings;
            m_Net.Init(int.Parse(appSettings["port"]));
            m_DataManager.Init();
            m_Running = true;
            m_NetworkThread.Start();
            Console.WriteLine("[INFO] Game initialized");
        }

        public void Run()
        {
            m_DataManager.Update();
            m_EntityManager.Update();
            Thread.Sleep(14);
        }

        public void UpdateNetwork()
        {
            while (m_Running)
            {
                m_Net.Update();
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
