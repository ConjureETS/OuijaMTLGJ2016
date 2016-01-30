using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

namespace InputHandler
{
    public class ControllerManager : InputManager
    {
        private bool[] _initialSetupDone;
        private PlayerIndex[] _playerIndexes;
        private GamePadState[] _gamePadPreviousStates;
        private GamePadState[] _gamePadStates;

        protected override void InitialSetup()
        {
            _initialSetupDone = new bool[MAX_PLAYER_COUNT];
            _playerIndexes = new PlayerIndex[MAX_PLAYER_COUNT];
            _gamePadPreviousStates = new GamePadState[MAX_PLAYER_COUNT];
            _gamePadStates = new GamePadState[MAX_PLAYER_COUNT];

            for (int i = 0; i < MAX_PLAYER_COUNT; i++)
            {
                _gamePadStates[i] = GamePad.GetState(_playerIndexes[i]);
            }
        }

        protected override void MapInputs()
        {
            for (int i = 0; i < MAX_PLAYER_COUNT; i++)
            {
                _gamePadPreviousStates[i] = _gamePadStates[i];
                _gamePadStates[i] = GamePad.GetState(_playerIndexes[i]);

                if (!_gamePadPreviousStates[i].IsConnected || !_initialSetupDone[i])
                {
                    _initialSetupDone[i] = true;

                    if (_gamePadStates[i].IsConnected)
                    {
                        _playerIndexes[i] = (PlayerIndex)i;

                        Debug.Log(string.Format("GamePad {0} is ready", _playerIndexes[i]));
                    }
                }

                MapPlayerInput(_inputMappers[i], _gamePadStates[i], _gamePadPreviousStates[i]);
            }
        }

        // TODO: Maybe reduce it to only the inputs actually used in the game?
        private void MapPlayerInput(InputMapper inputMapper, GamePadState state, GamePadState previousState)
        {
            foreach (int axisInt in InputMapperAsset.GetMappedXboxAxis())
            {
                MapXboxAxis(axisInt, inputMapper, state);
            }

            foreach (int buttonInt in InputMapperAsset.GetMappedXboxButtons())
            {
                MapXboxButton(buttonInt, inputMapper, state, previousState);
            }

            // TODO: Put the following code into another class, so we can have 2 distinct XboxManager and KeyboardManager classes

            // We map only the keyboard keys that are going to be used in the game

            foreach (int key in InputMapperAsset.GetMappedKeyboardKeys())
            {
                inputMapper.SetRawButtonState(100 + key, Input.GetKey((KeyCode)key), Input.GetKey((KeyCode)key) && !Input.GetKeyDown((KeyCode)key));
            }

            foreach (int key in InputMapperAsset.GetMappedKeyboardKeysAxis())
            {
                float value = Input.GetKey((KeyCode)key) ? 1f : 0f;

                inputMapper.SetRawAxisValue(100 + key, value);
            }
        }

        private void MapXboxButton(int buttonInt, InputMapper inputMapper, GamePadState state, GamePadState previousState)
        {
            XboxInputConstants.Buttons button = (XboxInputConstants.Buttons)buttonInt;

            bool pressed = false;
            bool previouslyPressed = false;

            switch (button)
            {
                case XboxInputConstants.Buttons.A:
                    pressed = state.Buttons.A == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.A == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.B:
                    pressed = state.Buttons.B == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.B == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.X:
                    pressed = state.Buttons.X == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.X == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.Y:
                    pressed = state.Buttons.Y == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.Y == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.LeftShoulder:
                    pressed = state.Buttons.LeftShoulder == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.LeftShoulder == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.RightShoulder:
                    pressed = state.Buttons.RightShoulder == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.RightShoulder == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.Back:
                    pressed = state.Buttons.Back == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.Back == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.Start:
                    pressed = state.Buttons.Start == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.Start == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.LeftStick:
                    pressed = state.Buttons.LeftStick == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.LeftStick == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.RightStick:
                    pressed = state.Buttons.RightStick == ButtonState.Pressed;
                    previouslyPressed = previousState.Buttons.RightStick == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.DPadLeft:
                    pressed = state.DPad.Left == ButtonState.Pressed;
                    previouslyPressed = previousState.DPad.Left == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.DPadRight:
                    pressed = state.DPad.Right == ButtonState.Pressed;
                    previouslyPressed = previousState.DPad.Right == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.DPadUp:
                    pressed = state.DPad.Up == ButtonState.Pressed;
                    previouslyPressed = previousState.DPad.Up == ButtonState.Pressed;
                    break;
                case XboxInputConstants.Buttons.DPadDown:
                    pressed = state.DPad.Down == ButtonState.Pressed;
                    previouslyPressed = previousState.DPad.Down == ButtonState.Pressed;
                    break;
            }

            inputMapper.SetRawButtonState(buttonInt, pressed, previouslyPressed);
        }

        private void MapXboxAxis(int axisInt, InputMapper inputMapper, GamePadState state)
        {
            XboxInputConstants.Axis axis = (XboxInputConstants.Axis)axisInt;

            float value = 0f;

            switch (axis)
            {
                case XboxInputConstants.Axis.LeftStickLeft:
                    // If the left stick X value is negative, we keep it and take its absolute value
                    value = state.ThumbSticks.Left.X < 0f ? -state.ThumbSticks.Left.X : 0f;
                    break;
                case XboxInputConstants.Axis.LeftStickRight:
                    // If the left stick X value is positive, we keep it
                    value = state.ThumbSticks.Left.X > 0f ? state.ThumbSticks.Left.X : 0f;
                    break;
                case XboxInputConstants.Axis.LeftStickDown:
                    value = state.ThumbSticks.Left.Y < 0f ? -state.ThumbSticks.Left.Y : 0f;
                    break;
                case XboxInputConstants.Axis.LeftStickUp:
                    value = state.ThumbSticks.Left.Y > 0f ? state.ThumbSticks.Left.Y : 0f;
                    break;
                case XboxInputConstants.Axis.RightStickLeft:
                    value = state.ThumbSticks.Right.X < 0f ? -state.ThumbSticks.Right.X : 0f;
                    break;
                case XboxInputConstants.Axis.RightStickRight:
                    value = state.ThumbSticks.Right.X > 0f ? state.ThumbSticks.Right.X : 0f;
                    break;
                case XboxInputConstants.Axis.RightStickDown:
                    value = state.ThumbSticks.Right.Y < 0f ? -state.ThumbSticks.Right.Y : 0f;
                    break;
                case XboxInputConstants.Axis.RightStickUp:
                    value = state.ThumbSticks.Right.Y > 0f ? state.ThumbSticks.Right.Y : 0f;
                    break;
                case XboxInputConstants.Axis.TriggerLeft:
                    value = state.Triggers.Left;
                    break;
                case XboxInputConstants.Axis.TriggerRight:
                    value = state.Triggers.Right;
                    break;
            }

            inputMapper.SetRawAxisValue(axisInt, value);
        }
    }
}
