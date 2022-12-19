using Microsoft.Xna.Framework.Input;

namespace UILibrary.IO
{
    public static class KeyboardController
    {
        private static KeyboardState lastState;
        private static KeyboardState thisState;

        public static void Update()
        {
            lastState = thisState;
            thisState = Keyboard.GetState();
        }

        public static bool IsPressed(Keys key) => thisState.IsKeyDown(key) && lastState.IsKeyUp(key);

        static KeyboardController()
        {
            lastState = thisState = Keyboard.GetState();
        }
    }
}