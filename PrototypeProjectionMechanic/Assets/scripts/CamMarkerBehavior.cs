using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMarkerBehavior : MonoBehaviour {

	public GameObject player;
	public GameObject playerClone;
	public GameObject playerClone2;
	public GameObject playerClone3;

	private SpriteRenderer spriteClone1;
	private SpriteRenderer spriteClone2;
	private SpriteRenderer spriteClone3;

	public float offsetCoef = 2;

	void Start()
	{
		//GET CLONE SPRITE
		spriteClone1 = playerClone.GetComponent<SpriteRenderer> ();
		spriteClone2 = playerClone2.GetComponent<SpriteRenderer> ();
		spriteClone3 = playerClone3.GetComponent<SpriteRenderer> ();
	}


	void Update () {

		if (spriteClone1.enabled == true) 
		{
			Vector3 position = player.transform.position + (playerClone.transform.position - player.transform.position) / offsetCoef;
			transform.position = position;
		}

		if (spriteClone2.enabled == true) 
		{
			Vector3 position = player.transform.position + (playerClone2.transform.position - player.transform.position) / offsetCoef;
			transform.position = position;
		}

		if (spriteClone3.enabled == true) 
		{
			Vector3 position = player.transform.position + (playerClone3.transform.position - player.transform.position) / offsetCoef;
			transform.position = position;
		}
	}
}
