using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PomperActiveObject : MonoBehaviour
{
    #region parameters
    [SerializeField]
    private float _cooldownTime;
    [SerializeField]
    private GameObject _bubblePrefab;
    [SerializeField]
    private Vector3 _instantiateOffset = new Vector3 (-0.3f, 0.78f, 0);
    #endregion
    #region properties
    private float _elapsedTime;
    public bool _bubbleActive;
    #endregion
    #region references
    private Transform _myTransform;
    #endregion
    #region methods
    private void Activate()
    {
        this.enabled = true;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _elapsedTime = _cooldownTime + 1f;
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_bubbleActive)
        {
            if(_elapsedTime > _cooldownTime)
            {
                _bubbleActive = true;
                Vector3 instantiatePosition = _myTransform.position + _instantiateOffset;
                GameObject bubble = Instantiate(_bubblePrefab, instantiatePosition, Quaternion.identity);
                bubble.GetComponent<BubbleAttack>().pompero = this;
            }
            _elapsedTime += Time.deltaTime;
        }
        else
        {
            _elapsedTime = 0f;
        }
    }
}
