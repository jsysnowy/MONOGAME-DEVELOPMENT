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
            int TEAMSIZE = 15;

            blueTeam = new Objects.Unit[TEAMSIZE];
            redTeam = new Objects.Unit[TEAMSIZE];
            // Add the blue dudes:
            for ( int i = 0; i < TEAMSIZE; i++) {
                float x = (float)ApocalypeEngine.rnd.NextDouble() * 617 + 20;
                float y = (float)ApocalypeEngine.rnd.NextDouble() * 675 + 20;
                Objects.Unit newUnit = new Objects.Unit(x, y, "bluesprite", "blueprojectile");
                Add(newUnit);
                blueTeam[i] = newUnit;
            }

            // Add the red dudes:
            for (int i = 0; i < TEAMSIZE; i++) {
                float x = (float)ApocalypeEngine.rnd.NextDouble() * 617 + 667;
                float y = (float)ApocalypeEngine.rnd.NextDouble() * 675 + 20;
                Objects.Unit newUnit = new Objects.Unit(x, y, "redsprite", "redprojectile");
                Add(newUnit);
                redTeam[i] = newUnit;
            }

            // Let all units know their allies and enemies
            for (int i = 0; i < blueTeam.Length; i++) {
                blueTeam[i].setEnemies(redTeam);
                blueTeam[i].setAllies(blueTeam);
            }

            for ( int i = 0; i < redTeam.Length; i++) {
                redTeam[i].setEnemies(blueTeam);
                redTeam[i].setAllies(redTeam);
            }
        }
    }
}
