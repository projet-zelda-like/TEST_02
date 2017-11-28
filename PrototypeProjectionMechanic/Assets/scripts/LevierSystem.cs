using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierSystem : MonoBehaviour {

	public bool isActivated = false;
	private bool originState = false;
	public GameObject connectedDoor;
	public SpriteRenderer spriteDoor;
	public TimeController timeController;
	private ArrayList stateinTime;

	public bool isTimerOn = false; //bool qui indique si l'ouverture est régler par un timer
	public float timeActive = 5.0f;
	private float timeActiveCount = 0.0f;

	void Start(){
		stateinTime = new ArrayList ();
		originState = isActivated; //enregistre l'état d'origine de la bool d'activation
	}

	void Update () 
	{

		/*
		//réglage selon le temps
		if (timeController.IsRecording == true) 
		{
			//enregistre l'état de is activated à chaque frame
			stateinTime.Add (isActivated);
		} 
		else if (stateinTime.Count > 0) 
		{
			//remet l'état enregistrer a chaque frame
			isActivated = (bool)stateinTime [0]; //lis la première entré
			stateinTime.RemoveAt (0);//supprime la premier entré de la liste
		} 
		else  if (isTimerOn == false)
		{
			//reset sur l'état d'origine
			isActivated = originState;
		}*/

		#region ouverture et fermeture de la porte

		if (isActivated == false) //porte fermé
		{
			connectedDoor.GetComponent<DoorSystem> ().isOpen = false; //ferme la porte connected
			spriteDoor.color = Color.white; //met en blanc
		} 
		else  //porte ouverte
		{
			connectedDoor.GetComponent<DoorSystem> ().isOpen = true; //ouvre la porte connected
			spriteDoor.color = Color.red; //met en rouge le levier

			if (isTimerOn == true) {
				
				//incrémente le compteur
				timeActiveCount += Time.deltaTime;

				if (timeActiveCount >= timeActive) 
				{
					isActivated = false; //ferme la porte
					timeActiveCount = 0; //reset le compteur
				}
			}
		}
		#endregion
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<SpriteRenderer>().enabled == true)	
			isActivated = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<SpriteRenderer> ().enabled == true)
			isActivated = false;
	}
}
