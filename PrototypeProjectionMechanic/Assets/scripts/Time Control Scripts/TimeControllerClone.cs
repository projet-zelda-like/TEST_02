using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllerClone : MonoBehaviour {

	[Header ("Les GameObject du player")]
	public GameObject player; //le player
	public SpriteRenderer playerClone; //le double du personnage joueur
	public GameObject playerCloneCorpse; //Le cadavre laissé par un double

	[Header ("Les variables du Record")]
	public ArrayList playerClonePositions; //une listes des positions du joueur clone dans le temps.
	public bool IsPlaying = false; //bool qui indique si un enregistrement est actuellement joué
	public bool isRecorded = false; //bool qui indique si clone est en enregistrement
	public int MaxRecordedEntries; //le maximum de frame à enregistrer

	//compteur pour enregistrement
	public int frameRateRecord = 6; //taux d'enregistrement des actions
	private int frameCount = 0; //compteur du record
	private int frameTime = 0; //compteur du play

	private TimeController timeController; //pour stocker les object qui on en component le script TimeController

	//play variables
	private Vector3 previousPosition;
	private Vector3 currentPosition;
	private Vector3 nextPosition;
	public Vector3 InactivePosition;

	[Header ("Input")]
	public KeyCode actionClone;

	// Use this for initialization
	void Start () 
	{
		timeController = FindObjectOfType (typeof(TimeController)) as TimeController; //get time controller
		MaxRecordedEntries = timeController.MaxRecordedEntries; //récupère le nombre d'entré maximale sur le time controller
		playerClonePositions = new ArrayList ();//initialisation de listes
	}
	
	// Update is called once per frame
	void Update () 
	{


			//check si le joueur appui sur la touche
			if (Input.GetKeyDown (actionClone)) 
			{
				
				//check si un autre clone est en enregistrement
				if (timeController.IsRecording == true && isRecorded == false)
					return;
			
				//check s'il enregistre
				if (isRecorded == true) 
				{
					//desactive enregistrement
					isRecorded = false;
					timeController.IsRecording = false;
					playerClone.enabled = false;
					return;
				}

				//check si la liste est rempli
				if (playerClonePositions.Count > 0) 
				{
					//MODE PLAY
					Debug.Log("PLAY ACTIVATION");
					IsPlaying = true; //démarre l'enregistrement
					transform.position = new Vector3(player.transform.position.x,player.transform.position.y - 2.0f, 0.0f); //le met sur la position du joueur
					playerClone.enabled = true; //active le clone//active le clone
				} 
				else 
				{
					//MODE RECORD
					Debug.Log("isRecorded ACTIVATION");
					transform.position = new Vector3(player.transform.position.x,player.transform.position.y - 2.0f, 0.0f); //le met sur la position du joueur
					playerClone.enabled = true; //active le clone//active le clone
					isRecorded = true; //active le mode record
					timeController.IsRecording = true;
				}
			}

		//check s'il doit record les actions
		if(isRecorded == true)
		{
			RecordClone (gameObject, playerClonePositions, isRecorded); //RECORDING
		}

		//Check S'il doit joué un enregistrement
		PlayClone(gameObject,playerClonePositions,IsPlaying);


		//VERIFICATION
		if (timeController.IsRecording == false)
			isRecorded = false;

		if (playerClonePositions.Count == 0 && IsPlaying == true)
			IsPlaying = false;

		if (IsPlaying == false && isRecorded == false) {
			playerClone.enabled = false; //désactive le sprite
			transform.position = InactivePosition; //met le gameObject loin du lieu d'action
		}
	}

	public void MoveClone(GameObject clone, ArrayList clonePositionList) //joue l'enregistrement 1
	{
		if (timeController.IsRecording == false && clonePositionList.Count > 0) 
		{
			frameTime++; //incrémente un compteur fake
			float interpolation = (float)frameTime / (float)frameRateRecord; //calcul de l'interpolation selon les frame pour déterminé le temps parcouru entre les "keyframe"
			clone.transform.position = Vector3.Lerp (previousPosition, currentPosition, interpolation); //mouvement entre les "keyframe"

			if (frameTime == frameRateRecord) //change les position quand le compteur est atteint 
			{
				//interpolation entre la current et next
				previousPosition = (Vector3)clonePositionList [0]; //lis la première entré de la liste

				if (clonePositionList.Count == 1) //s'il ne reste que 1 entré dans la liste
				{ 
					currentPosition = previousPosition; //met le current sur le previous
					Instantiate (playerCloneCorpse, clone.transform.position, clone.transform.localRotation); //créer un cadavre à la dernière position
				}
				else
					currentPosition = (Vector3)clonePositionList [1]; //lis la deuxième entré de la liste

				clonePositionList.RemoveAt (0); //supprime la première entré dela liste
				frameTime = 0;
			}
		}
	}

	public void PlayClone(GameObject clone, ArrayList clonePositionList, bool isPlayingBool)
	{
		//mouvement du clone 1
		if (isPlayingBool == true && clonePositionList.Count > 0)  //Si liste rempli et mode play actif
		{ 
			MoveClone (clone,clonePositionList); //bouge le clone selon la fonction MoveClone
		} 
		else if(isPlayingBool == true)
		{
			isPlayingBool = false; //desactive le mode play
		}
	}

	public void RecordClone(GameObject clone,ArrayList clonePositionList, bool IsRecordedBool) //fonction pour enregistrement des actions du clone1
	{
		if (clonePositionList.Count < MaxRecordedEntries && IsRecordedBool == true) //vérifie s'il ne dépasse pas la limite de temps et s'il enregistre
		{
			frameCount ++; //incrémente le compteur

			if (frameCount != frameRateRecord)
				return;

			clonePositionList.Add (clone.transform.position); //enregistre la position du clone à chaque frame
			frameCount = 0; //reset le compteur
			timeController.timeRecorded += 1; //incrémente le nombre de frame enregistré
			Debug.Log("Record");
		}
		else 
		{
			timeController.IsRecording = false; //arrête l'enregistrement de force
			IsRecordedBool = false; //desactive le isRecoded1
			Debug.Log("STOOOOOP");
			playerClone.enabled = false; //desactive le sprite
		}
	}
}