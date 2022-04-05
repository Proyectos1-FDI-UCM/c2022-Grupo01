using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaManager : MonoBehaviour
{
    [SerializeField]
    private List<Door> _listOfDoors;
    private List<BossDoor> _listOfBossDoors;
    [SerializeField]
    private List<EnemyLifeComponent> _listOfEnemies;
    private List<Vector3> _listEnemyPosition;
    public enum SalaStates {Inicial, Inactiva, Activa,Completada};
    public SalaStates myState;
    private BossMovement _bossMovement;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogWarning("Detector "+ this);

        PlayerAttack _player = collision.gameObject.GetComponent<PlayerAttack>();
        EnemyLifeComponent _enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        Door _door = collision.gameObject.GetComponent<Door>();
        BossDoor _bossDoor = collision.gameObject.GetComponent<BossDoor>();
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
        if(_bossDoor != null)
        {
            _bossDoor.salaManager = this;
            _bossDoor.Register();
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
        AudioManager.Instance.Play("FireGenerates");
        myState = SalaStates.Activa;
        foreach(Door door in _listOfDoors)
        {
            door.gameObject.GetComponent<SpriteRenderer>().enabled=true;
            door.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
        foreach (BossDoor _bossDoor in _listOfBossDoors)
        {
            _bossDoor.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            _bossDoor.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }

    }
    public void RegisterEnemy(EnemyLifeComponent enemy)
    {
        
        _listOfEnemies.Add(enemy);
        enemy.ID = _listOfEnemies.Count - 1;
        _listEnemyPosition.Add(enemy.gameObject.transform.position);
    }

    public void OnEnemyDies(EnemyLifeComponent enemy)
    {
        _listOfEnemies.Remove(enemy);
    }

    public void RegisterDoor(Door _door)
    {
        _listOfDoors.Add(_door);
    }

    public void RegisterDoor(BossDoor _bossDoor)
    {
        _listOfBossDoors.Add(_bossDoor);
    }
    public void DestroyDoors()
    {
        foreach(Door _door in _listOfDoors)
        {
            _door.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _door.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
        foreach(BossDoor _bossDoor in _listOfBossDoors)
        {
            _bossDoor.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _bossDoor.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    private void Awake()
    {
        _listOfDoors = new List<Door>();
        _listOfBossDoors = new List<BossDoor>();
        _listOfEnemies = new List<EnemyLifeComponent>();
        _listEnemyPosition = new List<Vector3>();
        myState = SalaStates.Inicial;
    }

    // Update is called once per frame
    void Update()
    {   
        if(myState == SalaStates.Inicial)
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
