using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaManager : MonoBehaviour
{
     
    public float enemies;
    public bool puerta = true;
    public GameObject doors;
    public void RegisterEnemy()
    {
        Debug.Log("si");
        enemies++;        
    }

    public void OnEnemyDies( )
    {
        enemies--;               
    }

    private void Awake()
    {
        enemies = 0;
    }  

    // Update is called once per frame
    void Update()
    {       
        if (enemies <= 0 && puerta)
        {
            Destroy(doors);
            puerta = false;
        }
        
    }
}
