using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeController : MonoBehaviour {

	[Header ("Les GameObject du player")]
	public GameObject player; //le player
	public GameObject playerClone; //le double du personnage joueur
	public GameObject playerClone2; //le deuxième double
	public GameObject playerClone3; //le troisième double
	public GameObject playerCloneCorpse; //Le cadavre laissé par un double

	public bool IsRecording = false; //bool qui indique si ça enregistre
	public int timeRecorded = 0; //nombre de frame enregistrée

	public Image imageUI; //image d'effet d'enregistrement à l'écran

	public int MaxRecordedEntries = 360; //le maximum de frame à enregistrer


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		//recording
		switch (IsRecording) 
		{
		case false:
			imageUI.enabled = false; //désactive l'effet à l'écran
			timeRecorded = 0; //reset le nombre de frame enregister
			break;
		case true:
			imageUI.enabled = true; //active l'effet à l'écran
			break;
		}
	}
}
