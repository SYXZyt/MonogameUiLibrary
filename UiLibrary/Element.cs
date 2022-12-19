using Microsoft.Xna.Framework.Graphics;

namespace UILibrary
{
    /// <summary>
    /// Base class for all UI elements
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Update this <see cref="Element"/>
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// Draw this <see cref="Element"/>
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> to draw to</param>
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}