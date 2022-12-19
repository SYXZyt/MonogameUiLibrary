using UILibrary.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    public class Button : Element
    {
        protected readonly AABB aabb;
        protected readonly Texture2D texture;

        public Vector2 Centre => new(aabb.X + (aabb.Width / 2), aabb.Y + (aabb.Height / 2));

        public AABB AABB => aabb;
        public Texture2D Texture => texture;

        public bool IsClicked()
        {
            //The plan is the get the position of the mouse, and check if that position falls inside of our aabb
            //Step 1, get the position of the mouse
            AABB mouse = (AABB)MouseController.GetMousePosition();

            //Check for collision with the button, and this
            //We also need to check that the mouse cursor has also clicked this frame
            return aabb.CollisionCheck(mouse) && MouseController.IsReleased(MouseController.MouseButton.LEFT);
        }

        public bool IsDown() => aabb.CollisionCheck((AABB)MouseController.GetMousePosition()) && MouseController.IsDown(MouseController.MouseButton.LEFT);
        public bool IsUp() => !aabb.CollisionCheck((AABB)MouseController.GetMousePosition()) || MouseController.IsUp(MouseController.MouseButton.LEFT);
        public bool IsReleased() => aabb.CollisionCheck((AABB)MouseController.GetMousePosition()) && MouseController.IsReleased(MouseController.MouseButton.LEFT);

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, (Rectangle)aabb, Color.White);
        }

        public override void Update() { }

        public Button(AABB aabb, Texture2D texture)
        {
            this.aabb = aabb;
            this.texture = texture;
        }
    }
}