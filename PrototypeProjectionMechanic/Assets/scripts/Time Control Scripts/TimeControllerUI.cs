using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeControllerUI : MonoBehaviour {

	public Image timeBar;
	public Image maskBar;
	public Image backgroundBar;

	public float fillAmount; //float de remplissage
	private TimeController timeController;

	// Use this for initialization
	void Start () {
		timeController = FindObjectOfType (typeof(TimeController)) as TimeController;
	}
	
	// Update is called once per frame
	void Update () {

		//prend le max du time controller
		float MaxTimeRecorded = timeController.MaxRecordedEntries;
		//prend le timerecorded
		float currentTimeRecorded = timeController.timeRecorded;

		//detect si le tme controller enregistre
		if (timeController.IsRecording == true) 
		{
			//affiche les éléemnts d'UI et applique les changement dessus
			timeBar.enabled = true;
			maskBar.enabled = true;
			backgroundBar.enabled = true;

			//conversion des valeur
			float fillToShow = currentTimeRecorded/MaxTimeRecorded;
			fillAmount = fillToShow;

			timeBar.fillAmount = fillAmount; //met le fillamoutn du code sur celui de l'image

		} 
		else 
		{
			//désaffiche les éléemnts d'UI et applique les changement dessus

			timeBar.enabled = false;
			maskBar.enabled = false;
			backgroundBar.enabled = false;
		}
	}
}
