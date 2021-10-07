using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachineController : MonoBehaviour
{
    public abstract void SetState(string stateName);
}

public abstract class StateMachineController<T, U> : BaseStateMachineController where T : State<U> where U : StateMachineController<T, U>
{
    private T currentState = default;
    protected T CurrentState
    {
        get => currentState;
        set
        {
            if(currentState != null) currentState.OnStateExit();

            currentState = value;
            currentState.OnStateEnter();
        }
    }
    
    public override void SetState(string stateName) => CurrentState = statesList.Find(state => state.StateName.Equals(stateName));

    protected virtual void Update() => CurrentState.Tick();
    protected virtual void FixedUpdate() => CurrentState.FixedTick();

    [SerializeField] protected List<T> statesList = new List<T>();
    protected virtual void Initialize(U initializer)
    {
        foreach(T state in statesList)
        {
            state.Initialize(initializer);
        }
    }
}

public class PlayerStateMachine : StateMachineController<PlayerState, PlayerStateMachine>
{
    private void Start()
    {
        Initialize(this);
        SetState("Idle");
    }

    protected override void Update()
    {
        base.Update();

        if(!GameManager.isPaused)
        {
            if (Input.GetMouseButton(1))
            {
                SetState("Aiming");
            }
            else
                SetState("Idle");
        }
    }
}
