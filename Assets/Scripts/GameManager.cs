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

    public void ShowHealth(float health)
	{
        _uiManager.ShowHealth(health);
	}

    public void CreateBossBar(string name, float health)
    {
        _uiManager.CreateBossBar(name, health);
    }

    public void UpdateBossBar(float health)
    {
        _uiManager.UpdateBossbar(health);
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

    public void SetLoadingScreen(bool setLoadingScreen)
    {
        _player.SetActive(!setLoadingScreen);
        _uiManager.SetLoadingScreen(setLoadingScreen);
    }
	#endregion

    public IEnumerator OnPlayerDie()
	{
        _player.SetActive(false);
        _cam.lerpParameter = 0;
        _cam.GetComponent<FollowComponent>().SetPlayerDead();
        _uiManager.HideHUD(true);
        yield return new WaitForSeconds(3f);
        SetDeathMenu(true);
    }
    //Necromancer
    public void DeadEnemies()
    {
        PlayerManager.Instance.IncreaseEnemyCounter();
    }

    public void GenerateNewFloor()
	{
        GetComponent<RandomGenerator>().GenerateFloor();
	}

    public void Quit()
    {
        Application.Quit();
    }

    void SetDeathMenu(bool setDeathMenu)
    {
        _uiManager.SetDeathMenu(setDeathMenu);
    }

    public void LoadGame()
	{
        SceneManager.LoadScene("CompleteScene");
	}

    public void LoadTutorial()
	{
        SceneManager.LoadScene("Tutorial");
	}

    public void BackToMenu()
	{
        PauseMenu(false);
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
