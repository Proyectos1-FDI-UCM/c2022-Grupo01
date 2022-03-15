using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountainScript : MonoBehaviour
{
    public bool _isClogged;
    public SalaManager _salaManager;

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpongeMovement _spongeMovement = collision.gameObject.GetComponent<SpongeMovement>();
        if (_spongeMovement != null)
        {
            if (_spongeMovement.movement != Vector3.zero)
            {
                _isClogged = true;
                _salaManager.CompruebaFuente();
            }
            Destroy(_spongeMovement.gameObject);
        }
    }
    #endregion

    void Start()
    {
        _isClogged = false;
    }

    void Update()
    {
        
    }
}
