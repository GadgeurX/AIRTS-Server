using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AiRTServer.Entities
{
    class WorldPosition : Entity
    {
        public WorldPosition(Vector3D p_Position) : base(0, -1, p_Position)
        {
        }
    }
}
