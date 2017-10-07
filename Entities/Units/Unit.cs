using AiRTServer.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace AiRTServer.Entities.Units
{
    public abstract class Unit : Entity
    {
        double m_Speed;

        public Unit(int p_Id, int p_Player, Vector3D p_Position) : base(p_Id, p_Player, p_Position)
        {
            this.addActionDefinition("Basic", BasicAction);
            this.addActionDefinition("Move", MoveAction);
        }

        public double Speed
        {
            get
            {
                return m_Speed;
            }

            set
            {
                lock (this)
                    m_Speed = value;
            }
        }

        public override void closeAction()
        {
            base.closeAction();
            Game.Instance.PlayerManager.sendToAll(new AnimPacket(this.Id, "idle", 1.0));
        }

        private void MoveAction(int p_AlphaMs, Entity p_Cible)
        {
            lock (p_Cible)
            {
            Vector3D l_Direction = p_Cible.Position - this.Position;
            l_Direction.Normalize();
            this.Angle = Math.Acos(0 * l_Direction.X + 1 * l_Direction.Z) * (180.0 / Math.PI);
                if (l_Direction.X < 0)
                    Angle = -Angle;
            l_Direction = l_Direction * (m_Speed * (p_AlphaMs / 1000.0));
            this.Position += l_Direction;
            if (Position.X - (Size.X / 2 + this.Range) < p_Cible.Position.X + (p_Cible.Size.X / 2) &&
                Position.X + (Size.X / 2 + this.Range) > p_Cible.Position.X - (p_Cible.Size.X / 2) &&
                Position.Z - (Size.Z / 2 + this.Range) < p_Cible.Position.Z + (p_Cible.Size.Z / 2) &&
                (Size.Z / 2) + Position.Z > p_Cible.Position.Z - (p_Cible.Size.Z / 2))
            {
                    if (Game.Instance.PlayerManager.getPlayerById(Player).isHostile(p_Cible.Player) == HOSTILITY.ENEMY)
                    {
                        Game.Instance.PlayerManager.sendToAll(new AnimPacket(this.Id, "attack", 1.0));
                        Action = "Attack";
                    }
                    else
                    {
                        this.closeAction();
                    }
            }
        }
        }

        private void AttackAction(int p_AlphaMs, Entity p_Cible)
        {
            lock (p_Cible)
            {
                Vector3D l_Direction = p_Cible.Position - this.Position;
                l_Direction.Normalize();
                this.Angle = Math.Acos(0 * l_Direction.X + 1 * l_Direction.Z) * (180.0 / Math.PI);
                if (l_Direction.X < 0)
                    Angle = -Angle;
                if (Position.X - (Size.X / 2 + this.Range) < p_Cible.Position.X + (p_Cible.Size.X / 2) &&
                    Position.X + (Size.X / 2 + this.Range) > p_Cible.Position.X - (p_Cible.Size.X / 2) &&
                    Position.Z - (Size.Z / 2 + this.Range) < p_Cible.Position.Z + (p_Cible.Size.Z / 2) &&
                    (Size.Z / 2) + Position.Z > p_Cible.Position.Z - (p_Cible.Size.Z / 2))
                {
                    p_Cible.Life -= this.BaseDamage / 7.14;
                }
                else
                {
                    Game.Instance.PlayerManager.sendToAll(new AnimPacket(this.Id, "walk", this.Speed));
                    Action = "Move";
                }
            }
        }

        private void BasicAction(int p_AlphaMs, Entity p_Cible)
        {
            Game.Instance.PlayerManager.sendToAll(new AnimPacket(this.Id, "walk", this.Speed));
            Action = "Move";
        }
    }
}
