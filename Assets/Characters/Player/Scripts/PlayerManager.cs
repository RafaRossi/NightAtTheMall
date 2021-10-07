using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerManager : Manager<PlayerManager>
{
    private Vector2 facingDirection = Vector2.up;
    public static Vector2 FacingDirection
    {
        get => Instance.facingDirection;
        set => Instance.facingDirection = value;
    }

    [SerializeField] private Rigidbody2D rb = default;
    public static Rigidbody2D Rigidbody
    {
        get
        {
            if(!Instance.rb)
            {
                Instance.rb = Instance.GetComponent<Rigidbody2D>();
            }

            return Instance.rb;
        }
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, facingDirection, Color.black, 2);
    }

    public static Vector2 GetPosition() => Instance.transform.position;
}
