using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D _bossRB;
    private Transform _myTransform;
    public Vector3 _direction, InitialPosition;
    public float speed = 5, giro = 0, cooldown = 5, timeLeft, vuelta = 0, carga;

    public bool agua=true, recarga=false;
    public GameObject centro;
    private BoxCollider2D bossColider;
    public SpongeSalaManager _mySpongeSalaManager;
    private RangeAttack _myRangeAttack;

    public void ExecuteMovementBoss()
    {
        this.enabled = true;
        

    }
    public void Giro()
    {
        giro++;
        if (_direction.x == 1)
        {
            _direction.x = 0;
            _direction.y =1;
            animator.SetTrigger("IDLE");
        }
        else if(_direction.y == 1)
        {
            _direction.x = -1;
            _direction.y = 0;
           animator.SetTrigger("ABAJO");
        }
        else if (_direction.x == -1)
        {
            _direction.x = 0;
            _direction.y = -1;
           animator.SetTrigger("IDLE");
        }
        else 
        {
            _direction.x = 1;
            _direction.y = 0;
           animator.SetTrigger("ARRIBA");
        }
      
    }
    // Start is called before the first frame update
    void Start()
    {
        _bossRB = GetComponent<Rigidbody2D>();
        _direction = new Vector3(1, 0, 0);
        _myTransform = transform;
        bossColider = GetComponent<BoxCollider2D>();
        InitialPosition = _myTransform.position;
        _myRangeAttack = GetComponent<RangeAttack>();
        animator.SetTrigger("ARRIBA");
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = timeLeft - Time.deltaTime;
        carga = carga - Time.deltaTime;

        if (timeLeft < 0 && !agua)
        {
            bossColider.isTrigger = true;
            _bossRB.constraints = RigidbodyConstraints2D.None;
            _bossRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            // Vuelta a los bordes desde el centro
            _bossRB.MovePosition(_myTransform.position + (InitialPosition - _myTransform.position) * speed * Time.fixedDeltaTime);
            // Detección de si ha llegado al borde
            if (Vector3.Magnitude(InitialPosition-_myTransform.position)< 0.1)
            {
                vuelta++;
                agua = true;
                recarga = false;
                bossColider.isTrigger = false;
                _direction.x = 1;
                _direction.y = 0;
                giro = -(vuelta % 4);
                _myRangeAttack.enabled = true;
                animator.SetBool("RECARGA", false);
                Debug.LogWarning("TOC");
            }
        }
        // Movimiento principal en los bordes
        if (agua)
        {
            _bossRB.MovePosition(_myTransform.position + _direction.normalized * speed * Time.fixedDeltaTime);
            
            
        }
        else if(!recarga)
        {
            _myRangeAttack.enabled = false;
            bossColider.isTrigger = true;
            
            // Movimiento al centro
            _bossRB.MovePosition(_myTransform.position+( centro.transform.position - _myTransform.position).normalized * speed * Time.fixedDeltaTime);
            // Detección de si está en el centro
            if (Vector3.Magnitude(centro.transform.position - _myTransform.position) < 0.1)
            {
                //agua = true;
                
                bossColider.isTrigger = false;
                timeLeft = cooldown;
                animator.SetBool("RECARGA", true);
                _bossRB.constraints = RigidbodyConstraints2D.FreezeAll;
                if (carga < 0)
                {
                    //animator.SetTrigger("IDLE");
                    _mySpongeSalaManager.DestaparFuentes();
                    recarga = true;
                }
            }
            else
            {
                carga = cooldown;
            }
        }
    }

}
