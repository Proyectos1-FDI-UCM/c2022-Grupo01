using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    #region references
    [SerializeField] private Slider _healthSlider, _cooldownSlider, _secondaryHealthSlider, _bossbarSlider;
    [SerializeField] private GameObject _objectInfo, _objectInfoPrefab, _pauseMenu, _loadingScreen, _stats, _deathMenu, _winMenu, _controls, _controlsButton, _secondControlsButton;
    [SerializeField] private TextMeshProUGUI _healthBarText, _bossbarText, _playerSpeed, _playerRangeDamage, _playerMeleeDamage;
    [SerializeField] private FollowComponent _cam;
    PlayerManager _playerManager;
    #endregion

    #region properties
    private float _normalMaxHealth = 100f;
    private float _sliderWidth = 275.5359f;   //width of the slider
    private GameObject _player;
    #endregion

    #region methods
    public void ShowHealth(float health)
	{
        if(health > _normalMaxHealth)
        {
            _healthSlider.value = _normalMaxHealth;
            _secondaryHealthSlider.gameObject.SetActive(true);
            float healthDiff = PlayerManager.Instance.maxHealth - _normalMaxHealth;
            _secondaryHealthSlider.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (_sliderWidth * (healthDiff/100) , _secondaryHealthSlider.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            _secondaryHealthSlider.maxValue = Mathf.Clamp(healthDiff, 0, 100);
            _secondaryHealthSlider.value = health - _normalMaxHealth;
        }
        else
        {
            _secondaryHealthSlider.gameObject.SetActive(false);
            _healthSlider.value = health;
        }

        _healthBarText.text = "Hydration: " + health;
    }

    public void CreateBossBar(string bossName, float maxHealth)
    {
        _bossbarSlider.gameObject.SetActive(true);
        _bossbarSlider.maxValue = maxHealth;
        _bossbarText.text = bossName;
    }

    public void UpdateBossbar(float bossHealth)
    {
        if (bossHealth > 0)
        {
            _bossbarSlider.value = bossHealth;
        }
        else HideBossbar();   
    }

    public void HideBossbar()
    {
        _bossbarSlider.gameObject.SetActive(false);
    }

    public void ShowActiveCooldown(float cooldown, float maxCooldown)
	{
        _cooldownSlider.value = cooldown/maxCooldown;
	}

    public void SetCooldownBar(bool setter)
	{
        _cooldownSlider.gameObject.SetActive(setter);
        if(setter) _cooldownSlider.value = _cooldownSlider.maxValue;
	}

    public void ObjectInfo(string name, string description)
	{
        GameObject objectInfo = Instantiate(_objectInfoPrefab, _objectInfo.transform.position, Quaternion.identity);
        objectInfo.transform.SetParent(_objectInfo.transform);
        objectInfo.transform.GetChild(0).GetComponentInChildren<Text>().text = name.ToUpper();
        objectInfo.transform.GetChild(1).GetComponentInChildren<Text>().text = description.ToUpper();
        StartCoroutine(DeactivateObjectInfoAfterSeconds(objectInfo, 4));
	}

    public void DeactivateObjectInfo(GameObject objectInfo)
	{
        Destroy(objectInfo);
    }

    public void SetLoadingScreen(bool setLoadingScreen)
    {
        HideHUD(setLoadingScreen);

        _loadingScreen.SetActive(setLoadingScreen);
    }

    public void HideHUD(bool hideHUD)
    {
        _cooldownSlider.gameObject.SetActive(!hideHUD);
        _secondaryHealthSlider.transform.parent.gameObject.SetActive(!hideHUD);
        _stats.gameObject.SetActive(!hideHUD);
        _healthBarText.gameObject.SetActive(!hideHUD);
    }

    public void PauseMenu(bool pause)
	{
        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
        //GameManager.Instance.StopEnemies();
        GameManager.Instance.enabled = !pause;
		_pauseMenu.SetActive(pause);
        _player.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().enabled = !pause;
        _cam.enabled = !pause;
    }

    public void UpdateSpeed()
	{
        _playerSpeed.text = _playerManager.speed.ToString();
    }
    public void UpdateMeleeDamage()
	{
        _playerMeleeDamage.text = _playerManager.meleeDamage.ToString();
	}
    public void UpdateRangeDamage()
	{
        _playerRangeDamage.text = _playerManager.rangeDamage.ToString();
    }

    public void SetDeathMenu(bool setDeathMenu)
    {
        _deathMenu.SetActive(setDeathMenu);
    }

    public void SetWinMenu(bool setWinMenu)
    {
        _winMenu.SetActive(setWinMenu);
    }

    public void SetControlsImage(bool image)
	{
        _controls.SetActive(image);
        _controlsButton.SetActive(!image);
        _secondControlsButton.SetActive(image);
	}
    #endregion

    IEnumerator DeactivateObjectInfoAfterSeconds(GameObject objectInfo, float seconds)
	{
        yield return new WaitForSeconds(seconds);
        DeactivateObjectInfo(objectInfo);
	}
    private void Start()
    {
        _cooldownSlider.gameObject.SetActive(false);
        _player = PlayerManager.Instance.player;
        _playerManager = GetComponent<PlayerManager>();
        _deathMenu.SetActive(false);
        _winMenu.SetActive(false);
    }
}
