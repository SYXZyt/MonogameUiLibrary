using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace UILibrary.Buttons
{
    /// <summary>
    /// A collection of switches all bound together
    /// </summary>
    public sealed class SwitchArray : Element, IEnumerable
    {
        private readonly List<Switch> switches;
        private int selectedSwitch;

        /// <summary>
        /// Get the switch at a specific index
        /// </summary>
        /// <param name="index">The index to get</param>
        /// <returns>The switch at the given index</returns>
        public Switch this[int index]
        {
            get => switches[index];
        }

        /// <summary>
        /// Add a new switch to the collection
        /// </summary>
        /// <param name="_switch">The switch to add</param>
        public void AddSwitch(Switch _switch) => switches.Add(_switch);

        /// <summary>
        /// Get the index of the current switch clicked
        /// </summary>
        /// <returns>The index in the collection or -1 if no switches clicked</returns>
        public int GetActiveIndex() => selectedSwitch;

        /// <summary>
        /// Force a switch to become the active one
        /// </summary>
        /// <param name="selectedSwitch">The index of the switch to set as active</param>
        public void SetActiveIndex(int selectedSwitch)
        {
            Clear();
            this.selectedSwitch = selectedSwitch;
            switches[selectedSwitch].SetState(true);
        }

        /// <summary>
        /// Unclick all switches
        /// </summary>
        public void Clear()
        {
            foreach (Switch s in switches)
            {
                s.SetState(false);
            } selectedSwitch = -1;
        }

        /// <summary>
        /// Update the collection
        /// </summary>
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

        /// <summary>
        /// Draw all of the loaded switches
        /// </summary>
        /// <param name="spriteBatch">The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> to use to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Switch s in switches) s.Draw(spriteBatch);
        }

        public IEnumerator<Switch> GetEnumerator()
        {
            foreach (Switch s in switches) yield return s;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Create a new switch array
        /// </summary>
        public SwitchArray()
        {
            switches = new();
            selectedSwitch = -1;
        }
    }
}