using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Scenes
{
    public abstract class Scene
    {
        protected UIManager uIManager;
        protected GraphicsDevice GraphicsDevice { get; set; }

        public UIManager UIManager => uIManager;

        public void SetGraphicsManager(GraphicsDevice graphics) => GraphicsDevice = graphics;

        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void DrawGUI(SpriteBatch spriteBatch, GameTime gameTime);

        public Scene()
        {
            uIManager = new();
            SceneManager.Instance.SetMouseVisible(true);
        }
    }
}