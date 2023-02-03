using UILibrary.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Scenes
{
    /// <summary>
    /// Class which will update, draw and handle scenes in general
    /// </summary>
    public class SceneManager : Game
    {
        /// <summary>
        /// Get the singleton instance of the <see cref="UILibrary.Scenes.SceneManager"/>
        /// </summary>
        public static SceneManager Instance { get; protected set; } = null;

        /// <summary>
        /// Get whether the <see cref="UILibrary.Scenes.SceneManager"/> has ownership of the <see cref="UIManager"/>
        /// </summary>
        public bool ManagedUIManager { get; set; } = true; //By default we always want to automatically update/draw the manager
        
        protected readonly Dictionary<string, Scene> scenes;
        protected Scene active;

        protected SpriteBatch spriteBatch;
        public readonly GraphicsDeviceManager graphics;

        public delegate void ExitMethod();
        private static List<ExitMethod> exitMethods;

        /// <summary>
        /// Add a new scene to the manager
        /// </summary>
        /// <param name="sceneName">The name of the scene</param>
        /// <param name="scene">The scene to load</param>
        public void AddScene(string sceneName, Scene scene) => scenes[sceneName] = scene;

        /// <summary>
        /// Set a new scene to be the active one while automatically unloading the currently active scene
        /// </summary>
        /// <param name="sceneName">The scene to load</param>
        public void LoadScene(string sceneName)
        {
            Console.WriteLine($"LDSCENE: nameof '{sceneName}'");

            active?.UnloadContent();
            active = scenes[sceneName];
            active?.SetGraphicsManager(GraphicsDevice);
            active?.LoadContent();
        }

        /// <summary>
        /// Force a new scene to be active without unloading or loading the current scene
        /// </summary>
        /// <param name="sceneName"></param>
        public void SetScene(string sceneName)
        {
            active = scenes[sceneName];
        }

        /// <summary>
        /// Set whether the mouse is visible
        /// </summary>
        /// <param name="isMouseVisible">If the mouse should be visible or not</param>
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

        /// <summary>
        /// Add a new method to execute upon exit
        /// </summary>
        /// <param name="method">The method to add</param>
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