using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	#region parameters
	public int movementSpeed = 10;

	[SerializeField]
    private float rollSpeed = 20, hookSpeed = 2, rango = 20;
    [SerializeField] private float rodarCooldown = 0.5f;

    [SerializeField] private LayerMask notRollingLayers;
    [SerializeField] private int rollRaycastDistanceModifier;
    #endregion

    #region properties
    [HideInInspector] public bool gancho = false;
    private bool target = false, getInput = true;
    private float _timeForRoll = 0;

    private Vector3 movementWalk, ganchoDirection, rollDirection;
    public Vector3 animationDirection;
    private Vector2 mouse, ganchoPos;
	#endregion

	#region references
    [SerializeField]
    private Animator animator;
	[SerializeField]
    private Camera cam;
    [SerializeField]
    private Rigidbody2D gunRB;
    [SerializeField]
    private GameObject hook;

    private Transform _myTransform, _hookTransform;
    private Collider2D _playerCollider;
    private Rigidbody2D playerRB;
    private PlayerManager _playerManager;
    #endregion

    #region methods
    void Move(Vector3 movement)
    {
        //Debug.Log(movement);
        playerRB.MovePosition(transform.position + movement.normalized * movementSpeed * Time.fixedDeltaTime);
        animationDirection = movement;
        animator.SetBool("Walk", true);
        AudioManager.Instance.PlayInterval("PlayerStep", 0.4f);
        GetComponent<PlayerAttack>().SetAttackPoint(animationDirection);
    }

    IEnumerator Rodar(Vector3 movement)
    {
        _playerManager.PlayerInRoll(true);
        //Empezamos la animaci�n de ruedo
        animator.SetTrigger("Ruedo");
        //Hacemos que el jugador pueda atravesar enemigos
        _playerCollider.isTrigger = true;
        //Hacemos que no se pueda realizar movimiento durante el ruedo
        getInput = false;

        if (movement == Vector3.zero) rollDirection = new Vector3(1, 0, 0);
        else rollDirection = movement;
        
        int n = 0;
        while (n < 20 && !Physics2D.Raycast(_myTransform.position, rollDirection, Vector3.Distance(_myTransform.position, _myTransform.position + rollDirection.normalized / rollRaycastDistanceModifier), notRollingLayers))
        {   
            playerRB.MovePosition(transform.position + rollDirection.normalized * rollSpeed * Time.fixedDeltaTime);
            n++;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        movementWalk = Vector3.zero;
        
        _playerCollider.isTrigger = false;
        animator.SetTrigger("IdleTrigger");
        getInput = true;
        Debug.Log(getInput);
        _timeForRoll = 0;
        _playerManager.PlayerInRoll(false);
    }

    public void LanzaGancho(Vector3 position)
    {
        //Debug.LogWarning(position);
        ganchoPos = position;
        //ganchoDirection = position - _myTransform.position;
        ganchoDirection = _hookTransform.position - _myTransform.position;
        target = true;
        _playerCollider.isTrigger = true;
        animator.SetBool("JUMP",true);
    }

    public void ModifyPlayerSpeed(int speed)
    {
        movementSpeed += speed;
        _playerManager.UpdateSpeed(movementSpeed);
        rollSpeed = 2 * movementSpeed;
    }
    #endregion

    void Start()
    {
        _playerManager = PlayerManager.Instance;
        _myTransform = transform;
        _hookTransform = hook.transform;
        gancho = false;
        target = false;
        hook.SetActive(false);
        _playerCollider = GetComponent<Collider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        _playerManager.UpdateSpeed(movementSpeed);
    }

	void FixedUpdate()
    {
        _timeForRoll += Time.fixedDeltaTime;
        
        //Input movimiento personaje
        if(getInput)
		{
            movementWalk.x = Input.GetAxisRaw("Horizontal");
            movementWalk.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("X", animationDirection.x);
            animator.SetFloat("Y", animationDirection.y);
        }

        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        
        bool pickUpHook = Mathf.Abs(_myTransform.position.x - _hookTransform.position.x) < 0.1 && Mathf.Abs(_myTransform.position.y - _hookTransform.position.y) < 0.1;
        
        if (gancho == false)
        {
            
            //Vuelta del gancho
            _hookTransform.Translate(hookSpeed * 5  * Time.deltaTime * (_myTransform.position - _hookTransform.position));

            if (pickUpHook && getInput)
            {
                hook.SetActive(false);
                target = false;
                _playerCollider.isTrigger = false;
                
            }
            
            if (pickUpHook && Input.GetKey(KeyCode.E)/*Input.GetMouseButton(1)*/)
            {
                ganchoPos = mouse;
                ganchoDirection = ganchoPos - (Vector2) _myTransform.position;
                
                gancho = true;

                hook.SetActive(true);
            }

            //Input rodar
            if (Input.GetKey(KeyCode.Space) && _timeForRoll >= rodarCooldown && getInput)
            {
                StartCoroutine(Rodar(animationDirection));
            }

            //Si has recibido un input y no est�s rodando te mov�s
            if (movementWalk != Vector3.zero && getInput) Move(movementWalk);

            else playerRB.velocity = Vector2.zero;   // para que no se acumule la fuerza que le aplica el enemigo al colisoniar con el. Revisar si se puede hacer mejor
        }

        else
        {
            animator.SetBool("Walk", false);

            if (target == false)
            {
                if (Vector3.Magnitude(_myTransform.position - _hookTransform.position) < rango) _hookTransform.Translate(hookSpeed * Time.deltaTime * ganchoDirection.normalized * 5);

                else gancho = false;
            }

            else 
            {
                if (!pickUpHook)
                {
                    //Move(ganchoDirection);
                    animator.SetBool("JUMP", true);
                    animator.SetBool("JUMP", false);
                    Move(ganchoDirection);
                    
                    //_myTransform.Translate(ganchoSpeed * Time.deltaTime * ganchoDirection.normalized * 5);
                }

                else 
                {
                    gancho = false;
                    target = false;
                    animator.SetBool("JUMP", false);
                    //animator.SetTrigger("IdleTrigger");
                }               
            }
        }

        if (movementWalk == Vector3.zero)
        {
            animator.SetBool("Walk", false);            
        }

        Vector2 lookDir = mouse - gunRB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gunRB.rotation = angle;
        gunRB.position = GetComponentInParent<Rigidbody2D>().position;
    }  
}