using System;
using UnityEngine;

public abstract class BaseState<EState> where EState : Enum // Only allow Enums to be used as state keys
{
    // Whenever we create a new state, we need to provide an Enum associated with it.
    public BaseState(EState key)
    {
        StateKey = key;
    }
    public EState StateKey { get; private set; }
    
    // Core state lifecycle methods
    public abstract void EnterState(); // Called when entering a state to initialize any necessary variables, play animations, etc.
    public abstract void ExitState();  // Called when exiting a state, used for cleanup if necessary
    public abstract void UpdateState(); // Called every frame while in this state, used for ongoing behavior
    
    // State transition logic
    public abstract EState GetNextState(); // Determines which state to transition to
    
    // Physics event handling (trigger collisions)
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerStay(Collider other);
    public abstract void OnTriggerExit(Collider other);
}
