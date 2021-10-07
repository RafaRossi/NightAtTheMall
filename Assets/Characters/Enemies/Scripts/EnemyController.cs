using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : StateMachineController<EnemyState, EnemyController>
{
    private void Start()
    {
        Initialize(this);
        SetState("Idle");
    }

    protected override void Update()
    {
        base.Update();

        if (!GameManager.isPaused)
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
