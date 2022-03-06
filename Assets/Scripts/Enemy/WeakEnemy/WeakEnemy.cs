using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : MonoBehaviour
{
    private float _num=0;
    [SerializeField]
    private EnemyLifeComponent _life;

   // public Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement _player = collision.gameObject.GetComponent<PlayerMovement>();
        if (_player != null)
        {
            //hacer daño
            Destroy(gameObject);
            GameManager.Instance.OnWeakDies(this);
           
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.RegisterWeakEnemy(this);
        _num = 1;
        _life = GetComponent<EnemyLifeComponent>();
      //  animator.SetBool("Walk",true);
    }

    // Update is called once per frame
    void Update()
    {

        if (_life._currentLife <= 0 && _num > 0)
        {
            GameManager.Instance.OnWeakDies(this);
            _num--;
            
        } 
    }
}
