using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    #region references
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
    }
    /*  Esto no se usa mas... por ahora
    public void ShowCooldown(float cooldown, float maxCooldown)
	{
        _cooldownSlider.value = cooldown/maxCooldown;
	}
    */
    #endregion
    private void Start()
    {
        
    }
}
