using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    #region properties
    private bool _colliding = false;
    #endregion

    #region paremeters
    [SerializeField] private float _transitionTime;
    #endregion

    #region references
    [SerializeField] private GameObject _message;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerLife _player = collision.GetComponent<PlayerLife>();

        if (_player != null)
        {
            _colliding = true;
            ShowMessage(_colliding);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerLife _player = collision.GetComponent<PlayerLife>();

        if (_player != null)
        {
            _colliding = false;
            ShowMessage(_colliding);
        }
    }

    private void ShowMessage(bool show)
    {
        _message.SetActive(show);
    }

    IEnumerator NextFloor()
    {
        //añadir transición
        yield return new WaitForSeconds(_transitionTime);
        GameManager.Instance.GenerateNewFloor();
    }
    // Start is called before the first frame update
    void Start()
    {
        _message.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_colliding && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(NextFloor());
        }
    }
}
