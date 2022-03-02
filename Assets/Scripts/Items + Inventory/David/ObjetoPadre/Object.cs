using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes { Buff, NoBuff,Active }

public class Object : MonoBehaviour
{ 
    public int ID;
    public Sprite itemSprite; //para qué queremos el sprite ;(
    public ItemTypes type;
    public string nameOnScreen;
    public string littleDescriptionOnScreen;
    public string descriptionOnInventoryMenu;
}

