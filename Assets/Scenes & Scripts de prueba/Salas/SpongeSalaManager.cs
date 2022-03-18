using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeSalaManager : MonoBehaviour
{
    [SerializeField]
    private List<BossDoor> _listOfDoors;
    [SerializeField]
    private List<SpongeLifeComponent> _listOfEnemies;
    private List<Vector3> _listEnemyPosition;
    public enum SalaStates { Inicial, Inactiva, Activa, Completada };
    [SerializeField]
    private List<fountainScript> _listOfFountains;
    public SalaStates myState;
    private BossMovement _bossMovement;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAttack _player = collision.gameObject.GetComponent<PlayerAttack>();
        SpongeLifeComponent _enemy = collision.gameObject.GetComponent<SpongeLifeComponent>();
        BossDoor _door = collision.gameObject.GetComponent<BossDoor>();
        fountainScript _fountain = collision.gameObject.GetComponent<fountainScript>();

        if (_player != null)
        {
            EnterSala();
        }
        if (_enemy != null)
        {
            _enemy.sala = this;
            _enemy.gameObject.GetComponentInChildren<SpongeDetectPlayer>().sala = this;
            _enemy.GetComponent<BossMovement>()._mySpongeSalaManager = this;
            _enemy.Register();
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

    public void DestaparFuentes()
    {
        foreach (fountainScript fountain in _listOfFountains)
        {
            fountain.Destaponar();
            fountain._isClogged = false;
        }
    }

    // ¿Queremos que salga de la sala con la bolsa de hielo?
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
        foreach (SpongeLifeComponent enemy in _listOfEnemies)
        {
            enemy.gameObject.transform.position = _listEnemyPosition[enemy.ID];
            enemy.gameObject.GetComponentInChildren<SpongeDetectPlayer>().Deactivate();

        }
    }

    public void EnterSala()
    {
        myState = SalaStates.Activa;
        foreach (BossDoor door in _listOfDoors)
        {
            door.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            door.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
        }
    }
    public void RegisterEnemy(SpongeLifeComponent enemy)
    {
        _listOfEnemies.Add(enemy);
        enemy.ID = _listOfEnemies.Count - 1;
        _listEnemyPosition.Add(enemy.gameObject.transform.position);
        Debug.Log("enemy: " + _listOfEnemies.Count);
    }

    public void OnEnemyDies(SpongeLifeComponent enemy)
    {
        _listOfEnemies.Remove(enemy);
    }

    public void RegisterDoor(BossDoor _door)
    {
        _listOfDoors.Add(_door);
        Debug.Log("doors: " + _listOfDoors.Count);
    }
    public void DestroyDoors()
    {
        foreach (BossDoor _door in _listOfDoors)
        {
            _door.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _door.gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    private void Awake()
    {
        _listOfDoors = new List<BossDoor>();
        _listOfEnemies = new List<SpongeLifeComponent>();
        _listEnemyPosition = new List<Vector3>();
        myState = SalaStates.Inicial;
        _listOfFountains = new List<fountainScript>();
    }

    void Update()
    {
        if (myState == SalaStates.Inicial)
        {
            DestroyDoors();
            myState = SalaStates.Inactiva;
        }
        else if (_listOfEnemies.Count == 0 && myState == SalaStates.Activa)
        {
            DestroyDoors();
            myState = SalaStates.Completada;
        }

    }
}
