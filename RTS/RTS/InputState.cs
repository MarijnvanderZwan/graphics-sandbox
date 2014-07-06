
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
namespace RTS
{
    public class InputState
    {
        public static MouseState oldMouseState;
        static KeyboardState oldKeyboardState;

        public static MouseState MouseState
        {
            get;
            private set;
        }

        public static KeyboardState KeyboardState
        {
            get;
            private set;
        }

        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(MouseState.X, MouseState.Y);
            }
        }

        public static Vector2 OldMousePosition
        {
            get
            {
                return new Vector2(oldMouseState.X, oldMouseState.Y);
            }
        }

        public static Vector2 DeltaMousePosition
        {
            get
            {
                return OldMousePosition - MousePosition;
            }
        }

        public static int DeltaMouseScrollWheelValue
        {
            get
            {
                return MouseState.ScrollWheelValue - oldMouseState.ScrollWheelValue;
            }
        }

        public static bool MouseLeftButtonDown
        {
            get
            {
                return MouseState.LeftButton == ButtonState.Pressed;
            }
        }

        public static bool MouseRightButtonDown
        {
            get
            {
                return MouseState.RightButton == ButtonState.Pressed;
            }
        }

        public static bool MouseLeftButtonPressed
        {
            get
            {
                return MouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed;
            }
        }

        public static bool MouseRightButtonPressed
        {
            get
            {
                return MouseState.RightButton == ButtonState.Released && oldMouseState.RightButton == ButtonState.Pressed;
            }
        }


        public static bool MouseInClientArea()
        {
            float x = MousePosition.X;
            float y = MousePosition.Y;
            return x >= 0 && x < 1024 && y >= 0 && y < 768;
        }

        public static bool IsKeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && oldKeyboardState.IsKeyUp(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        public static void Initialize()
        {
            MouseState = Mouse.GetState();
            KeyboardState = Keyboard.GetState();
        }

        public static void Update()
        {
            oldMouseState = MouseState;
            oldKeyboardState = KeyboardState;

            MouseState = Mouse.GetState();
            KeyboardState = Keyboard.GetState();
        }
    }
}
