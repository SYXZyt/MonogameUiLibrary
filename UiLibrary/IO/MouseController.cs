using Microsoft.Xna.Framework.Input;

namespace UILibrary.IO
{
    /// <summary>
    /// Class to store helper methods for mouse IO
    /// </summary>
    public static class MouseController
    {
        /// <summary>
        /// Enum storing each mouse button
        /// </summary>
        public enum MouseButton
        {
            LEFT,
            MIDDLE,
            RIGHT,
        }

        private static MouseState lastState;
        private static MouseState thisState;

        /// <summary>
        /// Get the position of the mouse
        /// </summary>
        /// <returns>The position of the mouse as a tuple</returns>
        public static (int x, int y) GetMousePosition()
        {
            int x = Mouse.GetState().X;
            int y = Mouse.GetState().Y;
            return (x, y);
        }

        /// <summary>
        /// Update the state of the mouse
        /// </summary>
        public static void Update()
        {
            lastState = thisState;
            thisState = Mouse.GetState();
        }

        /// <summary>
        /// Check if a button was pressed just this frame
        /// </summary>
        /// <param name="whichButton">The button to check for</param>
        /// <returns>True if it was just pressed</returns>
        public static bool IsPressed(MouseButton whichButton) => whichButton switch
        {
            MouseButton.LEFT => thisState.LeftButton == ButtonState.Pressed && lastState.LeftButton == ButtonState.Released,
            MouseButton.MIDDLE => thisState.MiddleButton == ButtonState.Pressed && lastState.MiddleButton == ButtonState.Released,
            MouseButton.RIGHT => thisState.RightButton == ButtonState.Pressed && lastState.RightButton == ButtonState.Released,
            _ => false,
        };

        /// <summary>
        /// Check if a button was released just this frame
        /// </summary>
        /// <param name="whichButton">The button to check for</param>
        /// <returns>True if it was just released</returns>
        public static bool IsReleased(MouseButton whichButton) => whichButton switch
        {
            MouseButton.LEFT => thisState.LeftButton == ButtonState.Released && lastState.LeftButton == ButtonState.Pressed,
            MouseButton.MIDDLE => thisState.MiddleButton == ButtonState.Released && lastState.MiddleButton == ButtonState.Pressed,
            MouseButton.RIGHT => thisState.RightButton == ButtonState.Released && lastState.RightButton == ButtonState.Pressed,
            _ => false,
        };

        /// <summary>
        /// Check if a button is down
        /// </summary>
        /// <param name="whichButton">The button to check for</param>
        /// <returns>True if it is down</returns>
        public static bool IsDown(MouseButton whichButton) => whichButton switch
        {
            MouseButton.LEFT => thisState.LeftButton == ButtonState.Pressed,
            MouseButton.MIDDLE => thisState.MiddleButton == ButtonState.Pressed,
            MouseButton.RIGHT => thisState.RightButton == ButtonState.Pressed,
            _ => false,
        };

        /// <summary>
        /// Check if a button is up
        /// </summary>
        /// <param name="whichButton">The button to check for</param>
        /// <returns>True if it is up</returns>
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