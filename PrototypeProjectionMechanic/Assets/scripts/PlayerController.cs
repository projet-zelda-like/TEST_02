using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public KeyCode right;
	public KeyCode left;
	public KeyCode up;
	public KeyCode down;

	public float speed = 2;
	public Rigidbody2D body;

	private TimeController timeController; //pour stocker les object qui on en component le script TimeController



	// Use this for initialization
	void Start () {
		timeController = FindObjectOfType (typeof(TimeController)) as TimeController;
	}
	
	// Update is called once per frame
	void Update () {

		//mouvement seulement s'il n'y a pas de record
		if(timeController.IsRecording == false)
			Move ();
	}

	void Move(){
		if (Input.GetKey(right))
			transform.Translate (Vector3.right * speed * Time.deltaTime);

		if (Input.GetKey(left))
			transform.Translate (Vector3.left * speed * Time.deltaTime);

		if (Input.GetKey(up))
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		
		if (Input.GetKey(down))
			transform.Translate (Vector3.down * speed * Time.deltaTime);
		
		float walkH = Input.GetAxis ("Vertical");
		body.velocity = new Vector2(walkH * speed, body.velocity.y);
		float walkV = Input.GetAxis("Horizontal");
		body.velocity = new Vector2 (walkV * speed, body.velocity.x);
			}
}
