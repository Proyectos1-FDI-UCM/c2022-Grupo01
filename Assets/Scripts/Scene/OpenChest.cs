using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    #region properties
    private bool _canOpenChest = false;
    private bool _firstApproach = false;
    private GameObject _shadowObject = null;
    [SerializeField] private Sprite _newSprite;
    [SerializeField] private Sprite _newOpenedSprite;
    private GameObject _objectHeld;
    private bool _shadowCasted = false;
    private bool _hasBeenOpened = false;
    #endregion

    #region parameters
    [SerializeField] private Vector3 _offset = new Vector3 (0,0.5f,0);
    #endregion

    #region references
    [SerializeField] private GameObject chest;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerLife player = other.GetComponent<PlayerLife>();

        if (player != null && !_hasBeenOpened) CastShadow();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerLife player = other.GetComponent<PlayerLife>();

        if (player != null && !_hasBeenOpened) DestroyShadow();
    }

    void CastShadow()
    {
        if (!_hasBeenOpened)
        {
            _shadowObject = Instantiate(chest, transform.position, Quaternion.identity);
            _shadowCasted = true;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).transform.position = transform.position + new Vector3(0, 1.25f);
        }
    }
    
    void DestroyShadow()
    {
        if (!_hasBeenOpened)
        {
            Destroy(_shadowObject);
            _shadowCasted = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Open()
    {
        if (!_hasBeenOpened)
        {
            int rnd = Random.Range(3, GameManager.Instance.itemList.Count);
            _objectHeld = GameManager.Instance.itemList[rnd];
            GetComponent<SpriteRenderer>().sprite = _newSprite;
            _shadowObject.GetComponent<SpriteRenderer>().sprite = _newOpenedSprite;
            Instantiate(_objectHeld, transform.position + _offset, Quaternion.identity);
            _canOpenChest = false;
            AudioManager.Instance.Play("OpenedChest");
            GameManager.Instance.itemList.RemoveAt(rnd);
            transform.GetChild(0).gameObject.SetActive(false);
            _hasBeenOpened = true;
        }
    }
}