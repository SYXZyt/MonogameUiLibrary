using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary
{
    public sealed class ProgressBar
    {
        private readonly Texture2D filled;
        private readonly Texture2D unfilled;

        private readonly AABB aabb;

        private int value;
        private readonly int maxValue;
        private readonly int lengthInPixels;

        public void UpdateProgress(int value) => this.value = value;
        
        public void Draw(SpriteBatch spriteBatch)
        {
            float progress = (float)value / maxValue * 100f;
            int filledProgress = (int)progress * lengthInPixels / 100;

            spriteBatch.Draw(filled, new Rectangle(aabb.X, aabb.Y, filledProgress, aabb.Height), Color.White);
            spriteBatch.Draw(unfilled, new Rectangle(aabb.X + filledProgress, aabb.Y, lengthInPixels-filledProgress, aabb.Height), Color.White);
        }

        public ProgressBar(Texture2D filled, Texture2D unfilled, AABB aabb, int maxValue)
        {
            this.filled = filled;
            this.unfilled = unfilled;
            this.aabb = aabb;
            this.lengthInPixels = aabb.Width;
            this.maxValue = maxValue;

            value = 0;
        }
    }
}