using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeCounterHunter : MonoBehaviour
{
	public Text contador;
	public GameObject canvasFinal;
	public GameObject Hunter;
	
	/*
	 * Getting the Players rigidbody component.
	 * And grabbing the mainCamera from Players child transform.
	 */

	IEnumerator timer()
	{
		int tiempo = 150;
		while (tiempo > 0)
		{
			tiempo = tiempo - 1;
			yield return new WaitForSeconds(1);
			if (contador)
			{
				contador.text = tiempo.ToString();
			}
		}
		//Aqui iria lo del canvas si gana o pierde
		if (canvasFinal)
		{
			canvasFinal.SetActive(true);
			(Hunter.GetComponent("MouseLookScript") as MonoBehaviour).enabled = false;
			(Hunter.GetComponent("PlayerMovementScript") as MonoBehaviour).enabled = false;
			Screen.lockCursor = false;
		}
		// Destroy(gameObject);
	}
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(timer());

	}

	// Update is called once per frame
	void Update()
	{

	}
}