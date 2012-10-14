using GameBox.Input;
using OpenTK;
using OpenTK.Input;
using System;

namespace GameBox.Events
{
    public class UpdateFrameEvent
    {
        private FrameEventArgs args;
        private KeyboardDevice keyboard;

        internal UpdateFrameEvent(FrameEventArgs args_, KeyboardDevice keyboard_)
        {
            args = args_;
            keyboard = keyboard_;
        }

        public bool getKeyState(GBKey key)
        {
            return keyboard[(Key)key];
        }
    }
}
