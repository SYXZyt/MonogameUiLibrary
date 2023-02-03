using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary
{
    /// <summary>
    /// A class to represent a progress bar
    /// </summary>
    public sealed class ProgressBar
    {
        private readonly Texture2D filled;
        private readonly Texture2D unfilled;

        private readonly AABB aabb;

        private int value;
        private readonly int maxValue;
        private readonly int lengthInPixels;

        /// <summary>
        /// Update the progress of the bar
        /// </summary>
        /// <param name="value">The value to update to</param>
        public void UpdateProgress(int value) => this.value = value;

        /// <summary>
        /// Draw the progress bar to the screen
        /// </summary>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> used to draw</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            float progress = (float)value / maxValue * 100f;
            int filledProgress = (int)progress * lengthInPixels / 100;

            spriteBatch.Draw(filled, new Rectangle(aabb.X, aabb.Y, filledProgress, aabb.Height), Color.White);
            spriteBatch.Draw(unfilled, new Rectangle(aabb.X + filledProgress, aabb.Y, lengthInPixels-filledProgress, aabb.Height), Color.White);
        }

        /// <summary>
        /// Create a new progress bar
        /// </summary>
        /// <param name="filled">The texture of the filled portion</param>
        /// <param name="unfilled">The texture of the unfilled portion</param>
        /// <param name="aabb">The bounding box of the progress bar</param>
        /// <param name="maxValue">The max value of the inputted data (Does not need to be 100. The draw function will automatically calculate the percentage)</param>
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