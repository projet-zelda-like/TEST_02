using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : MonoBehaviour {

	public SpriteRenderer sprite; //le sprite du GameObject

	void Start () {
		sprite.color = Color.yellow; //change sa couleur quand il est mort
	}
}
