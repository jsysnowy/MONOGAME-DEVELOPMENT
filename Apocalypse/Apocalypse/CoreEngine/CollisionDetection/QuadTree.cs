using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apocalypse.CoreEngine.CollisionDetection {

    /// <summary>
    /// QuadTree splits the screen space into quadrants of 4, which store colliding game objects inside each node.
    /// This will drastically reduce collision detection weight by making sure only small groups of objects in nearby
    /// screenspace will be checked for collisions:
    /// </summary>
    class QuadTree {
        /// <summary>
        /// Stores the maximum number of objects allowed before the QuadTree splits.
        /// </summary>
        private int MAX_OBJ = 2;

        /// <summary>
        /// Stores the deepest level subnode allowed.
        /// </summary>
        private int MAX_DEPTH = 8;

        /// <summary>
        /// Stores the current level of this particular QuadTree.
        /// </summary>
        private int level;

        /// <summary>
        /// Stores all objects currently inside this QuadTree.
        /// </summary>
        private List<CoreEngine.GameObject> objects;

        /// <summary>
        /// Stores the current bounds of this QuadTree.
        /// </summary>
        private Rectangle bounds;

        /// <summary>
        /// Stores all subnodes inside this QuadTree.
        /// </summary>
        private QuadTree[] nodes;

        /// <summary>
        /// Creates a new instance of QuadTree.
        /// </summary>
        /// <param name="level_"></param>
        /// <param name="bounds_"></param>
        public QuadTree(int level_, Rectangle bounds_) {
            // Store passed in params:
            level = level_;
            bounds = bounds_;

            // Generate lists and arrays:
            objects = new List<GameObject>();
            nodes = new QuadTree[4];
        }
            
        /// <summary>
        /// Recursively clear all objects and childobjects from this QuadTree and it's childNodes.
        /// </summary>
        public void Clear() {
            // Clear all objects stores in List.
            objects.Clear();

            // Loop over all nodes and clear them too:
            for ( int i = 0; i < nodes.Length; i++) {
                if ( nodes[i] != null) {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }
            
        /// <summary>
        /// This instance of Quadnode splits into four new, smaller QuadNodes.
        /// </summary>
        public void Split() {
            // Store current bounds:
            int subWidth = (int)bounds.Width / 2;
            int subHeight = (int)bounds.Height / 2;
            int x = bounds.X;
            int y = bounds.Y;

            // Generate 4 new QuadTree nodes:
            nodes[0] = new QuadTree(level + 1, new Rectangle(x+subWidth, y, subWidth, subHeight));
            nodes[1] = new QuadTree(level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new QuadTree(level + 1, new Rectangle(x, y+subHeight, subWidth, subHeight));
            nodes[3] = new QuadTree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        /// <summary>
        /// Returns which subnobe a passed in Rectangle belongs to. Returning -1 means object doesn't completely fit, and belongs to a  parent node:
        /// </summary>
        /// <returns></returns>
        public int getIndex( Rectangle rect ) {
            // Store params based on passed in rect:
            int index = -1;
            int verticalMid = bounds.X + (bounds.Width / 2);
            int horizontalMid = bounds.Y + (bounds.Height / 2);

            // Stores if objects can fit completely in top or bottom quads:
            bool fitInTop = (rect.Y < horizontalMid && rect.Y + rect.Height < horizontalMid);
            bool fitInBottom = (rect.Y > horizontalMid);

            // Store if objects can fit in left or right quads:
            bool fitInLeft = (rect.X < verticalMid && rect.X + rect.Width < verticalMid);
            bool fitInRight = (rect.X > verticalMid);

            // Sets index based on bools defined above:
            if (fitInLeft) {
                if ( fitInTop ) {
                    index = 1;
                }  else if ( fitInBottom ) {
                    index = 2;
                }
            } else if ( fitInRight ) {
                if (fitInTop) {
                    index = 0;
                } else if (fitInBottom) {
                    index = 3;
                }
            }

            // Returns current value of index:
            return index;
        }

        /// <summary>
        /// Inserts an object into the QuadTree:
        /// </summary>
        public void Insert( CoreEngine.GameObject collider )  {
            // Check if this QuadTree has split:
            if (nodes[0] != null) {
                // Get index of the inserted obj:
                int insertedObjIndex = getIndex(collider.bounds);

                // If it fits entirely in a sub-quad, insert it into that instead:
                if (insertedObjIndex != -1) {
                    nodes[insertedObjIndex].Insert(collider);
                    return;
                }
            }

            // Add the object to the list of objects in this QuadTree:
            objects.Add(collider);

            // If params are hit to split, this QuadTree now needs to split:
            if (objects.Count > MAX_OBJ && level < MAX_DEPTH) {
                // Make sure i already haven't split:
                if (nodes[0] == null) {
                    Split();
                }

                // All current objects are distributed in newly split QuadTree:
                int i = 0;
                while (i < objects.Count ) {
                    // Get index of newly returned obj:
                    int index = getIndex(objects[i].bounds);

                    // If index is not -1:
                    if ( index != -1 ) {
                        nodes[index].Insert(objects[i]);
                        objects.RemoveAt(i);
                    } else {
                        // Increment i.
                        i++;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a list of all objects the passed in rectange could possible collide with:
        /// </summary>
        /// <param name="returnObjects"></param>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public List<GameObject> Retrieve( List<GameObject> returnObjects, Rectangle bounds ) {
            // Find which index bounds sits in:
            int index = getIndex(bounds);

            // If index was not -1 and subnodes exist:
            if (index != -1 && nodes[0] != null) {
                // Retreive all objects which are inside the subnode it exists inside:
                nodes[index].Retrieve(returnObjects, bounds);
            }

            // Add all objects belonging to this particular node:
            for ( int i = 0; i < objects.Count; i++) {
                returnObjects.Add(objects[i]);
            }

            // Return array with all new stuff in it:
            return returnObjects;
        }

        /// <summary>
        /// Draws this Quad onto the screen
        /// </summary>
        /// <param name="sB"></param>
        public void Draw( SpriteBatch sB, GraphicsDevice gD, Color col) {
            // If nodes exist, then draw them instead:
            if ( nodes[0] != null) {
                nodes[0].Draw(sB, gD, Color.Red);
                nodes[1].Draw(sB, gD, Color.Green);
                nodes[2].Draw(sB, gD, Color.Yellow);
                nodes[3].Draw(sB, gD, Color.Blue);
                return;
            }


            Texture2D texture = new Texture2D(gD, bounds.Width, bounds.Height);
            Color[] colorData = new Color[bounds.Width * bounds.Height];
            for (int i = 0; i < (bounds.Width * bounds.Height); i++) {
                col.A = 20;
                colorData[i] = col;
            }
                
            texture.SetData<Color>(colorData);
            sB.Draw(texture, new Vector2(bounds.X, bounds.Y));
        }
    }
}
