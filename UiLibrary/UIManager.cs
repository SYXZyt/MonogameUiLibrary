using Microsoft.Xna.Framework.Graphics;

namespace UILibrary
{
    public sealed class UIManager
    {
        private readonly List<Element> elements;

        public Element Get(int index)
        {
            //Ensure that a valid value is provided
            if (index >= elements.Count) return null;
            if (index < 0) return null;

            return elements[index];
        }

        public void Add(Element button) => elements.Add(button);

        public void Update()
        {
            foreach (Element b in elements) b.Update();
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
        }
    }
}