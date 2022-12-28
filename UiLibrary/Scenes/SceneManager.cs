using UILibrary.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Scenes
{
    public class SceneManager : Game
    {
        public static SceneManager Instance { get; protected set; } = null;
        
        protected readonly Dictionary<string, Scene> scenes;
        protected Scene active;

        protected SpriteBatch spriteBatch;
        public readonly GraphicsDeviceManager graphics;

        public void AddScene(string sceneName, Scene scene) => scenes[sceneName] = scene;
        public void LoadScene(string sceneName)
        {
            Console.WriteLine($"LDSCENE: nameof '{sceneName}'");

            active?.UnloadContent();
            active = scenes[sceneName];
            active?.SetGraphicsManager(GraphicsDevice);
            active?.LoadContent();
        }
        public void SetScene(string sceneName)
        {
            active = scenes[sceneName];
        }

        public void SetMouseVisible(bool isMouseVisible) => IsMouseVisible = isMouseVisible;

        protected override void OnExiting(object sender, EventArgs args)
        {
            active?.UnloadContent();
        }

        protected override void BeginRun()
        {
            active?.SetGraphicsManager(GraphicsDevice);
            active?.LoadContent();
        }
        protected override void LoadContent()
        {
            spriteBatch = new(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseController.Update();
            KeyboardController.Update();

            active?.UIManager.Update();
            active?.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            active?.Draw(spriteBatch, gameTime);
            active?.DrawGUI(spriteBatch, gameTime);
            active?.UIManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        public SceneManager(Vector2 screenSize)
        {
            graphics = new(this)
            {
                PreferredBackBufferWidth = (int)screenSize.X,
                PreferredBackBufferHeight = (int)screenSize.Y,
            };
            scenes = new();

            Instance = this;
        }
    }
}