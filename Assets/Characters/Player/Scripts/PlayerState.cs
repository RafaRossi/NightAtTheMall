using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> : ScriptableObject where T : BaseStateMachineController
{
    public abstract string StateName { get; }
    public abstract void Tick();
    public abstract void FixedTick();
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    protected T StateController { get; set; }
    public virtual void Initialize(T controller) => StateController = controller;
}

public abstract class PlayerState : State<PlayerStateMachine>
{ 
}