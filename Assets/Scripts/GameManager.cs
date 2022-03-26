using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region references
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private FollowComponent _cam;
    [SerializeField]
    public UI_Manager _uiManager;

    //private WeakEnemy _weakEnemies;
    private PlayerManager _playerManager;

    #endregion

    public Vector3 _playerDirection, _necroPosition;
    public List<GameObject> itemList;

    //Numero de enemigos eliminados durante la partida, para objeto Bayeta
    [HideInInspector] public int _deadEnemyCount = 0;

    static private GameManager _instance;
    static public GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

	#region methods
	#region UI
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

    public void PauseMenu(bool pause)
	{
        _uiManager.PauseMenu(pause);
	}

	#endregion

    public void OnPlayerDie()
	{
        _player.SetActive(false);
        _cam.lerpParameter = 0;
        _cam.GetComponent<FollowComponent>().SetPlayerDead();
        //Necro
        SceneManager.LoadScene("Menu");
    }
    //Necromancer
    public void DeadEnemies()
    {
        _deadEnemyCount += 1;
    }

    public void GenerateNewFloor()
	{
        GetComponent<RandomGenerator>().GenerateFloor();
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

	private void Start()
	{
        GetComponent<RandomGenerator>().GenerateFloor();
	}
}
