using UILibrary;
using UILibrary.IO;
using UILibrary.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace UIDemo
{
    internal sealed class Demo : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private readonly SpriteBatch spriteBatch;
        private readonly UIManager uIManager;

        private Texture2D buttonUnclicked;
        private Texture2D buttonClicked;

        private ImageButton button;
        private SpriteFont mainFont;
        private Label label;
        private Label fps;

        private Switch switch1;
        private Switch switch2;
        private SwitchArray switchArray;

        private ImageTextButton textButton;

        private readonly FPS frameRate;

        int i = 0;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            uIManager.Draw(spriteBatch);
            label.DrawWithShadow(spriteBatch);
            fps.DrawWithShadow(spriteBatch);

            switchArray.Draw(spriteBatch);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            i++;
            frameRate.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            MouseController.Update();
            KeyboardController.Update();
            uIManager.Update();
            fps.SetLabelText($"{Math.Round(Math.Clamp(frameRate.AvgFps, 0, 999), 3)}");
            textButton.Label.SetScale((float)(textButton.Label.Scale + (Math.Sin(i / 8) * 0.1)));

            switchArray.Update();

            if (button.IsClicked()) Exit();

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            buttonUnclicked = Texture2D.FromFile(GraphicsDevice, @"Content\button_0.png");
            buttonClicked = Texture2D.FromFile(GraphicsDevice, @"Content\button_1.png");

            mainFont = SpriteFontLoader.LoadFontFromFile(@"Content\Pixeled.ttf", GraphicsDevice);
            button = new ImageButton(new(10, 10, 64, 64), buttonUnclicked, buttonClicked);

            label = new("EXIT", 1f, button.Centre, Color.White, mainFont, Origin.MIDDLE_CENTRE, 0);

            fps = new("999", 1.5f, new(graphics.PreferredBackBufferWidth, 0), new(64, 255, 0), mainFont, Origin.TOP_RIGHT, 0);

            AABB otherButton = new(100, 100, 128, 128);
            textButton = new(otherButton, buttonUnclicked, buttonClicked, "Text", 1.6f, mainFont);

            switch1 = new(new(500, 500, 64, 64), buttonUnclicked, buttonClicked);
            switch2 = new(new(564, 500, 64, 64), buttonUnclicked, buttonClicked);

            switchArray.AddSwitch(switch1);
            switchArray.AddSwitch(switch2);

            uIManager.Add(textButton);
            uIManager.Add(button);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            buttonClicked.Dispose();
            buttonUnclicked.Dispose();

            base.UnloadContent();
        }

        public Demo()
        {
            graphics = new(this)
            {
                PreferredBackBufferHeight = 720,
                PreferredBackBufferWidth = 1280,
            }; graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            spriteBatch = new(GraphicsDevice);
            uIManager = new();
            IsMouseVisible = true;
            frameRate = new();
            switchArray = new();
        }
    }
}
