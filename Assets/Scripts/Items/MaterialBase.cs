using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New material object", menuName = "Inventory/Items/Material")]
public class MaterialBase : ItemBase
{
    public void Awake()
    {
        type = ItemType.MATERIAL; 
    }
}
