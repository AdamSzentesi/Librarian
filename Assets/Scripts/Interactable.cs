﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public class Interactable : Obstacle
    {
        public bool IsBeingUsed { get; protected set; }

        private int _RegisteredIndex = -1;

        private void Awake()
        {
            _RegisteredIndex = Level.RegisterInteractable(this);
        }

        virtual public bool StartUsing(Character character) { return false; }
        virtual public void StopUsing(Character character) { }

        private void OnDestroy()
        {
            Level.UnregisterInteractable(_RegisteredIndex);
        }
    }
}