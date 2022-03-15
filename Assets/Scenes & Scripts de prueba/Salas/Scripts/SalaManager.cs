using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaManager : MonoBehaviour
{
    [SerializeField]
    private List<Door> _listOfDoors;
    [SerializeField]
    private List<EnemyLifeComponent> _listOfEnemies;
    private List<Vector3> _listEnemyPosition;
    public enum SalaStates {Inicial, Inactiva, Activa,Completada};
    [SerializeField]
    private List<fountainScript> _listOfFountains;
    public SalaStates myState;
    private BossMovement _bossMovement;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogWarning("Detector "+ this);

        PlayerAttack _player = collision.gameObject.GetComponent<PlayerAttack>();
        EnemyLifeComponent _enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        Door _door = collision.gameObject.GetComponent<Door>();
        fountainScript _fountain = collision.gameObject.GetComponent<fountainScript>();

        if (_player != null)
        {
            EnterSala();
        }
        if (_enemy != null)
        {
            _enemy.sala = this;
            _enemy.gameObject.GetComponentInChildren<DetectPlayer>().sala = this;
            _enemy.Register();
            // Si está el jefe Boss Esponja se coge su script de movimiento
            // Si no, _bossMovement = null
            // **Pensar en otra manera para que sea más eficiente, ya que por cada enemigo
            // se ejecuta este GetComponent
            _bossMovement = _enemy.GetComponent<BossMovement>();
        }
        if (_door != null)
        {
            _door.sala = this;
            _door.Register();
        }
        if (_fountain != null)
        {
            _listOfFountains.Add(_fountain);
            _fountain._salaManager = this;
        }
    }

    public void CompruebaFuente()
    {
        int numFuentesLlenas = 0;
        foreach (fountainScript fountain in _listOfFountains)
        {
            if (fountain._isClogged)
            {
                numFuentesLlenas++;
            }
        }
        if (numFuentesLlenas == _listOfFountains.Count)
        {
            _bossMovement.agua = false;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        PlayerAttack _player = collision.gameObject.GetComponent<PlayerAttack>();
        if (_player != null && myState != SalaStates.Completada)
        {
            Reload();
            myState = SalaStates.Inicial;
        }
    }
    public void Reload()
    {
        
        foreach(EnemyLifeComponent enemy in _listOfEnemies)
        {
            enemy.gameObject.transform.position = _listEnemyPosition[enemy.ID];
            enemy.gameObject.GetComponentInChildren<DetectPlayer>().Deactivate() ;
            
        }
    }

    public void EnterSala()
    {
        //Debug.Log("Funciona");
        myState = SalaStates.Activa;
        foreach(Door door in _listOfDoors)
        {
            door.gameObject.GetComponent<SpriteRenderer>().enabled=true;
            door.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
    }
    public void RegisterEnemy(EnemyLifeComponent enemy)
    {
        
        _listOfEnemies.Add(enemy);
        enemy.ID = _listOfEnemies.Count - 1;
        _listEnemyPosition.Add(enemy.gameObject.transform.position);
        Debug.Log("enemy: " + _listOfEnemies.Count);
    }

    public void OnEnemyDies( EnemyLifeComponent enemy)
    {
        _listOfEnemies.Remove(enemy);
        //_listEnemyPosition.Remove(_listEnemyPosition[enemy.ID]);
    }

    public void RegisterDoor(Door _door)
    {
        _listOfDoors.Add(_door);
        Debug.Log("doors: "+_listOfDoors.Count);
        

    }
    public void DestroyDoors()
    {

        foreach(Door _door in _listOfDoors)
        {
            _door.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _door.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    private void Awake()
    {
        _listOfDoors = new List<Door>();
        _listOfEnemies = new List<EnemyLifeComponent>();
        _listEnemyPosition = new List<Vector3>();
        myState = SalaStates.Inicial;
        _listOfFountains = new List<fountainScript>();
    }

    public void Start()
    {
       // Debug.LogWarning(this);
        
    }

    // Update is called once per frame
    void Update()
    {   if(myState == SalaStates.Inicial)
        {
            DestroyDoors();
            myState = SalaStates.Inactiva;
        }    
        else if (_listOfEnemies.Count == 0 && myState==SalaStates.Activa)
        {
            DestroyDoors();
            myState = SalaStates.Completada;
        }
        
    }
}
