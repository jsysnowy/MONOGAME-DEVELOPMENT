using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apocalypse.Objects {
    class Shooter {
        // This is the gameObject this shooter is attached to.
        CoreEngine.GameObject parent;
        string colour;

        /// <summary>
        /// Create a new instance of Shooter.
        /// </summary>
        public Shooter( CoreEngine.GameObject parent_, string colour_ ) {
            // Store the parent.
            parent = parent_;
            colour = colour_;
        }
            
        /// <summary>
        /// Tell the shooter to shoot at something.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ShootAt(float x, float y) {
            Projectile bullet = new Projectile(parent.transform.X, parent.transform.Y, x, y, colour);
            Scenes.BattleScene.instance.Add(bullet);
        }
    }
}
