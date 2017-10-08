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
            this.addActionDefinition(EntityAction.DEFAULT, BasicAction);
            this.addActionDefinition(EntityAction.MOVE, MoveAction);
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

        public bool IsInRange(Entity p_Cible)
        {
            return Position.X - (Size.X / 2 + this.Range) < p_Cible.Position.X + (p_Cible.Size.X / 2) &&
                Position.X + (Size.X / 2 + this.Range) > p_Cible.Position.X - (p_Cible.Size.X / 2) &&
                Position.Z - (Size.Z / 2 + this.Range) < p_Cible.Position.Z + (p_Cible.Size.Z / 2) &&
                (Size.Z / 2) + Position.Z > p_Cible.Position.Z - (p_Cible.Size.Z / 2);
        }

        public void LookAt(Entity p_Cible)
        {
            Vector3D l_Direction = p_Cible.Position - this.Position;
            l_Direction.Normalize();
            this.Angle = Math.Acos(0 * l_Direction.X + 1 * l_Direction.Z) * (180.0 / Math.PI);
            if (l_Direction.X < 0)
                this.Angle = -this.Angle;
        }

        public override void closeAction()
        {
            base.closeAction();
            Game.Instance.PlayerManager.SendToAll(new AnimPacket(this.Id, "idle", 1.0));
        }

        private void MoveAction(int p_AlphaMs, Entity p_Cible)
        {
            lock (p_Cible)
            {
                LookAt(p_Cible);
                Vector3D l_Direction = p_Cible.Position - this.Position;
                l_Direction.Normalize();
                l_Direction = l_Direction * (m_Speed * (p_AlphaMs / 1000.0));
                this.Position += l_Direction;
                if (IsInRange(p_Cible))
                {
                    if (Game.Instance.PlayerManager.GetPlayerById(Player).IsHostile(p_Cible.Player) == HOSTILITY.ENEMY)
                    {
                        Game.Instance.PlayerManager.SendToAll(new AnimPacket(this.Id, "attack", 1.0));
                        Action = EntityAction.ATTACK;
                    }
                    else
                        this.closeAction();
                }
            }
        }

        private void AttackAction(int p_AlphaMs, Entity p_Cible)
        {
            lock (p_Cible)
            {
                LookAt(p_Cible);
                if (IsInRange(p_Cible))
                    p_Cible.Life -= this.BaseDamage / 7.14;
                else
                {
                    Game.Instance.PlayerManager.SendToAll(new AnimPacket(this.Id, "walk", this.Speed));
                    Action = EntityAction.MOVE;
                }
            }
        }

        private void BasicAction(int p_AlphaMs, Entity p_Cible)
        {
            Game.Instance.PlayerManager.SendToAll(new AnimPacket(this.Id, "walk", this.Speed));
            Action = EntityAction.MOVE;
        }
    }
}
