using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HolyFlotadorImage : MonoBehaviour
{
    #region Singleton
    private static Image _instance;

    public static Image Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = GetComponent<Image>();
    }
    #endregion
}
