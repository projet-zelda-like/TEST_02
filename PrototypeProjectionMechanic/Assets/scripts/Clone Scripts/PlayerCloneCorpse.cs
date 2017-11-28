using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneCorpse : MonoBehaviour {

	public CircleCollider2D deathCircle; //la hitbox
	public float timeActive; //le temps que dure la hitbox de deathCircle
	private float currentTimeActive = 0.0f; //variable qui sert à régler le compteur
	private bool isActive = true; //determine l'état actif ou non de la hitbox

	void Update()
	{
		//compteur de temps actif

		if (isActive == false)
			return; //annule tout si la hitbox n'est plus active

		currentTimeActive += Time.deltaTime;//incrémente le compteur

		if (currentTimeActive > Time.deltaTime)
			isActive = false; //désactive la hitbox
	}

	void OnTriggerStay2D(Collider2D other){
		if (isActive == true) //check si la hitbox est active
		{
			if (other.tag == "nature") //check si le tag est bon
			{
				other.GetComponent<DeathState> ().enabled = true; //active le component de mort de l'objet
				Debug.Log("KILL");
			}
		}
	}

}
