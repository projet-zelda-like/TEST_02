using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextualAction : MonoBehaviour {

	public KeyCode inputAction;


	void OnTriggerStay2D(Collider2D other)
	{

		//check le tag des collider en contact
		if (other.tag == "Levier") //trouve un levier
			{
			Debug.Log ("Touched");
			//en cas d'input
			if (Input.GetKeyDown(inputAction))
				{	
				bool doorState = other.GetComponent<LevierSystem> ().isActivated; //récupère le bool d'activation du levier

				switch (doorState) 
					{
						case true:
							other.GetComponent<LevierSystem> ().isActivated = false; //désactive le levier
							break;
						case false: 
							other.GetComponent<LevierSystem> ().isActivated = true; //active le levier
							break;
					}
				}
			}



	}
}
