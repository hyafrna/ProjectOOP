// Add these 'using' statements for MonoGame
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    public class Projectile
    {
        // MonoGame visual properties
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }

        // Properties from your original UML design
        public int Damage { get; private set; }
        public string Owner { get; private set; }
        private float speed;

        // Helper properties for game logic
        public bool IsActive { get; set; }
        private Vector2 direction;
        private Vector2 startPosition;
        private Vector2 direction1;

        // Bounding box for collision detection
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }
        }

        // Constructor
        public Projectile(Texture2D texture, Vector2 startPosition, int damage, string owner, Vector2 direction, float speed)
        {
            this.Texture = texture;
            this.Position = startPosition;
            this.Damage = damage;
            this.Owner = owner;
            this.speed = speed;
            this.Size = new Vector2(4, 10); // A 4x10 pixel rectangle

            // Normalize the direction to ensure constant speed
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            this.direction = direction;

            this.IsActive = true; // It's active when created
        }

        /// <summary>
        /// This 'Update' method replaces the 'Move()' method from your diagram.
        /// It's called every frame to update the projectile's position.
        /// </summary>
        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            // Move the projectile
            Position += direction * speed;

            // Deactivate the projectile if it goes off-screen
            if (Position.Y < -Texture.Height || Position.Y > graphicsDevice.Viewport.Height)
            {
                this.IsActive = false;
            }
        }

        /// <summary>
        /// This method replaces the 'Hit(Enemy target)' logic.
        /// When a collision is detected (in your main game loop),
        /// this method is called to deactivate the projectile.
        /// </summary>
        public void OnHit()
        {
            this.IsActive = false;
        }

        /// <summary>
        /// Draws the projectile to the screen.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(
                this.Texture,
                destinationRectangle,
                Color.Yellow // Projectiles will be yellow
            );
        }
    }
}