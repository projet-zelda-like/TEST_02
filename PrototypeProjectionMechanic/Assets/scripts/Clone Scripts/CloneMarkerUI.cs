using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneMarkerUI : MonoBehaviour {

	public GameObject player;

	public GameObject playerClone;
	private SpriteRenderer spriteClone1;
	public RectTransform MarkerCloneTransform1;
	public Image markerCloneImage1;


	public GameObject playerClone2;
	private SpriteRenderer spriteClone2;
	public RectTransform MarkerCloneTransform2;
	public Image markerCloneImage2;

	public GameObject playerClone3;
	private SpriteRenderer spriteClone3;
	public RectTransform MarkerCloneTransform3;
	public Image markerCloneImage3;

	public float rotSpeed = 2.0f;

	private TimeController timeController;

	// Use this for initialization
	void Start () {
		timeController = FindObjectOfType (typeof(TimeController)) as TimeController;

		spriteClone1 = playerClone.GetComponent<SpriteRenderer> ();
		spriteClone2 = playerClone2.GetComponent<SpriteRenderer> ();
		spriteClone3 = playerClone3.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Clone 1
		float distance1 = Vector3.Distance(player.transform.position,playerClone.transform.position);

		if (spriteClone1.enabled == true && timeController.IsRecording == false && distance1 > 5.0f ) 
		{
			markerCloneImage1.enabled = true;

			Vector3 direction1 = playerClone.transform.position - MarkerCloneTransform1.position;
			float angle1 = Mathf.Atan2 (direction1.y, direction1.x) * Mathf.Rad2Deg;
			Quaternion rotation1 = Quaternion.AngleAxis (angle1, Vector3.forward);
			MarkerCloneTransform1.rotation = Quaternion.Slerp (MarkerCloneTransform1.rotation, rotation1, rotSpeed * Time.deltaTime);
		} 
		else 
		{
			markerCloneImage1.enabled = false;
		}


		// Clone 2
		float distance2 = Vector3.Distance(player.transform.position,playerClone2.transform.position);

		if (spriteClone2.enabled == true && timeController.IsRecording == false && distance2 > 5.0f ) 
		{
			markerCloneImage2.enabled = true;

			Vector3 direction2 = playerClone2.transform.position - MarkerCloneTransform2.position;
			float angle2 = Mathf.Atan2 (direction2.y, direction2.x) * Mathf.Rad2Deg;
			Quaternion rotation2 = Quaternion.AngleAxis (angle2, Vector3.forward);
			MarkerCloneTransform2.rotation = Quaternion.Slerp (MarkerCloneTransform2.rotation, rotation2, rotSpeed * Time.deltaTime);
		} 
		else 
		{
			markerCloneImage2.enabled = false;
		}

		// Clone 3
		float distance3 = Vector3.Distance(player.transform.position,playerClone3.transform.position);

		if (spriteClone3.enabled == true && timeController.IsRecording == false && distance3 > 5.0f ) 
		{
			markerCloneImage3.enabled = true;

			Vector3 direction3 = playerClone3.transform.position - MarkerCloneTransform3.position;
			float angle3 = Mathf.Atan2 (direction3.y, direction3.x) * Mathf.Rad2Deg;
			Quaternion rotation3 = Quaternion.AngleAxis (angle3, Vector3.forward);
			MarkerCloneTransform3.rotation = Quaternion.Slerp (MarkerCloneTransform3.rotation, rotation3, rotSpeed * Time.deltaTime);
		} 
		else 
		{
			markerCloneImage3.enabled = false;
		}

	}
}
