using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea]
    public string note;
    // public Sprite itemImage; 
    public GameObject itemPrefab;
}
