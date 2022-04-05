using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : MonoBehaviour
{
    public NecromancerController _necromancer;
    private EnemyLifeComponent _myELC;

    // Start is called before the first frame update
	private void OnDestroy()
	{
        _necromancer._weakCounter--;
	}
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement _myPlayerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if(_myPlayerMovement != null)
        {
            _myELC.Damage(50);
        }
    } 

    private void Start()
    {
        _myELC = GetComponent<EnemyLifeComponent>();
    }
}
