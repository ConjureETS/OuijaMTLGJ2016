using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InputHandler
{
    public struct InputMap
    {
        public List<InputToActionMap>[] ButtonsToActionsMap;
        public List<InputToActionMap>[] ButtonsToStatesMap;
        public List<InputToActionMap>[] AxisToRangesMap;
    }

    public struct InputToActionMap
    {
        public int input;
        public string action;
    }
}
