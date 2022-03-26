using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region properties
    private bool _pause = false;
	#endregion

	// Start is called before the first frame update
	void Start()
    {
        _pause = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
            _pause = !_pause;
            GameManager.Instance.PauseMenu(_pause);
		}
		if (Input.GetKeyDown(KeyCode.K))
		{
            GameManager.Instance.GenerateNewFloor();
		}
    }
}
