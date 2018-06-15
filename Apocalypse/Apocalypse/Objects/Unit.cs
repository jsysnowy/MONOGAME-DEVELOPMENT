using System;
using System.Collections.Generic;
using Apocalypse.CoreEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apocalypse.Objects {
    class Unit : CoreEngine.GameObject {
        // Stats
        public float speed = 19;
        public int HP = 3;

        // Weaponary:
        Shooter shooter;

        // Knowledge
        Unit[] allies;
        Unit[] enemies;

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
                //transform.X += (float)((ApocalypeEngine.rnd.NextDouble() * speed) - speed / 2);
                //transform.Y += (float)((ApocalypeEngine.rnd.NextDouble() * speed) - speed / 2);
                drawIndex = transform.Y;

                // IF randomly correct, shoot!
                if ((Math.Round(ApocalypeEngine.rnd.NextDouble() * 50)) == 10) {
                    if (enemies != null) {
                        Unit target = FindClosestToMe(enemies);
                        if (target != null) {
                            float xTarget = (float)ApocalypeEngine.rnd.NextDouble() * 60 - 30;
                            float yTarget = (float)ApocalypeEngine.rnd.NextDouble() * 60 - 30;
                            shooter.ShootAt(target.transform.X+xTarget, target.transform.Y+yTarget);
                        }
                    }
                }

                // Update collision
                base.Update(dT);
            }
        }

        /// <summary>
        /// Pass in lis of allies to this Unit.
        /// </summary>
        /// <param name="allies_"></param>
        public void setAllies( Unit[] allies_ ) {
            allies = allies_;
        }

        /// <summary>
        /// Pass in a list of enemies to this Unit.
        /// </summary>
        /// <param name="enemies_"></param>
        public void setEnemies(Unit[] enemies_) {
            enemies = enemies_;
        }

        /// <summary>
        /// Called if i get hit by something
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollision(GameObject other) {
            if (name == "bluesprite" && other.name == "redprojectile" || name == "redsprite" && other.name == "blueprojectile") {
                HP--;

                if (HP == 0) {
                    this.canCollide = false;
                    this.SetTexture("deadsprite");
                }
            }
        }

        /// <summary>
        /// Returns the closest unit to me.
        /// </summary>
        /// <param name="possibleTargets"></param>
        /// <returns></returns>
        private Unit FindClosestToMe(Unit[] possibleTargets ) {
            Unit closest = null;
            float distance = -1;
            // Loop over all targets, find closest.
            for (int i = 0; i < possibleTargets.Length; i++) {
                if (possibleTargets[i].HP > 0) {
                    if (closest == null) {
                        closest = possibleTargets[i];
                        distance = (float)Math.Sqrt(Math.Pow((possibleTargets[i].transform.X - transform.X), 2) + Math.Pow((possibleTargets[i].transform.Y - transform.Y), 2));
                    } else {
                        float newDist = (float)Math.Sqrt(Math.Pow((possibleTargets[i].transform.X - transform.X), 2) + Math.Pow((possibleTargets[i].transform.Y - transform.Y), 2));
                        if ( newDist < distance ) {
                            distance = newDist;
                            closest = possibleTargets[i];
                        }
                    }
                }
            }

            return closest;
        }
    }
}
