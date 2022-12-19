using Microsoft.Xna.Framework.Input;

namespace UILibrary.IO
{
    public static class MouseController
    {
        public enum MouseButton
        {
            LEFT,
            MIDDLE,
            RIGHT,
        }

        private static MouseState lastState;
        private static MouseState thisState;

        public static (int x, int y) GetMousePosition()
        {
            int x = Mouse.GetState().X;
            int y = Mouse.GetState().Y;
            return (x, y);
        }

        public static void Update()
        {
            lastState = thisState;
            thisState = Mouse.GetState();
        }

        public static bool IsPressed(MouseButton whichButton) => whichButton switch
        {
            MouseButton.LEFT => thisState.LeftButton == ButtonState.Pressed && lastState.LeftButton == ButtonState.Released,
            MouseButton.MIDDLE => thisState.MiddleButton == ButtonState.Pressed && lastState.MiddleButton == ButtonState.Released,
            MouseButton.RIGHT => thisState.RightButton == ButtonState.Pressed && lastState.RightButton == ButtonState.Released,
            _ => false,
        };

        public static bool IsReleased(MouseButton whichButton) => whichButton switch
        {
            MouseButton.LEFT => thisState.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed,
            MouseButton.MIDDLE => thisState.MiddleButton == ButtonState.Released && lastState.MiddleButton == ButtonState.Pressed,
            MouseButton.RIGHT => thisState.RightButton == ButtonState.Released && lastState.RightButton == ButtonState.Pressed,
            _ => false,
        };

        public static bool IsDown(MouseButton whichButton) => whichButton switch
        {
            MouseButton.LEFT => thisState.LeftButton == ButtonState.Pressed,
            MouseButton.MIDDLE => thisState.MiddleButton == ButtonState.Pressed,
            MouseButton.RIGHT => thisState.RightButton == ButtonState.Pressed,
            _ => false,
        };

        public static bool IsUp(MouseButton whichButton) => whichButton switch
        {
            MouseButton.LEFT => thisState.LeftButton == ButtonState.Released,
            MouseButton.MIDDLE => thisState.MiddleButton == ButtonState.Released,
            MouseButton.RIGHT => thisState.RightButton == ButtonState.Released,
            _ => false,
        };

        static MouseController()
        {
            lastState = thisState = Mouse.GetState();
        }
    }
}