using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fountainScript : MonoBehaviour
{
    public bool _isClogged;
    public SpongeSalaManager _salaManager;
    [SerializeField]
    private float _speed = 10f;

    private SpriteRenderer render;
    public Animator animator;
    public Sprite clogged;

   
    private Transform _myTransform;
    
    [SerializeField]
    private GameObject _bulletPrefab, _center;
    [SerializeField]
    private float delay=2.5f;

    #region methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpongeMovement _spongeMovement = collision.gameObject.GetComponent<SpongeMovement>();
        if (_spongeMovement != null)
        {
            if (_spongeMovement.movement != Vector3.zero)
            {
                _isClogged = true;
                animator.enabled = false;
                render.sprite= clogged;

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
        rb.AddForce(direction * _speed, ForceMode2D.Impulse);
        Destroy(bullet,delay);
    }
    #endregion

    void Start()
    {
        _isClogged = false;
        _myTransform = transform;
        render = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        animator.enabled = true;
        render.sprite = clogged;
        _isClogged = true;
        animator.enabled = false;
    }

}
