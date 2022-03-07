using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes { Passive,Active }

public class Object : MonoBehaviour
{ 
    public int ID;
    public ItemTypes type;
    [TextArea(1, 2)] public string nameOnScreen;
    [TextArea(4, 5)] public string littleDescriptionOnScreen;
    //[TextArea(6,7)] public string descriptionOnInventoryMenu;
}

