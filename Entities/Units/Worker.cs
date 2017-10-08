using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AiRTServer.Entities.Units
{
    public class Worker : Unit
    {
        public Worker(int p_Id, int p_Player, Vector3D p_Position) : base(p_Id, p_Player, p_Position)
        {
            this.Speed = 1;
            this.ObjectName = "Worker";
            this.BuildTime = 10.0;
            this.MaxLife = 50;
            this.Life = MaxLife;
            this.Size = new Vector3D(0.5, 1.5, 0.5);
            this.BaseDamage = 1;
            this.BaseArmor = 10;
            this.Cost = new Resource();
            Cost.Gold.Amount = 10;
            this.SellValue = new Resource();
            SellValue.Gold.Amount = 2;
            Action = EntityAction.IDLE;
            Cible = null;
            Ki = 0;
            this.Range = 1;
        }

    }
}
