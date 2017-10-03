using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiRTServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[INFO] Game creating...");
            Game l_Game = new Game();
            Console.WriteLine("[INFO] Game created");
            Console.WriteLine("[INFO] Game initializing...");
            l_Game.init();
            Console.WriteLine("[INFO] Start game");
            Console.WriteLine("-------------------------------------");
            while (l_Game.isRunning)
            {
                l_Game.run();
            }
        }
    }
}