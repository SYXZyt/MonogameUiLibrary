using UILibrary.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    public class Switch : Element
    {
        protected readonly AABB aabb;
        protected readonly Texture2D unclicked;
        protected readonly Texture2D clicked;
        protected bool state;

        public Vector2 Centre => new(aabb.X + (aabb.Width / 2), aabb.Y + (aabb.Height / 2));

        public AABB AABB => aabb;
        public Texture2D Clicked => clicked;
        public Texture2D Unclicked => unclicked;
        public bool State => state;

        public void SetState(bool state) => this.state = state;
        public void SetState(Switch s) => state = s.state;

        public bool IsClicked() => (aabb.CollisionCheck((AABB)MouseController.GetMousePosition()) && MouseController.IsPressed(MouseController.MouseButton.LEFT));

        public override void Update()
        {
            if (IsClicked()) state = !state;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture = state ? clicked : unclicked;
            spriteBatch.Draw(texture, (Rectangle)aabb, Color.White);
        }

        public Switch(AABB aabb, Texture2D unclicked, Texture2D clicked, bool state = false)
        {
            this.aabb = aabb;
            this.unclicked = unclicked;
            this.clicked = clicked;
            this.state = state;
        }
    }
}