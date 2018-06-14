using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apocalypse.CoreEngine {
    class Scene {
        // Class Field:
        // - State information:
        public string name;

        // - Current Objects:
        public List<GameObject> sceneObjects;
        public List<GameObject> newObjects;

        /// <summary>
        /// Creates a new instance of Scene.
        /// </summary>
        /// <param name="name_"></param>
        public Scene( string name_ ) {
            // Init gameObjects:
            sceneObjects = new List<GameObject>();
            newObjects = new List<GameObject>();
            // Set param values
            name = name_;
        }
            
        /// <summary>
        /// Updates this scene.
        /// </summary>
        public void Update() {
            foreach (GameObject gO in newObjects) {
                sceneObjects.Add(gO);
            }

            newObjects = new List<GameObject>();
        }

        /// <summary>
        /// Adds an object to the scene.
        /// </summary>
        /// <param name="obj"></param>
        public void Add(GameObject obj) {
            newObjects.Add(obj);
        }

        /// <summary>
        /// Removes an object from the scene.
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(GameObject obj) {
            sceneObjects.Remove(obj);
        }

        /// <summary>
        /// Re-orders all objects in the scene via their drawIndex:
        /// </summary>
        public void ReorderObjects() {
            sceneObjects.Sort((x, y) => x.drawIndex.CompareTo(y.drawIndex));
        }
    }
}
