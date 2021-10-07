using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Movement", menuName = "States/Player/Player Movement")]
public class PlayerMovement : PlayerState
{
    protected Rigidbody2D rigidbody = null;

    private Vector2 movement = Vector2.zero;

    [SerializeField] private float moveSpeed = 1f;

    public override string StateName { get => "Idle"; }

    public override void Initialize(PlayerStateMachine player)
    {
        base.Initialize(player);

        rigidbody = PlayerManager.Rigidbody;
    }

    public override void Tick()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(movement != Vector2.zero) PlayerManager.FacingDirection = movement;
    }

    public override void FixedTick()
    {
        rigidbody.velocity = new Vector2(movement.x, movement.y).normalized * moveSpeed;
    }
}
