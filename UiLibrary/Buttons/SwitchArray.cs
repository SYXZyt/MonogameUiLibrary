using Microsoft.Xna.Framework.Graphics;

namespace UILibrary.Buttons
{
    public sealed class SwitchArray : Element
    {
        private readonly List<Switch> switches;
        private int selectedSwitch;

        public Switch this[int index]
        {
            get => switches[index];
        }

        public void AddSwitch(Switch _switch) => switches.Add(_switch);

        public int GetActiveIndex() => selectedSwitch;

        public void Clear()
        {
            foreach (Switch s in switches)
            {
                s.SetState(false);
            }
        }

        public override void Update()
        {
            bool hasUpdated = false;

            //Check if any switch has been clicked
            for (int i = 0; i < switches.Count; i++)
            {
                if (switches[i].IsClicked())
                {
                    //If this switch is already selected, unselect it
                    if (switches[i].State)
                    {
                        switches[i].SetState(false);
                        break;
                    }

                    selectedSwitch = i;
                    switches[i].SetState(true);
                    hasUpdated = true;
                    break;
                }
            }

            //If we have used a new selected, we need to make sure all other switches are unselected
            if (hasUpdated)
            {
                for (int i = 0; i < switches.Count; i++)
                {
                    if (i != selectedSwitch) switches[i].SetState(false);
                }
            }

            //Now check if the active is no longer active and update it
            if (selectedSwitch > -1 && !switches[selectedSwitch].State) selectedSwitch = -1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Switch s in switches) s.Draw(spriteBatch);
        }

        public SwitchArray()
        {
            switches = new();
            selectedSwitch = -1;
        }
    }
}