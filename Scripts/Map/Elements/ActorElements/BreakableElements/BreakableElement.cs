using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public abstract class BreakableElement : ActorElement, IHitable
    {
        public HealthHandler HealthHandler { get; private set; }

        public BreakableElement(Texture2D sprite,int hitsToBreak,Point mapPosition) : base(sprite, mapPosition)
        {
            HealthHandler = new HealthHandler(hitsToBreak);
            HealthHandler.OnDeath += Die;
        }

        public virtual bool TakeDamage(int damage)
        {
            HealthHandler.ModifyHealth(-1, HealthChangeMode.Current);
            return HealthHandler.IsDead;
        }

        public abstract void Die();  
    }
}
