using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    public sealed class SwitchArray : Element
    {
        private readonly List<Switch> switches;

        public Switch this[int index]
        {
            get => switches[index];
        }

        public void AddSwitch(Switch _switch) => switches.Add(_switch);

        public int GetActiveIndex()
        {
            for (int i = 0; i < switches.Count; i++)
            {
                if (switches[i].IsClicked()) return i;
            }
            return -1;
        }

        public void Clear()
        {
            foreach (Switch s in switches)
            {
                s.SetState(false);
            }
        }

        public override void Update()
        {
            int index = -1;
            for (int i = 0; i < switches.Count; i++)
            {
                if (switches[i].IsClicked())
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) return;

            for (int i = 0; i < switches.Count; i++)
            {
                if (i == index) switches[i].SetState(!switches[i].State);
                else switches[i].SetState(false);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Switch s in switches) s.Draw(spriteBatch);
        }

        public SwitchArray()
        {
            switches = new();
        }
    }
}