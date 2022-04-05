using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    #region properties
    private bool _canOpenChest = false;
    private bool _firstApproach = false;
    private GameObject _shadowObject;
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

        if(player != null)
        {
            _canOpenChest = true;
            _firstApproach = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerLife player = other.GetComponent<PlayerLife>();

        if (player != null)
        {
            _canOpenChest = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_hasBeenOpened)
		{
            if (_canOpenChest)
            {
                if (!_shadowCasted)
                {
                    _shadowObject = Instantiate(chest, transform.position, Quaternion.identity);
                    _shadowCasted = true;
                }
                if(Input.GetKey(KeyCode.O))
                {
                    int rnd = Random.Range(3, GameManager.Instance.itemList.Count);
                    _objectHeld = GameManager.Instance.itemList[rnd];
                    GetComponent<SpriteRenderer>().sprite = _newSprite;
                    _shadowObject.GetComponent<SpriteRenderer>().sprite = _newOpenedSprite;
                    Instantiate(_objectHeld, transform.position + _offset, Quaternion.identity);
                    _canOpenChest = false;
                    FindObjectOfType<AudioManager>().Play("OpenedChest");
                    GameManager.Instance.itemList.RemoveAt(rnd); 
                    _hasBeenOpened = true;
                }
            }

            else if (_firstApproach && !_canOpenChest)
            {
                if (_shadowObject != null) 
                { 
                    Destroy(_shadowObject); 
                    _shadowCasted = false; 
                }
            }
		}
    }
}