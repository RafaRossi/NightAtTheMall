using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _name = "";
    [SerializeField] private Sprite sprite = null;

    public string GetName() => _name;
    public Sprite GetSprite() => sprite;

}
