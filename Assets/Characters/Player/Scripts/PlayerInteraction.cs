using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{   
    [SerializeField] private float raySize = 1f;
    private Vector2 facingVector
    {
        get
        {
            return PlayerManager.FacingDirection;
        }
    }

    [SerializeField] private LayerMask mask = default;

    private void Update()
    {
        if(Input.GetButtonDown("Action"))
        {
            Interact();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    public T GetCast<T>() where T : IBase
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerManager.GetPosition(), facingVector, raySize, mask);

        if (hit)
        {
            return hit.collider.gameObject.GetComponent<T>();
        }
        return default;
    }

    public void Attack()
    {
        IDamageble target = GetCast<IDamageble>();

        if (target != null)
        {
            target.TakeDamage(35);
        }
    }

    public void Interact()
    {
        IInteractable target = GetCast<IInteractable>();

        if (target != null)
        {
            target.Interact();
        }
    }
}
