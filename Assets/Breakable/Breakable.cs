using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble : IBase
{
    List<ItemInfo> Drop();
    void TakeDamage(float damage);
    void Die();
}

public class Breakable : MonoBehaviour, IDamageble
{
    [SerializeField] private List<Drop> drops = new List<Drop>();
    [SerializeField] private float health = 100;
    [SerializeField] private ItemObject prefab = default;
    
    public float Health { 
        get => health; 
        set
        {
            health = value;

            if(health <= 0)
            {
                health = 0;

                Die();
            }
        }
    }

    public void Die()
    {
        foreach(ItemInfo item in Drop())
        {
            Instantiate(prefab, transform.position, Quaternion.identity).SetItem(item);
        }

        Destroy(gameObject);
    }

    public List<ItemInfo> Drop()
    {
        List<ItemInfo> items = new List<ItemInfo>();

        foreach(Drop drop in drops)
        {
            items.Add(drop.GetItemChance());
        }
        return items;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}

[System.Serializable]
public class Drop
{
    [SerializeField] private ItemInfo item = default;
    [SerializeField] private float chance = 100;

    public ItemInfo GetItemChance() => Random.Range(0, 100) < chance ? item : null;
}
