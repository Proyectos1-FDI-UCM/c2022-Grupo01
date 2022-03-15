using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    #region references
    [SerializeField] private Slider _healthSlider, _cooldownSlider, _secondaryHealthSlider;
    [SerializeField] private GameObject _objectInfo, _objectInfoPrefab;
    [SerializeField] private TextMeshProUGUI _healthBarText;
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

        _healthBarText.text = "Hidratación: " + health;
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

    public void ActiveObjectInfo(string name, string description)
	{
        GameObject objectInfo = Instantiate(_objectInfoPrefab, _objectInfo.transform.position, Quaternion.identity);
        objectInfo.transform.SetParent(_objectInfo.transform);
        objectInfo.transform.GetChild(0).GetComponentInChildren<Text>().text = name.ToUpper();
        objectInfo.transform.GetChild(1).GetComponentInChildren<Text>().text = description.ToUpper();
        StartCoroutine(Cosas(objectInfo));
	}

    public void DeactivateObjectInfo(GameObject objectInfo)
	{
        Destroy(objectInfo);
    }
    #endregion

    IEnumerator Cosas(GameObject objectInfo)
	{
        yield return new WaitForSeconds(4);
        DeactivateObjectInfo(objectInfo);
	}
    private void Start()
    {
        _cooldownSlider.gameObject.SetActive(false);
    }
}
