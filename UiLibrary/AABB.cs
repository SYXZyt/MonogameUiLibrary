using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary
{
    /// <summary>
    /// Object representing a rectangular based hitbox. <see cref="AABB"/> is a simple method of hit detection, but does not work for non-rectangular shapes and does not support rotations.
    /// </summary>
    public sealed class AABB
    {
        private short x;
        private short y;
        private readonly short width;
        private readonly short height;

        /// <summary>
        /// X position of top-left corner
        /// </summary>
        public short X => x;

        /// <summary>
        /// Y position of top-left corner
        /// </summary>
        public short Y => y;

        /// <summary>
        /// How wide the hitbox is
        /// </summary>
        public short Width => width;

        /// <summary>
        /// How tall the hitbox is
        /// </summary>
        public short Height => height;

        public static bool operator ==(AABB lhs, AABB rhs) => lhs.Equals(rhs);

        public static bool operator !=(AABB lhs, AABB rhs) => !lhs.Equals(rhs);

        public static explicit operator Rectangle(AABB aabb) => new(aabb.x, aabb.y, aabb.width, aabb.height);
        public static explicit operator AABB((int x, int y) position) => new((short)position.x, (short)position.y, 1, 1);

        public void Move(Vector2 newPos)
        {
            x = (short)newPos.X;
            y = (short)newPos.Y;
        }

        public override bool Equals(object obj)
        {
           if (obj is null) return false;
            if (obj is not AABB) return false;

            AABB other = obj as AABB;

            return (
                x == other.x &&
                y == other.y &&
                width == other.width &&
                height == other.height);
        }

        public override int GetHashCode()
        {
            //In order to calculate a hash code, we will generate a unique string to this object, and get the hashcode if that
            string s = ToString();
            return s.GetHashCode();
        }

        public override string ToString() => $"{x}, {y}, {width}, {height}";

        /// <summary>
        /// Check if two <see cref="AABB"/> objects have collided
        /// </summary>
        /// <param name="a">The first object to check</param>
        /// <param name="b">The second object</param>
        /// <returns>True if collision occured</returns>
        public static bool CollisionCheck(AABB a, AABB b)
        {
            //This algorithm only checks if the top left of b is overlapping a
            //This needs improving to allow all other corners to collide
            if (
                a.x < b.x + b.width &&
                a.x + a.width > b.x &&
                a.y < b.y + b.height &&
                a.y + a.height > b.y
                )
            {
                return true;
            }

            return false;
        }

        public void DrawDebugOverlay(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, float opacity)
        {
            Texture2D texture = Extension.CreateTexture(graphicsDevice, width, height, c => Color.Red);
            texture.Draw(new Vector2(x, y), spriteBatch, Color.White * opacity);
        }

        /// <summary>
        /// Check if another <see cref="AABB"/> is overlapping this
        /// </summary>
        /// <param name="other">The <see cref="AABB"/> to check for a collision with</param>
        /// <returns>True if collision occurred</returns>
        public bool CollisionCheck(AABB other) => CollisionCheck(this, other);

        /// <summary>
        /// Get the centre point of this <see cref="AABB"/>
        /// </summary>
        /// <returns>A <see cref="Vector2"/> storing the centre of this <see cref="AABB"/></returns>
        public Vector2 Centre() => new(x + (width / 2), y + (height / 2));

        public static explicit operator Vector2(AABB aabb) => new(aabb.X, aabb.Y); 

        public AABB()
        {
            x = y = width = height = 0;
        }

        public AABB(short x, short y, short width, short height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }
}