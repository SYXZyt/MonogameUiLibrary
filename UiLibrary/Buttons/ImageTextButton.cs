using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    public class ImageTextButton : ImageButton
    {
        protected Label label;
        private Vector2 labelOrigPos;

        public Label Label => label;

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

        public ImageTextButton(AABB aabb, Texture2D texture, Texture2D heldTexture, string text, float textScale, SpriteFont font) : base(aabb, texture, heldTexture)
        {
            label = new(text, textScale, Centre, Color.White, font, Origin.MIDDLE_CENTRE, 0f);
            labelOrigPos = label.Position;
        }
    }
}