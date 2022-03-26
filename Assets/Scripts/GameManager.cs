using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _player, _playerGun, _necromancer, _weakEnemy, _chest, _pauseMenu, _canvas;
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
    private bool pause = false;

    //Numero de enemigos eliminados durante la partida, para objeto Bayeta
    [SerializeField]
    public int _deadEnemyCount = 0;

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

    public void ObjectInfo(string name, string description)
	{
        _uiManager.ObjectInfo(name, description);
	}

    public void OnPlayerDie()
	{
        _player.SetActive(false);
        _cam.lerpParameter = 0;
        _cam.GetComponent<FollowComponent>().SetPlayerDead();
        //Necro
        vivo = false;
        SceneManager.LoadScene("Menu");
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

    public void DeadEnemies()
    {
        _deadEnemyCount += 1;
    }

    private void PauseMenu(bool pause)
	{
        _uiManager.PauseMenu(pause);
	}

    public void LoadPlayTutorial()
    {
        SceneManager.LoadScene("LoadingTutorial");
    }

    public void LoadingPlayNormalGame()
    {
        SceneManager.LoadScene("LoadingGame");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
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
        // Menú de pausado
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            PauseMenu(pause);
        }
    }
}
