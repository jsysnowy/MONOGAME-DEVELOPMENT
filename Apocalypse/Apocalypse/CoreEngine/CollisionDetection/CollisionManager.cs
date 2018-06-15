using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apocalypse.CoreEngine.CollisionDetection {
    class CollisionManager {

        /// <summary>
        /// Whether or not this collision detection system is active or not:
        /// </summary>
        bool active { get; set; }
        
        /// <summary>
        /// The bounds in which this CollisionManager checks:
        /// </summary>
        private Rectangle bounds { get; set; }

        private List<GameObject> returnObjects;

        QuadTree collisionDetectionArea;

        /// <summary>
        /// Creates a new instance of CollisionManager.
        /// </summary>
        public CollisionManager( Rectangle bounds_, bool startsActive ) {
            // Sets passed in params
            bounds = bounds_;
            active = startsActive;

            // Creates the QuadTree:
            collisionDetectionArea = new QuadTree(0, bounds);

            // Creates list
            returnObjects = new List<GameObject>();
        }

        /// <summary>
        /// Collision system runs checks on all objects and triggers any colliding functions:
        /// </summary>
        public void Update(List<GameObject> objectsList) {
            // Wipe the current area:
            collisionDetectionArea.Clear();
            
            
            // Insert all colliding objects
            for (int i = 0; i < objectsList.Count; i++) {
                GameObject testObj = objectsList[i];
                // Make sure obj can collide
                if (testObj.canCollide) {
                    // Insert our object into the QuadTree:
                    collisionDetectionArea.Insert(testObj);
                }
            }

            for (int i = 0; i < objectsList.Count; i++) {
                GameObject testObj = objectsList[i];
                // Make sure object can collide:
                if (testObj.canCollide) {
                    // Wipe returnObjects:
                    returnObjects.Clear();

                    // Retrieve all collidable objects:
                    returnObjects = collisionDetectionArea.Retrieve(returnObjects, testObj.bounds);

                    // Perform collision detection on all returnedObjects with our testObj:
                    for (int ii = 0; ii < returnObjects.Count; ii++) {
                        GameObject other = returnObjects[ii];
                        if (testObj != other) {
                            if (testObj.bounds.Intersects(other.bounds)) {
                                testObj.OnCollision(other);
                            }
                        }
                    }
                }
            }
        }
           
        public void Draw(SpriteBatch sB, GraphicsDevice gD ) {
            collisionDetectionArea.Draw(sB, gD, Color.White);
        }
    }
}
