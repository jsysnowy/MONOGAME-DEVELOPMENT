using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apocalypse.CoreEngine {
    class GameObject {
        // Class field:
        // - Name/Tags:
        public string name;

        // - Textures
        public string texName;
        public Texture2D tex;

        // - Drawing
        public bool visible;
        public float drawIndex;

        // - Positioning:
        public Vector2 transform;

        // - Settings
        public bool stopOOB;
        public bool canCollide;

        /// <summary>
        /// Creates a new instance of GameObject.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="texName_"></param>
        public GameObject(float x, float y, string texName_) {
            // Set transform
            transform = new Vector2(x, y);
            name = texName_;

            // Set default values
            visible = true;
            drawIndex = 1;
            stopOOB = false;
            canCollide = false;

            // Set texture
            texName = texName_;
        }
            
        /// <summary>
        /// Sets a new texture on this object.
        /// </summary>
        /// <param name="texName_"></param>
        public void SetTexture(string texName_) {
            // Set texture
            texName = texName_;
            tex = TextureLib.instance.GetTexture(texName);
            if (texName == "deadsprite") {
            texName = texName_;
            }
        }

        /// <summary>
        /// Updates logic in this GameObject.
        /// </summary>
        public virtual void Update(GameTime gT) {
            // If sprite can collide, check if it has collided:
            if (canCollide && tex != null) {
                foreach (GameObject other in Scenes.BattleScene.instance.sceneObjects ) {
                    if (this.canCollide && tex != null && other.canCollide && other.tex != null) {
                        // Make sure all 4 are true
                        int count = 0;

                        // Collision from other entering from RED:
                        if (other.transform.X + other.tex.Width > transform.X) {
                            count++;
                        }

                        // Collision from other entering from BLUE:
                        if (other.transform.X < transform.X + tex.Width) {
                            count++;
                        }

                        // Collision from other entering from GREEN:
                        if (other.transform.Y + other.tex.Height > transform.Y) {
                            count++;
                        }

                        // Collision from other entering from RED:
                        if (other.transform.Y < transform.Y + tex.Height) {
                            count++;
                        }

                        // Collision if 4!
                        if (count == 4) {
                            this.OnCollision(other);
                        }
                    }
                }
            } 
        }

        /// <summary>
        /// Called if this gameObject collided with something else.
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision( GameObject other ) {

        }
            
        /// <summary>
        /// Draws this sprite. Called in gameLoop.
        /// </summary>
        /// <param name="sB"></param>
        public void Draw( SpriteBatch sB ) {
            // Draw if sprite is visible:
            if (tex == null) {
                SetTexture(texName);
            }
            if (visible) {

                // If stop OOB , make sure sprite stays on the screen
                if (stopOOB) {
                    if (transform.X < 0) { transform.X = 0; } 
                    else if ((transform.X + tex.Width) > 1334) { transform.X = 1334- tex.Width; }
                    if (transform.Y < 0) { transform.Y = 0; }
                    else if ((transform.Y + tex.Height) > 750) { transform.Y = 750-tex.Height; }
                }

                sB.Draw(tex, transform);
            }
        }
    }
}
