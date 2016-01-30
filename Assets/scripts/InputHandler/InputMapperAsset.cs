using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace InputHandler
{
    public abstract class InputMapperAsset : ScriptableObject
    {
        public enum InputTypes { Action, State, Range }

        public abstract Dictionary<string, InputContext> GetMappedContexts();

        // TODO: Probably temporary, until we find a better way and all the classes are refactored
        public abstract List<int> GetMappedKeyboardKeysAxis();
        public abstract List<int> GetMappedKeyboardKeys();
        public abstract List<int> GetMappedXboxAxis();
        public abstract List<int> GetMappedXboxButtons();
    }
}