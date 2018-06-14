using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Apocalypse.Scenes {
    class BattleScene : CoreEngine.Scene {
        // Static:
        public static BattleScene instance;

        // Stores the teams:
        Objects.Unit[] blueTeam;
        Objects.Unit[] redTeam;

        /// <summary>
        /// Creates a new battlescene!:
        /// </summary>
        public BattleScene() : base("BattleScene") {
            // Setup instance:
            BattleScene.instance = this;

            // Add the background
            CoreEngine.GameObject background = new CoreEngine.GameObject(0, 0, "background");
            Add(background);

            // Team params and setup
            int TEAMSIZE = 150;

            // Add the blue dudes:
            for ( int i = 0; i < TEAMSIZE; i++) {
                float x = (float)ApocalypeEngine.rnd.NextDouble() * 617 + 20;
                float y = (float)ApocalypeEngine.rnd.NextDouble() * 675 + 20;
                Add(new Objects.Unit(x, y, "bluesprite", "blueprojectile"));
            }

            // Add the red dudes:
            for (int i = 0; i < TEAMSIZE; i++) {
                float x = (float)ApocalypeEngine.rnd.NextDouble() * 617 + 667;
                float y = (float)ApocalypeEngine.rnd.NextDouble() * 675 + 20;
                Add(new Objects.Unit(x, y, "redsprite", "redprojectile"));
            }
        }
    }
}
