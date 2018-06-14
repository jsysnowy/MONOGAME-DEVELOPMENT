using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apocalypse.CoreEngine {
    class TextureLib {
        /// <summary>
        /// Makes TextureLib accessible anywhere:
        /// </summary>
        public static TextureLib instance;

        /// <summary>
        /// Dictionary object stores all currently loaded textures in the spriteBatch, ready to be used.
        /// </summary>
        Dictionary<string, Texture2D> textures;
            
        /// <summary>
        /// Creates a new instance of TextureLib.
        /// </summary>
        /// <param name="sB_"></param>
        public TextureLib() {
            // Create instance:
            TextureLib.instance = this;

            // Create Dictionary:
            textures = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Adds a texture to the library of available textures.
        /// </summary>
        /// <param name="textureName"></param>
        /// <param name="tex"></param>
        /// <returns></returns>
        public bool AddTexture(string textureName, Texture2D tex) {
            if ( textures.ContainsKey(textureName)) {
                return false;
            }

            textures.Add(textureName, tex);

            return true;
        }
            
            
        /// <summary>
        /// Grabs a texture with passed in name. Default shows missing texture image.
        /// </summary>
        /// <param name="textureName"></param>
        /// <returns></returns>
        public Texture2D GetTexture(string textureName) {
            if ( textures.ContainsKey(textureName)) {
                return textures[textureName];
            } else {
                return null;
            }
        }
    }
}
