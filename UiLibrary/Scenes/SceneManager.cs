using UILibrary.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Scenes
{
    public class SceneManager : Game
    {
        public static SceneManager Instance { get; protected set; } = null;
        public bool ManagedUIManager { get; set; } = true; //By default we always want to automatically update/draw the manager
        
        protected readonly Dictionary<string, Scene> scenes;
        protected Scene active;

        protected SpriteBatch spriteBatch;
        public readonly GraphicsDeviceManager graphics;

        public delegate void ExitMethod();
        private static List<ExitMethod> exitMethods;

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

            if (exitMethods.Count > 0) Console.WriteLine("Executing 3rd party exit methods");
            foreach (ExitMethod exitMethod in exitMethods) exitMethod.Invoke();
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

            if (ManagedUIManager && IsActive) active?.UIManager.Update();
            active?.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.Deferred, blendState: BlendState.NonPremultiplied);
            active?.Draw(spriteBatch, gameTime);
            active?.DrawGUI(spriteBatch, gameTime);
            if (ManagedUIManager) active?.UIManager.Draw(spriteBatch);
            spriteBatch.End();
        }

        public static void AddNewExitMethod(ExitMethod method) => exitMethods.Add(method);

        public SceneManager(Vector2 screenSize)
        {
            graphics = new(this)
            {
                PreferredBackBufferWidth = (int)screenSize.X,
                PreferredBackBufferHeight = (int)screenSize.Y,
            };
            scenes = new();

            Instance = this;
            exitMethods = new();
        }
    }
}