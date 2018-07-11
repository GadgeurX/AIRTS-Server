using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AiRTServer.Entities.Units
{
    public class BarbarianWorker : Worker
    {
        public BarbarianWorker(int p_Player, Vector3D p_Position) : base(p_Player, p_Position)
        {
            this.ObjectName = "BarbarianWorker";
            this.Speed = 2.0;
        }
    }
}
