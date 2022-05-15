using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region parameters
    public int movementSpeed = 10;

    [SerializeField]
    private float rollSpeed = 20;
    [SerializeField] private float rodarCooldown = 0.5f;
    #endregion

    #region properties
    [HideInInspector] public bool gancho = false;
    private bool target = false, getInput = true;
    private float _timeForRoll = 0;

    public Vector3 animationDirection;
    private Vector3 movementWalk, ganchoDirection, rollDirection;
    private Vector2 mouse, ganchoPos, postePosition;
    [HideInInspector] public bool canRoll = true;
    private bool inRoll;
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

    #region newGancho
    //Necesitamos un método que lance el gancho en la dirección dada por el ratón. Eso se puede hacer fácil. 
    //Dentro de esta Corrutina vamos a hacer que se mueve en esa dirección X distancia o hasta que choque con un Poste.
    //Cuando choque con un poste queremos que pare el movimiento y que se ejecute otra Corrutina que sea MovePlayerTowardsPoste o algo así
    //Este otro método lo que va a hacer será mover al jugador en la dirección dada por el gancho. En este caso vamos a necesitar la posición que nos dé el 
    //poste desde su objeto hijo TravelPoint. Cuando el jugador haya llegado al punto deseado, paramos la corrutina y devolvemos al jugador a su estado normal.

    
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

    IEnumerator Roll(Vector3 movement)
    {
        inRoll = true;
        _playerManager.PlayerInRoll(inRoll);
        //Empezamos la animaci�n de ruedo
        animator.SetTrigger("Ruedo");
        getInput = false;
        //Hacemos que el jugador pueda atravesar enemigos
        _playerCollider.isTrigger = true;
        //Hacemos que no se pueda realizar movimiento durante el ruedo

        if (movement == Vector3.zero) rollDirection = new Vector3(1, 0, 0);
        else rollDirection = movement;

        yield return new WaitForSeconds(20 * Time.fixedDeltaTime);

        EndRoll();
    }

    public void EndRoll()
    {
        StopCoroutine("Roll");
        movementWalk = Vector3.zero;
        inRoll = false;
        getInput = true;
        _playerCollider.isTrigger = false;
        animator.SetTrigger("IdleTrigger");
        _timeForRoll = 0;
        _playerManager.PlayerInRoll(inRoll);
    }

    public void LanzaGancho(Vector3 position, Vector3 travelPoint)
    {
        //Debug.LogWarning(position);
        ganchoPos = position;
        //ganchoDirection = position - _myTransform.position;
        ganchoDirection = travelPoint - _myTransform.position;
        target = true;
        _playerCollider.isTrigger = true;
        animator.SetBool("JUMP", true);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    public void LaunchHook()
	{
        PlayerManager.Instance.gancho.GetComponent<gancho>();
	}

    public IEnumerator MovePlayerToHookPoint(Vector3 travelPoint)
    {
        GetComponent<Collider2D>().isTrigger = true;
        hook.GetComponent<gancho>().canMove = false;
        hook.GetComponent<gancho>().hookSpeed = 0;
        getInput = false;
        animator.SetBool("JUMP", true);
        Vector3 dir = travelPoint - transform.position;
        while (Vector3.Magnitude(dir) > 0.3)
		{
            Move(dir);
            dir = travelPoint - transform.position;
            yield return new WaitForSeconds(Time.deltaTime);
		}

        animator.SetBool("JUMP", false);
        getInput = true;
        hook.GetComponent<gancho>().hookSpeed = hook.GetComponent<gancho>().speed;
        hook.GetComponent<gancho>().canMove = true;
        GetComponent<Collider2D>().isTrigger = false;
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
        _playerCollider = GetComponent<Collider2D>();
        playerRB = GetComponent<Rigidbody2D>();
        _playerManager.UpdateSpeed(movementSpeed);
        canRoll = true;
    }

	void FixedUpdate()
    {
        _timeForRoll += Time.fixedDeltaTime;
        
        //Input movimiento personaje
        if (getInput)
		{
            movementWalk.x = Input.GetAxisRaw("Horizontal");
            movementWalk.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("X", animationDirection.x);
            animator.SetFloat("Y", animationDirection.y);
        }

        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        
        //Lanzar gancho
        if (Input.GetKey(KeyCode.E) && hook.GetComponent<gancho>().canLaunch)
        {
            hook.SetActive(true);
            hook.GetComponent<gancho>().StartCoroutine(hook.GetComponent<gancho>().LaunchHook(mouse));
        }

		else if (!hook.GetComponent<gancho>().launching && hook.GetComponent<gancho>().canMove && Vector3.Magnitude(transform.position - hook.transform.position) > 0.2)
		{
            hook.transform.Translate(20 * Time.deltaTime * (transform.position - hook.transform.position).normalized);
        }

        //Input rodar
        if (Input.GetKey(KeyCode.Space) && _timeForRoll >= rodarCooldown && getInput && canRoll)
        {
            StartCoroutine(Roll(animationDirection));
        }

		if (inRoll) { transform.Translate(rollDirection.normalized * rollSpeed * Time.fixedDeltaTime); }

        //Si has recibido un input y no est�s rodando te mov�s
        if (movementWalk != Vector3.zero && getInput) Move(movementWalk);
        else if (movementWalk == Vector3.zero) { animator.SetBool("Walk", false); playerRB.velocity = Vector2.zero; }
        else playerRB.velocity = Vector2.zero;   // para que no se acumule la fuerza que le aplica el enemigo al colisoniar con el. Revisar si se puede hacer mejor

        Vector2 lookDir = mouse - gunRB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        gunRB.rotation = angle;
        gunRB.position = GetComponentInParent<Rigidbody2D>().position;
    }  
}