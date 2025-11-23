using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFirstGame
{
    public class Level
    {
        public int LevelNumber { get; private set; } // [cite: 59]
        public List<Enemy> Enemies { get; private set; } // 
        public List<PowerUp> PowerUps { get; private set; }
        
        // Textures needed for this level
        private Texture2D alienScoutTexture;
        private Texture2D alienFighterTexture;
        private Texture2D bossTexture;

        public bool IsComplete { get { return Enemies.Count == 0; } }

        public Level(int number, Texture2D scoutTex, Texture2D fighterTex, Texture2D bossTex)
        {
            this.LevelNumber = number;
            this.Enemies = new List<Enemy>();
            this.PowerUps = new List<PowerUp>();

            // Store textures
            this.alienScoutTexture = scoutTex;
            this.alienFighterTexture = fighterTex;
            this.bossTexture = bossTex;
        }

        /// <summary>
        /// Populates the Enemies list for this level.
        /// </summary>
        public void Load() // [cite: 63]
        {
            Enemies.Clear();
            
            // This 'spawn' logic is from your original design [cite: 62]
            if (LevelNumber == 1)
            {
                // Spawn Alien Scouts [cite: 41]
                Enemies.Add(new AlienScout(alienScoutTexture, new Vector2(100, 100)));
                Enemies.Add(new AlienScout(alienScoutTexture, new Vector2(300, 100)));
            }
            else if (LevelNumber == 2)
            {
                // Spawn Alien Fighters [cite: 41]
                Enemies.Add(new AlienFighter(alienFighterTexture, new Vector2(100, 150), new GameManager()));
                Enemies.Add(new AlienScout(alienScoutTexture, new Vector2(300, 100)));
            }
            else if (LevelNumber == 3)
            {
                Enemies.Add(new AlienFighter(alienFighterTexture,new Vector2(100,150), new GameManager()));
                Enemies.Add(new AlienFighter(alienFighterTexture,new Vector2(120,100), new GameManager()));
                Enemies.Add(new AlienScout(alienScoutTexture,new Vector2(300,50)));
            }
            else if (LevelNumber == 4)
            {
                // Spawn Final Boss [cite: 43]
                Enemies.Add(new BossAlienDragon(bossTexture, new Vector2(300, 50)));
            }
        }

        /// <summary>
        /// Updates all enemies in the level.
        /// </summary>
        public void Update(GameTime gameTime, Player player)
        {
            // Update all enemies
            foreach (var enemy in Enemies)
            {
                enemy.Update(gameTime, player);
            }
            
            // Remove inactive (dead) enemies
            Enemies.RemoveAll(e => !e.IsActive);
            
            // (Update PowerUps here, e.g., make them fall)
        }

        /// <summary>
        /// Draws all enemies and power-ups in the level.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (var pu in PowerUps)
            {
                pu.Draw(spriteBatch);
            }
        }
    }
    
    // NOTE: You would also need to create AlienScout.cs
    // and AlienFighter.cs, similar to BossAlienDragon.
}