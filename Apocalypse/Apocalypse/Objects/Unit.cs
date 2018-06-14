using System;
using System.Collections.Generic;
using Apocalypse.CoreEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apocalypse.Objects {
    class Unit : CoreEngine.GameObject {
        // Stats
        public float speed = 15;
        public int HP = 3;

        // Weaponary:
        Shooter shooter;

        /// <summary>
        /// Create a new unit.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texture"></param>
        public Unit(float x, float y, string texture, string bulletType ) : base( x, y, texture ) {
            // Stops gameObject going out of bounds.
            stopOOB = true;
            canCollide = true;

            shooter = new Shooter(this, bulletType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dT"></param>
        public override void Update( GameTime dT ) {
            if (HP > 0) {
                // Move the dude around:
                transform.X += (float)((ApocalypeEngine.rnd.NextDouble() * speed) - speed / 2);
                transform.Y += (float)((ApocalypeEngine.rnd.NextDouble() * speed) - speed / 2);
                drawIndex = transform.Y;

                // IF randomly correct, shoot!
                if ((Math.Round(ApocalypeEngine.rnd.NextDouble() *130)) == 10) {
                    float xTarget = (float)ApocalypeEngine.rnd.NextDouble() * 1334;
                    float yTarget = (float)ApocalypeEngine.rnd.NextDouble() * 750;
                    shooter.ShootAt(xTarget, yTarget);
                }

                // Update collision
                base.Update(dT);
            }
        }

        /// <summary>
        /// Called if i get hit by something
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollision(GameObject other) {
            if (name == "bluesprite" && other.name == "redprojectile" || name == "redsprite" && other.name == "blueprojectile") {
                HP--;

                other.visible = false;
                other.canCollide = false;

                if (HP == 0) {
                    this.canCollide = false;
                    this.SetTexture("deadsprite");
                }
            }
        }
    }
}
