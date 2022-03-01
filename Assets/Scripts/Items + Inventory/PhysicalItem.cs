using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PhysicalItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;

    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.sprite;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
        GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(6, 6, 1);
    }
}
