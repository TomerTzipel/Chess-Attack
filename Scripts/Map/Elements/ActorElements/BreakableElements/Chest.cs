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
            
            if (RunManager.Player.UseKey())
            {
                return base.TakeDamage(damage);
            }
            return false;
        }

        public override void Die()
        {
            //TODO:GIVE PLAYER SOMETHING
        }
    }
}
