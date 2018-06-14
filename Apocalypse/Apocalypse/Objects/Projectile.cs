using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Apocalypse.Objects {
    class Projectile : CoreEngine.GameObject {
        // Projectile speed:
        float PROJECTILESPEED = 80;


        // Target:
        float targetX;
        float targetY;
        float angle;

        /// <summary>
        /// Create a new instance of Projectile.
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="targetX"></param>
        /// <param name="targetY"></param>
        /// <param name="projColour"></param>
        public Projectile(float startX, float startY, float targetX, float targetY, string projColour) : base(startX, startY, projColour) {
            // Set params
            canCollide = true;

            // Calculate angle this projectile is traveling at.
            angle = (float)Math.Atan2(targetX - startX, targetY - startY);
        }

        /// <summary>
        /// Called every frame:
        /// </summary>
        /// <param name="gT"></param>
        public override void Update(GameTime gT) {
            // Move the particle
            transform.X += (float)(Math.Sin(angle) * PROJECTILESPEED);
            transform.Y += (float)(Math.Cos(angle) * PROJECTILESPEED);

            // Slow down projecile over time
            PROJECTILESPEED *= 0.995f;

            // TODO: Actually delete these.
            if ( PROJECTILESPEED < 0.01 ) {
                this.visible = false;
            }

            // Draws bullets:
            drawIndex = transform.Y;
        }
    }
}
