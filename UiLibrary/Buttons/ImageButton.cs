using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    /// <summary>
    /// A class representing a button which has different images based on if the button is held down or not
    /// </summary>
    public class ImageButton : Button
    {
        protected Texture2D heldTexture;
        protected bool isHeld;

        /// <summary>
        /// Draw the button to the screen
        /// </summary>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> used to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isHeld && heldTexture is not null)
            {
                spriteBatch.Draw(heldTexture, (Rectangle)aabb, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, (Rectangle)aabb, Color.White);
            }
        }

        /// <summary>
        /// Update the button object
        /// </summary>
        public override void Update() => isHeld = IsDown();

        /// <summary>
        /// Create a new image button object
        /// </summary>
        /// <param name="aabb">The bounding box of the button</param>
        /// <param name="texture">The texture of the button when idle</param>
        /// <param name="heldTexture">The texture of the button when held</param>
        public ImageButton(AABB aabb, Texture2D texture, Texture2D heldTexture) : base(aabb, texture)
        {
            this.heldTexture = heldTexture;
            isHeld = false;
        }
    }
}