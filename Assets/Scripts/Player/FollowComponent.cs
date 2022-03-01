using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowComponent : MonoBehaviour
{
    public Transform target;
    public float lerpParameter = 2/3f;
    public Vector3 offset;

    private Vector3 _lastPosition;
    private Camera cam;
    private bool _playerDead = false;

	#region methods
    public void SetPlayerDead()
	{
        _playerDead = !_playerDead;
        _lastPosition = transform.position;
	}
	#endregion
	// Start is called before the first frame update
	void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 mouse = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 desiredPosition = mouse + offset;
        if (!_playerDead) desiredPosition = Vector3.Lerp(target.position, desiredPosition, lerpParameter);
        else
        {
            _lastPosition = transform.position;
            desiredPosition = Vector3.Lerp(_lastPosition, target.position, 0.001f);
            desiredPosition.z = offset.z;
        }
        transform.position = desiredPosition;
    }
}
