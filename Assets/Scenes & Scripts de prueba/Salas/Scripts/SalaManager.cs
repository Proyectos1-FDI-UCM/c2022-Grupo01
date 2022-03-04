using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaManager : MonoBehaviour
{
     
    public float enemies;
    public GameObject doors;
    private List<Door> _listOfDoors;
    private List<EnemyLifeComponent> _listOfEnemies;
    private List<Vector3> _listEnemyPosition;

    public enum SalaStates {Inactiva,Activa,Completada};

    public SalaStates myState;

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAttack _player = collision.gameObject.GetComponent<PlayerAttack>();
        EnemyLifeComponent _enemy = collision.gameObject.GetComponent<EnemyLifeComponent>();
        Door _door = collision.gameObject.GetComponent<Door>();
        if (_player != null)
        {

            EnterSala();
        }
        if (_enemy != null)
        {
            _enemy.sala = this;
        }
        if (_door != null)
        {
            _door.sala = this;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        PlayerAttack _player = collision.gameObject.GetComponent<PlayerAttack>();
        if (_player != null && myState != SalaStates.Completada)
        {
            Reload();
        }
    }
    public void Reload()
    {
        int i = 0;
        foreach(EnemyLifeComponent enemy in _listOfEnemies)
        {
            enemy.gameObject.transform.position = _listEnemyPosition[i];
            i++;
        }
    }

    public void EnterSala()
    {
        //Debug.Log("Funciona");
        myState = SalaStates.Activa;
        foreach(Door door in _listOfDoors)
        {
            door.gameObject.GetComponent<SpriteRenderer>().enabled=true;
            door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void RegisterEnemy(EnemyLifeComponent enemy)
    {
        Debug.Log("si");
        _listOfEnemies.Add(enemy);
        _listEnemyPosition.Add(enemy.gameObject.transform.position);
    }

    public void OnEnemyDies( EnemyLifeComponent enemy)
    {
        _listOfEnemies.Remove(enemy);
        _listEnemyPosition.Remove(enemy.gameObject.transform.position);
    }

    public void RegisterDoor(Door _door)
    {
        _listOfDoors.Add(_door);
        Debug.Log(_listOfDoors.Count);
        

    }
    public void DestroyDoors()
    {

        foreach(Door _door in _listOfDoors)
        {
            _door.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _door.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Awake()
    {
        _listOfDoors = new List<Door>();
        _listOfEnemies = new List<EnemyLifeComponent>();
        _listEnemyPosition = new List<Vector3>();
    }

    public void Start()
    {
        myState = SalaStates.Inactiva;
    }

    // Update is called once per frame
    void Update()
    {       
        if (_listOfEnemies.Count <= 0 && myState==SalaStates.Activa)
        {
            DestroyDoors();
            myState = SalaStates.Completada;
            
        }
        
    }
}
