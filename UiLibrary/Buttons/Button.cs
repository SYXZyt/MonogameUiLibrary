using UILibrary.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    /// <summary>
    /// Class storing a clickable button
    /// </summary>
    public class Button : Element
    {
        protected readonly AABB aabb;
        protected readonly Texture2D texture;

        /// <summary>
        /// Get the centre point of the button
        /// </summary>
        public Vector2 Centre => new(aabb.X + (aabb.Width / 2), aabb.Y + (aabb.Height / 2));

        /// <summary>
        /// Get the bounding box of the button
        /// </summary>
        public AABB AABB => aabb;
        
        /// <summary>
        /// Get the base texture of the button
        /// </summary>
        public Texture2D Texture => texture;

        /// <summary>
        /// Has the button been clicked this frame
        /// </summary>
        /// <returns></returns>
        public bool IsClicked()
        {
            //The plan is the get the position of the mouse, and check if that position falls inside of our aabb
            //Step 1, get the position of the mouse
            AABB mouse = (AABB)MouseController.GetMousePosition();

            //Check for collision with the button, and this
            //We also need to check that the mouse cursor has also clicked this frame
            return aabb.CollisionCheck(mouse) && MouseController.IsReleased(MouseController.MouseButton.LEFT);
        }

        /// <summary>
        /// Is the button being held down
        /// </summary>
        public bool IsDown() => aabb.CollisionCheck((AABB)MouseController.GetMousePosition()) && MouseController.IsDown(MouseController.MouseButton.LEFT);

        /// <summary>
        /// Is the button not being held down
        /// </summary>
        public bool IsUp() => !aabb.CollisionCheck((AABB)MouseController.GetMousePosition()) || MouseController.IsUp(MouseController.MouseButton.LEFT);

        /// <summary>
        /// Has the button been let go of this frame
        /// </summary>
        public bool IsReleased() => aabb.CollisionCheck((AABB)MouseController.GetMousePosition()) && MouseController.IsReleased(MouseController.MouseButton.LEFT);

        /// <summary>
        /// Draw the button to the screen
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to use</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, (Rectangle)aabb, Color.White);
        }

        /// <summary>
        /// Update the button object
        /// </summary>
        public override void Update() { }

        /// <summary>
        /// Create a new button object
        /// </summary>
        /// <param name="aabb">The bounding box of the button</param>
        /// <param name="texture">The texture to use for the button</param>
        public Button(AABB aabb, Texture2D texture)
        {
            this.aabb = aabb;
            this.texture = texture;
        }
    }
}