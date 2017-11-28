using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneController : MonoBehaviour {

	// Use this for initialization
	public KeyCode right;
	public KeyCode left;
	public KeyCode up;
	public KeyCode down;

	public KeyCode attack;
	public BoxCollider2D attackHitBox;
	public float attackHitBoxDuration;
	private float currentAttackHitBoxDuration;

	public float speed = 2;
	private Rigidbody2D body;

	private TimeController timeController; //pour stocker les object qui on en component le script TimeController
	public TimeControllerClone timeControllerClone;



	// Use this for initialization
	void Start () {
		timeController = FindObjectOfType (typeof(TimeController)) as TimeController;
		body = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {

		Attack (); //méthode pour les attack

		bool isRecorded = timeControllerClone.isRecorded; //le recorded de son propre timeControllerClone

		//mouvement seulement s'il y a record
		if (timeController.IsRecording == true && isRecorded == true) {
			Move ();
			Debug.Log ("possible to move for clone");
		}
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

		float walkV = Input.GetAxis("Vertical");    //récupère l'axe Vertical pour bouger
		body.velocity = new Vector2(walkV * speed , body.velocity.y);  

		float walkH = Input.GetAxis("Horizontal");      //récupère l'axe Horizontal pour bouger
		body.velocity = new Vector2(walkH * speed , body.velocity.x );

	}
		
	void Attack(){

		//check si input et si hitbox d'attaque est inactive
		if (Input.GetKeyDown (attack) && attackHitBox.enabled == false) 
		{
			attackHitBox.enabled = true; //active la hitbox
			currentAttackHitBoxDuration = 0.0f;//met le compteur à zéro
		}

		//SI HITBOX ACTIVE
		if (attackHitBox.enabled == true) 
		{
			currentAttackHitBoxDuration += Time.deltaTime;  //incrémente le compteur

			//desactivation de la hitbox
			if (currentAttackHitBoxDuration >= attackHitBoxDuration)
				attackHitBox.enabled = false;
		}
		//si ce n'est pas le cas la rend active et lance un compteur d'activité de la box
	}
}
