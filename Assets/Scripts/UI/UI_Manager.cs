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
    private Slider _healthSlider, _cooldownSlider;
    #endregion

    #region methods
    public void ShowHealth(float health)
	{
        _healthSlider.value = health;
	}

    public void ShowCooldown(float cooldown, float maxCooldown)
	{
        _cooldownSlider.value = cooldown/maxCooldown;
	}
    #endregion
}
