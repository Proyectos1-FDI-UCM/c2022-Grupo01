using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyFlotador : MonoBehaviour
{
    #region methods
    public void Activate()
    {
        PlayerManager.Instance.myLifeState = PlayerManager.LifeStates.HolyFlotador;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
