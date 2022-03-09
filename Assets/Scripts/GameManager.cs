using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _player, _playerGun, _necromancer, _weakEnemy, _chest;
    [SerializeField]
    private FollowComponent _cam;
    [SerializeField]
    public UI_Manager _uiManager;

    private List<WeakEnemy> _listOfWeakEnemies;
    //private WeakEnemy _weakEnemies;
    private PlayerManager _playerManager;

    #endregion

    public Vector3 _playerDirection, _necroPosition;
    public int add = 0;
    public bool vivo;
    public List<GameObject> itemList;
    private float cooldown = 2.5f;
    private float _elapsedTime = 0f;

    static private GameManager _instance;
    static public GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

	#region methods
    public void SetCooldownBar(bool setter)
	{
        _uiManager.SetCooldownBar(setter);
	}

    public void ShowHealth(float health )
	{
        _uiManager.ShowHealth(health);
	}

    public void ShowActiveCooldown(float cooldown, float maxCooldown)
	{
        _uiManager.ShowActiveCooldown(cooldown, maxCooldown);
	}

    public void ActiveObjectInfo(string name, string description)
	{
        _uiManager.ActiveObjectInfo(name, description);
	}

    public void OnPlayerDie()
	{
        _playerGun.SetActive(false);
        _cam.lerpParameter = 0;
        _cam.GetComponent<FollowComponent>().SetPlayerDead();
        //Necro
        vivo = false;
	}

    //Necromancer
    public Vector3 PlayerPosition()
    {
        _playerDirection = _player.transform.position;
        return _playerDirection;
    }  
    public void RegisterWeakEnemy(WeakEnemy enemyToAdd)
    {
         _listOfWeakEnemies.Add(enemyToAdd);
    }

    public void OnWeakDies(WeakEnemy deadweak)
    {
        _listOfWeakEnemies.Remove(deadweak);
        add--;
    }

    public void WeakInstantation(GameObject weakEnemy, Vector3 pos)
    {
        Instantiate(weakEnemy, pos, Quaternion.identity);
    }
	#endregion

	private void Awake()
	{
        _instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
        _listOfWeakEnemies = new List<WeakEnemy>();
        
        vivo = true;
    }

    private void Update()
    { 
        _elapsedTime += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerManager.Instance.ChangeMaxLife(10);
            _elapsedTime = 0;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerManager.Instance.ChangePlayerLife(10);
            _elapsedTime = 0;
        }
    }

}
