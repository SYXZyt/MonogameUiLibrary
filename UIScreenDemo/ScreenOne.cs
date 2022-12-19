using UILibrary;
using UILibrary.Scenes;
using UILibrary.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UIScreenDemo
{
    internal class ScreenOne : Scene
    {
        private SpriteFont font;
        private string message;

        private Texture2D unclicked;
        private Texture2D clicked;

        private ImageButton nextSceneButton;

        public override void LoadContent()
        {
            Console.WriteLine($"LOAD: {typeof(ScreenOne).Name}");
            
            message = "SCREEN ONE";
            font = SpriteFontLoader.LoadFontFromFile(@"Content\Pixeled.ttf", GraphicsDevice);

            unclicked = Texture2D.FromFile(GraphicsDevice, @"Content\u.png");
            clicked = Texture2D.FromFile(GraphicsDevice, @"Content\c.png");

            nextSceneButton = new(new(200, 100, 64, 64), unclicked, clicked);
            UIManager.Add(nextSceneButton);
        }

        public override void UnloadContent()
        {
            Console.WriteLine($"UNLOAD: {typeof(ScreenOne).Name}");
        }

        public override void Update(GameTime gameTime)
        {
            if (nextSceneButton.IsClicked()) SceneManager.Instance.LoadScene("s2");
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(font, message, new(100, 100), Color.White);
        }

        public ScreenOne() { }
    }
}