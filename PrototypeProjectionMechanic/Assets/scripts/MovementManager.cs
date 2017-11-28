using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public Transform EndPositionGO;     //Point de ref pour le saut, la direction
    Rigidbody2D body;
    public float moveSpeed;  //Vitesse du player
	public float jumpSpeed; //vitesse du saut
	private bool isAbleToMove = true;
    Coroutine C;                //Coroutine utilisée ligne 77
    bool Jump = false;          //Booléen de saut
    Animator anim;      
    bool AnimJump = false;      //Booléen d'animation saut
    
	private TimeController timeController; //le timeController

    // Use this for initialization
    void Start()
    {
		timeController = FindObjectOfType (typeof(TimeController)) as TimeController; // get timecontroller
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       anim.SetInteger("Jump", 0);       //Met l'int dans l'animator a 0 (donc valeur de base)



        if (Jump)
        {
            body.transform.position = Vector3.Lerp(body.position, EndPositionGO.position, Time.deltaTime);      // Fait bouger le player jusqu'au point de reférence "EndPositionGO"
            anim.SetInteger("Jump", 1);        // l'int animation passe en 1 => Scale l'objet
        }

        if (AnimJump == true)
        {
            anim.SetInteger("Jump", 0);        // l'int animation revient a 0
        }

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "JumpBox")     //Vérifie la collision avec le tag "JumpBox"
        {   
			// les lignes suivante permettent de "couper" les controles du joueurs en retirant la vitesse

			isAbleToMove = false; //empeche de bouger
            body.velocity = Vector2.zero;
            Jump = true;        // Passe le booléen en true pour activer le Jump

            if (C != null)      //Si jamais la coroutine a été lancé une fois avant, cela lit ce if
            {
                StopCoroutine(C);       //Stop la coroutine
                C = StartCoroutine(loadingSaut1()); //démarre la coroutine
            }
            else
            {
                C = StartCoroutine(loadingSaut1());     //démarre la coroutine
            }
        }
    }

    IEnumerator loadingSaut1()
    {   
        yield return new WaitForSeconds(1f);    //Set le bool AnimJump en true au bout d'une seconde
        AnimJump = true;

        yield return new WaitForSeconds(1.2f);      //Utilisation de la backUp de moveSpeed pour le player, change les bool de jump et d'anim
		isAbleToMove = true;
        Jump = false;
        AnimJump = false;

    }

	void Move()
	{
		float walkV = Input.GetAxis("Vertical");    //récupère l'axe Vertical pour bouger
		body.velocity = new Vector2(walkV * moveSpeed , body.velocity.y);  

		float walkH = Input.GetAxis("Horizontal");      //récupère l'axe Horizontal pour bouger
		body.velocity = new Vector2(walkH * moveSpeed , body.velocity.x );
	}

	void FixedUpdate(){
		if (isAbleToMove == true && timeController.IsRecording == false) 
		{
			Move (); //permet de bouger
		}
	}
 }