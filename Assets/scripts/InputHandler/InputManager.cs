using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;

namespace InputHandler
{
    public abstract class InputManager : MonoBehaviour
    {
        public static InputManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private static InputManager _instance;

        protected InputMapper[] _inputMappers;

        public int MAX_PLAYER_COUNT = 2;
        public InputMapperAsset InputMapperAsset;

        protected abstract void InitialSetup();
        protected abstract void MapInputs();

        void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;

                _inputMappers = new InputMapper[MAX_PLAYER_COUNT];

                Dictionary<string, InputContext> mappedContexts = InputMapperAsset.GetMappedContexts();

                for (int i = 0; i < MAX_PLAYER_COUNT; i++)
                {
                    _inputMappers[i] = new InputMapper(mappedContexts, i);
                }

                // Do the needed initial setup in the derived classes
                InitialSetup();
            }
        }

        void Update()
        {
            // Do the input mapping here in the derived classes
            MapInputs();

            for (int i = 0; i < _inputMappers.Length; i++)
            {
                _inputMappers[i].Dispatch();
            }
        }

        public void AddCallback(int playerIndex, Action<MappedInput> action)
        {
            _inputMappers[playerIndex].AddCallback(action);
        }

        public void PushActiveContext(string name, int playerIndex)
        {
            _inputMappers[playerIndex].PushActiveContext(name);
        }

        public void PopActiveContext(int playerIndex)
        {
            // TODO: Give the choice to remove an active context not on top
            _inputMappers[playerIndex].PopActiveContext();
        }

        public void ClearContexts()
        {
            // For now, all input mappers are gonna have the same contexts at the same time

            for (int i = 0; i < _inputMappers.Length; i++)
            {
                _inputMappers[i].ClearActiveContexts();
            }
        }

        void LateUpdate()
        {
            for (int i = 0; i < _inputMappers.Length; i++)
            {
                _inputMappers[i].ResetInputs();
            }
        }
    }
}