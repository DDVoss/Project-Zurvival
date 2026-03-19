using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum // Only allow Enums to be used as state keys
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected bool IsTransitioningState = false;
    protected BaseState<EState> CurrentState;


    void Start()
    {
        CurrentState.EnterState(); // Initialize the current state
    }

    void Update()
    {
        // Get the next state key from the current state
        EState nextStateKey = CurrentState.GetNextState();

        // If IstransitioningState is false and the next state key is the same as the current state key, then update the current state.
        if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            CurrentState.UpdateState();    
        } else if (!IsTransitioningState){ 
            // If IstransitioningState is false and the next state key is different from the current state key, then transition to the next state.
            TransitionToState(nextStateKey);
        }
    }
    void TransitionToState(EState statekey)
    {
        // When transitionToState is called, we set it to true
        IsTransitioningState = true;
        CurrentState.ExitState(); // Exit and clean up the current state if necessary
        CurrentState = States[statekey]; // Set the current state to the new state
        CurrentState.EnterState(); // Enter the new state and initialize with any necessary setup E.g. resetting timers, playing animations, etc.
        IsTransitioningState = false;
    }

    void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }
    
    void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }
}
