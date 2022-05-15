using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		if (PlayerManager.Instance.player.activeSelf)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				_pause = !_pause;
				GameManager.Instance.PauseMenu(_pause);
			}
			if (Input.GetKeyDown(KeyCode.K))
			{
				_pause = !_pause;
				GameManager.Instance.GenerateNewFloor();
			}

			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
			}
		}
    }
}
