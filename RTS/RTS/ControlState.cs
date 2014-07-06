using Microsoft.Xna.Framework.Input;

namespace RTS
{
    public class ControlState
    {

        public ControlState()
        {

        }

        public HotKeys HotKey
        {
            get;
            set;
        }

        public void ResetState()
        {
            HotKey = HotKeys.None;
        }

        void PollHotKeys()
        {
            var keys = InputState.KeyboardState;

            if (InputState.IsKeyPressed(Keys.Tab))
            {
                HotKey = HotKeys.Tab;
            }
            if (keys.IsKeyDown(Keys.LeftShift) || keys.IsKeyDown(Keys.RightShift))
            {
                HotKey = HotKeys.Shift;
            }
            if (keys.IsKeyDown(Keys.LeftControl) || keys.IsKeyDown(Keys.RightControl))
            {
                HotKey = HotKeys.Control;
            }
            if (keys.IsKeyDown(Keys.LeftAlt) || keys.IsKeyDown(Keys.RightAlt))
            {
                HotKey = HotKeys.Alt;
            }
        }

        void PollMouseAction()
        {
            /*
            if (HotKey == HotKeys.Shift && InputState.MouseLeftButtonPressed)
            {

            }
            else if (InputState.MouseRightButtonPressed)
            {
                
            }*/
        }

        public void Update()
        {
            ResetState();
            PollHotKeys();
            PollMouseAction();
        }
    }
}
