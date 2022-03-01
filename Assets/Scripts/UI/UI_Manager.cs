using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    #region references
    [SerializeField]
    private Text _ammo;
    [SerializeField]
    private Slider _healthSlider, _cooldownSlider, _secondaryHealthSlider;
    private PlayerManager _myPlayerManager;
    #endregion

    #region properties
    private float _normalMaxHealth = 100f;
    private float _sliderWidth = 275.5359f;   //width of the slider
    #endregion
    #region methods
    public void ShowHealth(float health)
	{
        if(health > _normalMaxHealth)
        {
            _secondaryHealthSlider.gameObject.SetActive(true);
            float healthDiff = _myPlayerManager.maxHealth - _normalMaxHealth;
            //Vector3 localscale = _secondaryHealthSlider.gameObject.transform.localScale;
            //_secondaryHealthSlider.gameObject.transform.localScale = new Vector3((health - _normalMaxHealth) / 100, localscale.y, localscale.z);
            _secondaryHealthSlider.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (_sliderWidth * (healthDiff/100) , _secondaryHealthSlider.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            _secondaryHealthSlider.maxValue = healthDiff;
            _secondaryHealthSlider.value = health - _normalMaxHealth / healthDiff;
        }
        else
        {
            _secondaryHealthSlider.gameObject.SetActive(false);
            _healthSlider.value = health;
        }
    }

    public void ShowCooldown(float cooldown, float maxCooldown)
	{
        _cooldownSlider.value = cooldown/maxCooldown;
	}
    #endregion
    private void Start()
    {
        _myPlayerManager = PlayerManager.Instance;
        
    }
}
