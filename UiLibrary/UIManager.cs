using Microsoft.Xna.Framework.Graphics;
using UILibrary.Scenes;

namespace UILibrary
{
    public sealed class UIManager
    {
        private bool isActive;

        private readonly List<Element> elements;

        private void Activate(object sender, EventArgs args) => isActive = true;
        private void Deactivate(object sender, EventArgs args) => isActive = false;

        public Element Get(int index)
        {
            //Ensure that a valid value is provided
            if (index >= elements.Count) return null;
            if (index < 0) return null;

            return elements[index];
        }

        public void Clear() => elements.Clear();

        public void Add(Element button) => elements.Add(button);

        public void Update()
        {
            foreach (Element b in elements)
                if (isActive)
                    b.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Element b in elements)
            {
                b.Draw(spriteBatch);
            }
        }

        public UIManager()
        {
            elements = new();
            SceneManager.Instance.Activated += Activate;
            SceneManager.Instance.Deactivated += Deactivate;
        }
    }
}