using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Movement 3D", menuName = "States/Player/Player Movement 3D")]
public class PlayerMovement3D : PlayerState
{
    protected CharacterController controller;

    private Vector3 movement = Vector3.zero;

    [SerializeField] private float moveSpeed = 1f;

    public override string StateName { get => "Idle"; }

    public override void Initialize(PlayerStateMachine player)
    {
        base.Initialize(player);

        controller = player.GetComponent<CharacterController>();
    }

    public override void Tick()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    public override void FixedTick()
    {
        if(!controller.isGrounded)
        {
            Debug.Log("Não");

            movement += Physics.gravity;
        }
        else Debug.Log("Sim");
        controller.Move(new Vector3(movement.x, movement.y, movement.z).normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
