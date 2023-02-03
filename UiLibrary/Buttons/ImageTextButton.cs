using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    /// <summary>
    /// Class representing an <see cref="UILibrary.Buttons.ImageButton"/> but with text on the button instead
    /// </summary>
    public class ImageTextButton : ImageButton
    {
        protected Label label;
        private Vector2 labelOrigPos;

        public Label Label => label;

        /// <summary>
        /// Draw the button to the screen
        /// </summary>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> used to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Draw the button first, so we don't draw the text under it
            base.Draw(spriteBatch);

            if (isHeld)
            {
                Vector2 offset = new(label.Scale);
                label.MoveLabel(labelOrigPos + offset);
            }

            label.DrawWithShadow(spriteBatch);
            label.MoveLabel(labelOrigPos);
        }

        /// <summary>
        /// Create a new image text button
        /// </summary>
        /// <param name="aabb">The bounding box of the button</param>
        /// <param name="texture">The texture of the button when idle</param>
        /// <param name="heldTexture">The texture of the button when held</param>
        /// <param name="text">The text to display on the button</param>
        /// <param name="textScale">The scale of the text</param>
        /// <param name="font">The font to draw with</param>
        public ImageTextButton(AABB aabb, Texture2D texture, Texture2D heldTexture, string text, float textScale, SpriteFont font) : base(aabb, texture, heldTexture)
        {
            label = new(text, textScale, Centre, Color.White, font, Origin.MIDDLE_CENTRE, 0f);
            labelOrigPos = label.Position;
        }
    }
}