using System;
using OpenTK;
using GameBox.Context;

namespace GameBox.Services
{
    internal static class LogicController
    {
        internal enum MainState
        {
            Init,
            LoadGlobal,
        }

        private static MainState mState = MainState.Init;

        internal static void Init()
        {
        }

        internal static void Update(FrameEventArgs e)
        {
            switch (mState)
            {
                case MainState.Init:
                    GlobalContext.Init();
                    mState = MainState.LoadGlobal;
                    break;
                case MainState.LoadGlobal:
//                    GlobalContext.MainApp.
                    break;
            }
        }
    }
}
