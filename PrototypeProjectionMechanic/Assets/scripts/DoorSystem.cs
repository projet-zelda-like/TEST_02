using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour {

	public bool isOpen = false;
	public BoxCollider2D doorBox; //Collision de la porte
	public SpriteRenderer spriteDoor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isOpen == true) 
		{
			doorBox.enabled = false;
			spriteDoor.enabled = false;
		} 
		else 
		{
			doorBox.enabled = true;
			spriteDoor.enabled = true;
		}
	}
}
