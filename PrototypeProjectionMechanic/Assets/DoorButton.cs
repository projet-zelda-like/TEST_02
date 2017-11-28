using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {


	public GameObject connectedDoor; //la porte connecté au bouton
	public bool isOn = false; //état ouvert ou non
	public SpriteRenderer sprite; //le sprite du levier

	public bool isTimerOn = false; //bool qui indique si l'ouverture est régler par un timer
	public float timeActive = 5.0f; 
	private float timeActiveCount = 0.0f;

	private TimeController timeController;

	private bool isCloneIn = false;
	private bool isCloneIn2 = false;
	private bool isCloneIn3 = false;
	private bool isPlayerIn = false;

	// Use this for initialization
	void Start () {
		timeController = FindObjectOfType (typeof(TimeController)) as TimeController;
	}
	
	// Update is called once per frame
	void Update () {

		//EN CAS DE TRIGGER DU CLONE
		if (isCloneIn == true) 
		{
			GameObject clone = GameObject.Find ("PlayerClone1"); //trouve le clone
			float distanceClone = Vector3.Distance (transform.position, clone.transform.position); //la distance bouton-clone

			//check si le clone est loin
			if (distanceClone > 5.0f)
				isCloneIn = false; //bool de présence en false 
		}

		if (isCloneIn2 == true) 
		{
			GameObject clone = GameObject.Find ("PlayerClone2"); //trouve le clone
			float distanceClone = Vector3.Distance (transform.position, clone.transform.position); //la distance bouton-clone

			//check si le clone est loin
			if (distanceClone > 5.0f)
				isCloneIn = false; //bool de présence en false 
		}

		if (isCloneIn3 == true) 
		{
			GameObject clone = GameObject.Find ("PlayerClone3"); //trouve le clone
			float distanceClone = Vector3.Distance (transform.position, clone.transform.position); //la distance bouton-clone

			//check si le clone est loin
			if (distanceClone > 5.0f)
				isCloneIn = false; //bool de présence en false 
		}

		//ACTIVATION DU BOUTON
		if (isCloneIn == true || isPlayerIn == true || isCloneIn2 == true || isCloneIn3 == true) 
		{
			isOn = true;
		} 
		else 
		{
			isOn = false;
		}


		//OUVERTURE ET FERMETURE
		if (isOn == false) //porte fermé
		{
			connectedDoor.GetComponent<DoorSystem> ().isOpen = false; //ferme la porte connected
			sprite.color = Color.white; //met en blanc
		} 
		else  //porte ouverte
		{
			connectedDoor.GetComponent<DoorSystem> ().isOpen = true; //ouvre la porte connected
			sprite.color = Color.red; //met en rouge le levier

			#region Timer
			if (isTimerOn == true) 
			{

				//incrémente le compteur
				timeActiveCount += Time.deltaTime;

				if (timeActiveCount >= timeActive) 
				{
					isOn = false; //ferme la porte
					timeActiveCount = 0; //reset le compteur
				}
			}
			#endregion
		}

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<PlayerController> () != null)
			isPlayerIn = true;

		if (other.GetComponent<TimeControllerClone> () != null && timeController.IsRecording == false) 
		{
			if(other.name == "PlayerClone1")
				isCloneIn = true;

			if(other.name == "PlayerClone2")
				isCloneIn2 = true;

			if(other.name == "PlayerClone3")
				isCloneIn3 = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<PlayerController> () != null)
			isPlayerIn = false;
	}
}