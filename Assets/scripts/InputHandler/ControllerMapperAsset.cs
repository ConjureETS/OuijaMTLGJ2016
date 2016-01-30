using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace InputHandler
{
    // TODO: REFACTOR THE WHOLE CLASS, IT STINKS

    [SerializeField]
    public class ControllerMapperAsset : InputMapperAsset
    {
        [Serializable]
        public class XboxContext
        {
            public string name;
            public XboxAction[] ButtonActions;
            public XboxRange[] AxisRanges;
            public XboxState[] ButtonStates;
        }

        [Serializable]
        public class XboxAction
        {
            public string name;
            public XboxInputConstants.Buttons[] XboxButtons;
            public KeyCode[] KeyboardKeys;
        }

        [Serializable]
        public class XboxRange
        {
            public string name;
            public XboxInputConstants.Axis[] Axis;
            public KeyCode[] KeyboardKeys;
        }

        [Serializable]
        public class XboxState
        {
            public string name;
            public XboxInputConstants.Buttons[] XboxButtons;
            public KeyCode[] KeyboardKeys;
        }

        public XboxContext[] Contexts;

        // TODO: Temporary
        private List<int> _mappedKeyboardKeysAxis;
        private List<int> _mappedKeyboardKeys;
        private List<int> _mappedXboxAxis;
        private List<int> _mappedXboxButtons;

        // Context -> InputMap
        public override Dictionary<string, InputContext> GetMappedContexts()
        {
            _mappedKeyboardKeys = new List<int>();
            _mappedKeyboardKeysAxis = new List<int>();
            _mappedXboxButtons = new List<int>();
            _mappedXboxAxis = new List<int>();

            Dictionary<string, InputContext> mappedContexts = new Dictionary<string, InputContext>();

            foreach (XboxContext xboxContext in Contexts)
            {
                InputMap inputMap = new InputMap();

                inputMap.ButtonsToActionsMap = new List<InputToActionMap>[xboxContext.ButtonActions.Length];
                inputMap.ButtonsToStatesMap = new List<InputToActionMap>[xboxContext.ButtonStates.Length];
                inputMap.AxisToRangesMap = new List<InputToActionMap>[xboxContext.AxisRanges.Length];

                for (int i = 0; i < xboxContext.ButtonActions.Length; i++)
                {
                    XboxAction buttonAction = xboxContext.ButtonActions[i];

                    inputMap.ButtonsToActionsMap[i] = new List<InputToActionMap>();

                    foreach (XboxInputConstants.Buttons xboxButton in buttonAction.XboxButtons)
                    {
                        // TODO: We need to manage this in the InputMapper side
                        inputMap.ButtonsToActionsMap[i].Add(new InputToActionMap { action = buttonAction.name, input = (int)xboxButton });

                        if (!_mappedXboxButtons.Contains((int)xboxButton))
                        {
                            _mappedXboxButtons.Add((int)xboxButton);
                        }
                    }

                    // Keyboard part
                    foreach (KeyCode key in buttonAction.KeyboardKeys)
                    {
                        // TODO: Find a way to not add 100 to the code (for now, it's necessary since there are overlaps with the xbox enum)
                        inputMap.ButtonsToActionsMap[i].Add(new InputToActionMap { action = buttonAction.name, input = 100 + (int)key });

                        // TODO: Temporary
                        if (!_mappedKeyboardKeys.Contains((int)key))
                        {
                            _mappedKeyboardKeys.Add((int)key);
                        }
                    }
                }

                for (int i = 0; i < xboxContext.ButtonStates.Length; i++)
                {
                    XboxState buttonState = xboxContext.ButtonStates[i];

                    inputMap.ButtonsToStatesMap[i] = new List<InputToActionMap>();

                    foreach (XboxInputConstants.Buttons xboxButton in buttonState.XboxButtons)
                    {
                        // TODO: We need to manage this in the InputMapper side
                        inputMap.ButtonsToStatesMap[i].Add(new InputToActionMap() { action = buttonState.name, input = (int)xboxButton });

                        if (!_mappedXboxButtons.Contains((int)xboxButton))
                        {
                            _mappedXboxButtons.Add((int)xboxButton);
                        }
                    }

                    // Keyboard part
                    foreach (KeyCode key in buttonState.KeyboardKeys)
                    {
                        // TODO: Find a way to not add 100 to the code (for now, it's necessary since there are overlaps with the xbox enum)
                        inputMap.ButtonsToStatesMap[i].Add(new InputToActionMap { action = buttonState.name, input = 100 + (int)key });

                        // TODO: Temporary
                        if (!_mappedKeyboardKeys.Contains((int)key))
                        {
                            _mappedKeyboardKeys.Add((int)key);
                        }
                    }
                }

                for (int i = 0; i < xboxContext.AxisRanges.Length; i++)
                {
                    XboxRange axisRange = xboxContext.AxisRanges[i];

                    inputMap.AxisToRangesMap[i] = new List<InputToActionMap>();

                    foreach (XboxInputConstants.Axis xboxAxis in axisRange.Axis)
                    {
                        // TODO: We need to manage this in the InputMapper side
                        inputMap.AxisToRangesMap[i].Add(new InputToActionMap() { action = axisRange.name, input = (int)xboxAxis });

                        if (!_mappedXboxAxis.Contains((int)xboxAxis))
                        {
                            _mappedXboxAxis.Add((int)xboxAxis);
                        }
                    }

                    // Keyboard part
                    foreach (KeyCode key in axisRange.KeyboardKeys)
                    {
                        // TODO: Find a way to not add 100 to the code (for now, it's necessary since there are overlaps with the xbox enum)
                        inputMap.AxisToRangesMap[i].Add(new InputToActionMap { action = axisRange.name, input = 100 + (int)key });

                        // TODO: Temporary
                        if (!_mappedKeyboardKeysAxis.Contains((int)key))
                        {
                            _mappedKeyboardKeysAxis.Add((int)key);
                        }
                    }
                }

                InputContext context = new InputContext(xboxContext.name, inputMap);

                mappedContexts.Add(xboxContext.name, context);
            }

            return mappedContexts;
        }

        // TODO: Probably temporary, until we find a better way and all the classes are refactored

        // Utility method to be used by the ControllerManager class
        public override List<int> GetMappedKeyboardKeysAxis()
        {
            return _mappedKeyboardKeysAxis;
        }

        // Utility method to be used by the ControllerManager class
        public override List<int> GetMappedKeyboardKeys()
        {
            return _mappedKeyboardKeys;
        }

        public override List<int> GetMappedXboxAxis()
        {
            return _mappedXboxAxis;
        }

        public override List<int> GetMappedXboxButtons()
        {
            return _mappedXboxButtons;
        }
    }
}