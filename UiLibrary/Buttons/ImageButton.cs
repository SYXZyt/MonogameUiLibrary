using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    public class ImageButton : Button
    {
        protected Texture2D heldTexture;
        protected bool isHeld;

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

        public override void Update() => isHeld = IsDown();

        public ImageButton(AABB aabb, Texture2D texture, Texture2D heldTexture) : base(aabb, texture)
        {
            this.heldTexture = heldTexture;
            isHeld = false;
        }
    }
}