using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Aiming", menuName = "States/Player/Player Aiming")]
public class PlayerAiming : PlayerMovement
{
    public override string StateName => "Aiming";

    [Header("Aiming Properties")]
    [SerializeField] private float maxPrecision = 100;
    [SerializeField] private float precisionLoss = 30;
    [SerializeField] private float precisionGain = 25;
    private float currentPrecision = 0;

    private Camera camera = default;

    private Vector2 aimDirection = Vector2.zero;

    private Animator animator = default;

    public override void Initialize(PlayerStateMachine player)
    {
        base.Initialize(player);

        animator = player.GetComponent<Animator>();
        camera = Camera.main;
    }

    public override void FixedTick()
    {
        base.FixedTick();
        Aim();
    }

    public override void Tick()
    {
        base.Tick();

        aimDirection = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void Aim()
    {
        Vector2 lookDirection = aimDirection - rigidbody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        animator.SetFloat("Aiming", angle);
        //rigidbody.SetRotation(angle);
    }

    public void Shoot()
    {

    }
}
