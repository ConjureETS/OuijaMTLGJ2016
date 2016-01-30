using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace InputHandler
{
    public class InputContext
    {
        private Dictionary<int, string> _mappedButtons;
        private Dictionary<int, string> _mappedStates;
        private Dictionary<int, string> _mappedAxis;

        private string _name;

        public string Name
        {
            get { return _name; }
        }
        

        public InputContext(string contextName, InputMap inputMap)
        {
            _name = contextName;

            _mappedButtons = new Dictionary<int, string>();
            _mappedStates = new Dictionary<int, string>();
            _mappedAxis = new Dictionary<int, string>();

            foreach (List<InputToActionMap> buttonsToActionsMap in inputMap.ButtonsToActionsMap)
            {
                foreach (InputToActionMap buttonToActionMap in buttonsToActionsMap)
                {
                    _mappedButtons.Add(buttonToActionMap.input, buttonToActionMap.action);
                }
            }

            foreach (List<InputToActionMap> buttonsToStatesMap in inputMap.ButtonsToStatesMap)
            {
                foreach (InputToActionMap buttonToStateMap in buttonsToStatesMap)
                {
                    _mappedStates.Add(buttonToStateMap.input, buttonToStateMap.action);
                }
            }

            foreach (List<InputToActionMap> axisToRangesMap in inputMap.AxisToRangesMap)
            {
                foreach (InputToActionMap axisToRangeMap in axisToRangesMap)
                {
                    _mappedAxis.Add(axisToRangeMap.input, axisToRangeMap.action);
                }
            }
        }

        public string GetActionForButton(int button)
        {
            return _mappedButtons.ContainsKey(button) ? _mappedButtons[button] : null;
        }

        public string GetStateForButton(int button)
        {
            return _mappedStates.ContainsKey(button) ? _mappedStates[button] : null;
        }

        public string GetRangeForAxis(int axis)
        {
            return _mappedAxis.ContainsKey(axis) ? _mappedAxis[axis] : null;
        }
    }
}