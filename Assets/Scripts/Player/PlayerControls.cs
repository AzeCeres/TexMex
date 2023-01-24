using System;
using System.Collections.Generic;
using OmniDi.Library.CharacterControl.Input;
using OmniDi.Library.Controllers;
using OmniDi.Library.Util;
using UnityEngine;

namespace Player
{
    public class PlayerControls : Singleton<PlayerControls>
    {
        [SerializeField] private InputProvider[] inputProviders;

        [Tooltip("The maximum amount of clones that can be active at one time")]
        [SerializeField] private uint maximumNumberOfActiveClones = 2;


        private InputProvider _input;

        private uint _numberOfActiveClones;
        private List<uint> _activeCloneIds;

        private uint _currentlySelectedCloneId;
        /// <summary>
        /// The ID of the currently selected clone.
        /// </summary>
        public uint CurrentlySelectedCloneId
        {
            get => _currentlySelectedCloneId;
            private set
            {
                if (!_activeCloneIds.Contains(value)) return;
                _currentlySelectedCloneId = value;
            }

        }

        protected void Awake()
        {
            _input = inputProviders[0];
            for (int i = 1; i < inputProviders.Length; i++)
            {
                _input = _input.SetNext(inputProviders[i]);
            }

            _activeCloneIds = new List<uint>();
        }

        private void Update()
        {
            ReadInputs();
        }

        private void ReadInputs()
        {
            var inputs = _input.GetInput(new InputState());

            if (inputs.GetBoolValueFromActionName("Clone").ValueOrThrow().Value) Clone();
            if (inputs.GetBoolValueFromActionName("Switch").ValueOrThrow().Value) Switch();
            if (inputs.GetBoolValueFromActionName("Pause").ValueOrThrow().Value) Pause();
        }

        private void Clone()
        {
            if (_numberOfActiveClones >= maximumNumberOfActiveClones) return;
            _numberOfActiveClones++;
            Switch();
        }

        private void Switch()
        {
            var currentCloneIdIndex = _activeCloneIds.IndexOf(CurrentlySelectedCloneId);

            if (currentCloneIdIndex == _activeCloneIds.Count - 1)
            {
                CurrentlySelectedCloneId = _activeCloneIds[0];
                return;
            }

            CurrentlySelectedCloneId = (uint)(currentCloneIdIndex + 1);
        }

        private void Pause()
        {
            GameController.TogglePause();
        }
    }
}