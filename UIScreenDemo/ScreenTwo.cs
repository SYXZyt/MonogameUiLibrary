using UILibrary;
using UILibrary.IO;
using UILibrary.Scenes;
using UILibrary.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UIScreenDemo
{
    internal class ScreenTwo : Scene
    {
        private ProgressBar progressBar;
        const int max = int.MaxValue / 10000000;
        int i = 0;

        private Texture2D unfilled;
        private Texture2D filled;
        private Texture2D background;

        private Texture2D unclicked;
        private Texture2D clicked;

        private ImageButton resetButton;

        private Textbox textbox;

        private SpriteFont font;

        public override void LoadContent()
        {
            Console.WriteLine($"LOAD: {typeof(ScreenTwo).Name}");

            unfilled = Texture2D.FromFile(GraphicsDevice, @"Content\unfilled.png");
            filled = Texture2D.FromFile(GraphicsDevice, @"Content\filled.png");
            unclicked = Texture2D.FromFile(GraphicsDevice, @"Content\u.png");
            clicked = Texture2D.FromFile(GraphicsDevice, @"Content\c.png");
            background = Texture2D.FromFile(GraphicsDevice, @"Content\b.png");
            progressBar = new(filled, unfilled, new(8, 720 - 16, 1280 - 16, 8), max);

            resetButton = new(new(100, 100, 64, 64), unclicked, clicked);
            UIManager.Add(resetButton);

            font = SpriteFontLoader.LoadFontFromFile(@"Content\Pixeled.ttf", GraphicsDevice);

            textbox = new(new(300, 300), font, background, 20, 1.5f);
            textbox.SetActive(true);
            textbox.SetFocus(true);
        }

        public override void UnloadContent()
        {
            Console.WriteLine($"UNLOAD: {typeof(ScreenTwo).Name}");
        }

        public override void Update(GameTime gameTime)
        {
            textbox.Update();

            progressBar.UpdateProgress(i);
            if (i <= max)
            {
                i++;
            }

            if (resetButton.IsClicked()) i = 0;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            progressBar.Draw(spriteBatch);

            textbox.Draw(spriteBatch);
        }
    }
}