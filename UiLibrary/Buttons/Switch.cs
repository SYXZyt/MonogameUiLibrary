using UILibrary.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    /// <summary>
    /// Class storing a binary state switch
    /// </summary>
    public class Switch : Element
    {
        protected readonly AABB aabb;
        protected readonly Texture2D unclicked;
        protected readonly Texture2D clicked;
        protected bool state;

        /// <summary>
        /// The centre screen position of the bounding box
        /// </summary>
        public Vector2 Centre => new(aabb.X + (aabb.Width / 2), aabb.Y + (aabb.Height / 2));

        /// <summary>
        /// The bounding box of the switch
        /// </summary>
        public AABB AABB => aabb;

        /// <summary>
        /// The texture of the button when clicked
        /// </summary>
        public Texture2D Clicked => clicked;

        /// <summary>
        /// The texture of the button when unclicked
        /// </summary>
        public Texture2D Unclicked => unclicked;

        /// <summary>
        /// True if the switch is clicked
        /// </summary>
        public bool State => state;

        /// <summary>
        /// Set the state of the switch
        /// </summary>
        /// <param name="state">The new state of the switch</param>
        public void SetState(bool state) => this.state = state;

        /// <summary>
        /// Set the state of this switch to that of another switch
        /// </summary>
        /// <param name="s">The switch's value to copy</param>
        public void SetState(Switch s) => state = s.state;

        /// <summary>
        /// Get if the mouse has clicked the switch
        /// </summary>
        /// <returns>True if the switch has been clicked this frame</returns>
        public bool IsClicked() => (aabb.CollisionCheck((AABB)MouseController.GetMousePosition()) && MouseController.IsPressed(MouseController.MouseButton.LEFT));

        /// <summary>
        /// Update the switch
        /// </summary>
        public override void Update()
        {
            if (IsClicked()) state = !state;
        }

        /// <summary>
        /// Draw the switch to the screen
        /// </summary>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> to use to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = state ? clicked : unclicked;
            spriteBatch.Draw(texture, (Rectangle)aabb, Color.White);
        }

        /// <summary>
        /// Create a new switch object
        /// </summary>
        /// <param name="aabb">The bounding box of the switch</param>
        /// <param name="unclicked">The texture when unclicked</param>
        /// <param name="clicked">The texture when clicked</param>
        /// <param name="state">The default state of the switch</param>
        public Switch(AABB aabb, Texture2D unclicked, Texture2D clicked, bool state = false)
        {
            this.aabb = aabb;
            this.unclicked = unclicked;
            this.clicked = clicked;
            this.state = state;
        }
    }
}