using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Scenes
{
    /// <summary>
    /// A class structure for any scenes that will be used with the <see cref="UILibrary.Scenes.SceneManager"/>
    /// </summary>
    public abstract class Scene
    {
        protected UIManager uIManager;
        protected GraphicsDevice GraphicsDevice { get; set; }

        /// <summary>
        /// The <see cref="UIManager"/> used by this scene
        /// </summary>
        public UIManager UIManager => uIManager;

        /// <summary>
        /// Set the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/> of the scene to a specified one
        /// </summary>
        /// <param name="graphics">The <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/> to set as the active one</param>
        public void SetGraphicsManager(GraphicsDevice graphics) => GraphicsDevice = graphics;

        /// <summary>
        /// Method to load content of the scene
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Method to unload content of the scene
        /// </summary>
        public abstract void UnloadContent();

        /// <summary>
        /// Method to update the scene
        /// </summary>
        /// <param name="gameTime">The <see cref="Microsoft.Xna.Framework.GameTime"/> of this frame</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Method to draw the scene
        /// </summary>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> used to draw this frame</param>
        /// <param name="gameTime">The <see cref="Microsoft.Xna.Framework.GameTime"/> of this frame</param>
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        /// <summary>
        /// Method to draw the scene's GUI
        /// </summary>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> used to draw this frame</param>
        /// <param name="gameTime">The <see cref="Microsoft.Xna.Framework.GameTime"/> of this frame</param>
        public abstract void DrawGUI(SpriteBatch spriteBatch, GameTime gameTime);

        /// <summary>
        /// Create a new scene
        /// </summary>
        public Scene()
        {
            uIManager = new();
            SceneManager.Instance.SetMouseVisible(true);
        }
    }
}