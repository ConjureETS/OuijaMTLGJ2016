using UnityEngine;
using System.Collections;

namespace InputHandler
{
    //TODO: When we will be ready to read raw inputs from a file, we need this to simply be generic "BUTTON_ONE, BUTTON_TWO, etc."

    public class XboxInputConstants
    {
        // These buttons will eventually map to controls saved in a file
        public enum Buttons
        {
            A,
            B,
            X,
            Y,
            LeftShoulder,
            RightShoulder,
            Back,
            Start,
            LeftStick,
            RightStick,
            DPadLeft,
            DPadRight,
            DPadUp,
            DPadDown,
        }

        public enum Axis
        {
            LeftStickLeft,
            LeftStickRight,
            LeftStickUp,
            LeftStickDown,
            RightStickLeft,
            RightStickRight,
            RightStickUp,
            RightStickDown,
            TriggerLeft,
            TriggerRight
        }
    }
}
