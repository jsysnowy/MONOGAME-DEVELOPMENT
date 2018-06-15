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

        // - Bounds
        public Rectangle bounds;

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
            SetTexture(texName);
        }
            
        /// <summary>
        /// Sets a new texture on this object.
        /// </summary>
        /// <param name="texName_"></param>
        public void SetTexture(string texName_) {
            // Set texture
            texName = texName_;
            tex = TextureLib.instance.GetTexture(texName);
            this.bounds = new Rectangle((int)this.transform.X, (int)this.transform.Y, this.tex.Width, this.tex.Height);
        }

        /// <summary>
        /// Updates logic in this GameObject.
        /// </summary>
        public virtual void Update(GameTime gT) {
            this.bounds = new Rectangle((int)this.transform.X, (int)this.transform.Y, this.tex.Width, this.tex.Height);
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
