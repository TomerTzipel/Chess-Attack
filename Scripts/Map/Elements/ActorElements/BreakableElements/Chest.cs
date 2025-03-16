using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slay_The_Basilisk_MonoGame
{
    public class Chest : BreakableElement
    {
        public Chest(Point mapPosition) : base(AssetsManager.GetAsset(Asset.Chest), 1, mapPosition) { }

        public override bool TakeDamage(int damage)
        {
            //CHANGE CHECK TO SEE IF PLAYER HAS A KEY
            if (EnemyElement._enemies.Count == 0)
            {
                return base.TakeDamage(damage);
            }
            return false;
        }

        public override void Die()
        {
            //GIVE PLAYER SOMETHING
        }
    }
}
