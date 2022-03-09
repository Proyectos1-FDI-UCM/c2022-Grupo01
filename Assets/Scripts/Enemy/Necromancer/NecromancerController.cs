using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerController : MonoBehaviour
{
    private Transform _mytransform;
    [SerializeField]
    private float _currentTime, _timeLeft = 3;
    //public float add;

    [SerializeField]
    private GameObject _weakEnemy;

    #region methods
    public void ExecuteNecromancerController()
    {
        this.enabled = true;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _mytransform = transform;
        _currentTime = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //GameManager.Instance.NecroPosition(_mytransform.position);

        _currentTime = _currentTime - Time.deltaTime;
        if (_currentTime < 0 && GameManager.Instance.vivo == true && GameManager.Instance.add < 4)
        {

            GameManager.Instance.WeakInstantation(_weakEnemy, _mytransform.position);
            _currentTime = _timeLeft;
            GameManager.Instance.add++;
        }
        else if (GameManager.Instance.add >= 4)
        {
            GetComponent<RangeAttack>().enabled = true;
        }
    }
}
