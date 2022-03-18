using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountainScript : MonoBehaviour
{
    public bool _isClogged;
    public SpongeSalaManager _salaManager;

   
    private Transform _myTransform;
    
    [SerializeField]
    private GameObject _bulletPrefab, _center;

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpongeMovement _spongeMovement = collision.gameObject.GetComponent<SpongeMovement>();
        if (_spongeMovement != null)
        {
            if (_spongeMovement.movement != Vector3.zero)
            {
                _isClogged = true;
                // Cambio de sprite de fuente a amarillo (como si las esponjas las hubieran tapado)
                
                // Comprueba si están todas las fuentes tapadas
                _salaManager.CompruebaFuente();
            }
            // Si queremos que se destruya en la pared, meter el destroy en el if
            // Como está ahora, se destruye siempre al tocar la fuente
            Destroy(_spongeMovement.gameObject);
        }
    }

    public void Destaponar()
    {
        
        Vector3 direction = (_center.transform.position - _myTransform.position).normalized;
        GameObject bullet = Instantiate(_bulletPrefab, _myTransform.position + direction*2 , Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();   
        rb.AddForce(direction * 5, ForceMode2D.Impulse);
    }
    #endregion

    void Start()
    {
        _isClogged = false;
        _myTransform = transform;
    }

}
