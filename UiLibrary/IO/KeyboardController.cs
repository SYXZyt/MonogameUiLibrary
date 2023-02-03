using Microsoft.Xna.Framework.Input;

namespace UILibrary.IO
{
    /// <summary>
    /// Class to store helper methods for keyboard IO
    /// </summary>
    public static class KeyboardController
    {
        private static KeyboardState lastState;
        private static KeyboardState thisState;

        /// <summary>
        /// Update the state of the keyboard
        /// </summary>
        public static void Update()
        {
            lastState = thisState;
            thisState = Keyboard.GetState();
        }

        /// <summary>
        /// Check if a given key was pressed this frame
        /// </summary>
        /// <param name="key">The key to check for</param>
        /// <returns>True if the key was just pressed</returns>
        public static bool IsPressed(Keys key) => thisState.IsKeyDown(key) && lastState.IsKeyUp(key);

        static KeyboardController()
        {
            lastState = thisState = Keyboard.GetState();
        }
    }
}