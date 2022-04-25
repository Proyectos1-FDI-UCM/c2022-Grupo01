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

    [SerializeField] private Transform _playerToJuan, _playerToSponge;

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
    void SetDeathMenu(bool setDeathMenu)
    {
        _uiManager.SetDeathMenu(setDeathMenu);
	}

	void SetWinMenu(bool setDeathMenu)
	{
		_uiManager.SetWinMenu(setDeathMenu);
	}

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
    public void HideBossBar()
    {
        _uiManager.HideBossbar();
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

    public IEnumerator OnPlayerVictory()
	{
        //Musica de victoria
        _player.GetComponent<PlayerMovement>().enabled = false;
        _player.GetComponent<PlayerAttack>().enabled = false;
        _player.GetComponent<PlayerLife>().enabled = false;
        _cam.lerpParameter = 0;
        _cam.GetComponent<FollowComponent>().SetPlayerDead();
        _uiManager.HideHUD(true);

        yield return new WaitForSeconds(3f);
        SetWinMenu(true);
    }
    public IEnumerator OnPlayerDie()
	{
        AudioManager.Instance.PlayAfter(3.5f, "Out");
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
        _playerManager._playerMovement.gancho = false;
	}

    public void Quit()
    {
        Application.Quit();
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

    public void GoToJuan()
	{
        PlayerManager.Instance.player.transform.position = _playerToJuan.position;
	}

    public void GoToSponge()
	{
        PlayerManager.Instance.player.transform.position = _playerToSponge.position;
	}
    #endregion

    private void Awake()
	{
        _instance = this;
	}

	private void Start()
	{
        try { GetComponent<RandomGenerator>().GenerateFloor(); }
        catch { Debug.Log("There is no random generator"); }
	}
}
