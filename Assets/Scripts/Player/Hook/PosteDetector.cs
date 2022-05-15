using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosteDetector : MonoBehaviour
{
    #region references
    public Transform travelPoint;
	#endregion

	public void OnTriggerEnter2D(Collider2D collision)
    {
        gancho _gancho = collision.gameObject.GetComponent<gancho>();
        if (_gancho != null)
        {
            _gancho.LanzaGancho(transform.position, travelPoint.position);
        }
    }
}