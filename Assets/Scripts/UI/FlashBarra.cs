using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBarra : MonoBehaviour {

    public Color flashColor;
	public float flashDuracion;

	Material mat;

    private IEnumerator flashCoroutine;

	private void Awake() {
		mat = GetComponent<SpriteRenderer>().material;
	}

    private void Start()
    {
        mat.SetColor("_FlashColor", flashColor);
    }

    private void Update() {
		if(Input.GetKeyDown(KeyCode.L))
			Flash();
	}

	private void Flash(){
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = Flashea();
        StartCoroutine(flashCoroutine);
    }


    private IEnumerator Flashea()
    {
        float lerpTime = 0;

        while (lerpTime < flashDuracion)
        {
            lerpTime += Time.deltaTime;
            float perc = lerpTime / flashDuracion;

            CuantoFlash(1f - perc);
            yield return null;
        }
        CuantoFlash(0);
    }
    private void CuantoFlash(float flashAmount)
    {
        mat.SetFloat("_FlashAmount", flashAmount);
    }

}