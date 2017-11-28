using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

	[Header("Player and clones")]
	public GameObject player;
	public GameObject playerClone;
	public GameObject playerClone2;
	public GameObject playerClone3;

	[Header("Camera's target and speed")]
	public Transform target; //la cible principale au centre de l'écran
	public ArrayList targets; //liste des transform cibles de la caméra
	public float smoothSpeed = 0.5f;
	public float camBorder = 5.0f;
	public Vector3 offset;

	private TimeController timeController;

	Camera cam; //la caméra
	Rect viewArea; //la zone à afficher définie par un rectangle

	[Header("Zoom Values")]

	public float dezoomCoeficient;
	public float zoomSpeed = 0.1f;
	public float zoomMax = 10.0f;
	public float defautZoom = 5.0f;
	public GameObject camMarker; //le target à cibler entre le player et les clones

	void Awake()
	{
		timeController = FindObjectOfType(typeof(TimeController)) as TimeController;
		cam = GetComponent<Camera> (); //récupère le component caméra
	}

	void Start()
	{
		targets = new ArrayList (); //initialise la liste
		targets.Add (player.transform); //ajoute le player dans la liste
	}

	void Update()
	{
		if (timeController == null)
			timeController = FindObjectOfType(typeof(TimeController)) as TimeController;

		//update les target à suivre
		if (timeController.IsRecording == true) 
		{
			if (playerClone.GetComponent<SpriteRenderer>().enabled == true)
			{
				target = camMarker.transform;

				if (targets.Contains(playerClone.transform) == false)
					targets.Add (playerClone.transform);//ajoute le clone à la liste des targets
			}
			if (playerClone2.GetComponent<SpriteRenderer>().enabled) 
			{
				target = camMarker.transform;

				if (targets.Contains (playerClone2.transform) == false)
					targets.Add (playerClone2.transform);//ajoute le clone à la liste des targets
			}
			if (playerClone3.GetComponent<SpriteRenderer>().enabled == true) 
			{
				target = camMarker.transform;

				if (targets.Contains (playerClone3.transform) == false)
					targets.Add (playerClone3.transform);//ajoute le clone à la liste des targets
			}
		} 
		else 
		{
			target = player.transform; //la main target est le joueur

			//enlève les cible inutile de la liste
			if (targets.Contains(playerClone.transform) == true)
				targets.Remove (playerClone.transform);

			if (targets.Contains (playerClone2.transform) == true)
				targets.Remove (playerClone2.transform);

			if (targets.Contains (playerClone3.transform) == true)
				targets.Remove (playerClone3.transform);
		}

		/*
		//update le zoom
		float distanceClone1 = Vector3.Distance(player.transform.position,playerClone.transform.position);
		float distanceClone2 = Vector3.Distance(player.transform.position,playerClone2.transform.position);
		float distanceClone3 = Vector3.Distance(player.transform.position,playerClone3.transform.position);

		if (playerClone.activeInHierarchy == true && distanceClone1 > 8.0f && timeController.IsRecording == true) //si la distance et le clone sont présent
		{
			if(cam.orthographicSize < zoomMax) //limit de dezoom
				cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, (distanceClone1 / 100) * defautZoom * dezoomCoeficient, zoomSpeed * Time.deltaTime); 
		} 
		else if (playerClone2.activeInHierarchy == true && distanceClone2 > 8.0f && timeController.IsRecording == true) //si la distance et le clone sont présent
		{
			if(cam.orthographicSize < zoomMax) //limit de dezoom
				cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, distanceClone2 * dezoomCoeficient, zoomSpeed * Time.deltaTime); 
		} 
		else if (playerClone3.activeInHierarchy == true && distanceClone3 > 8.0f && timeController.IsRecording == true) //si la distance et le clone sont présent
		{
			if(cam.orthographicSize < zoomMax) //limit de dezoom
				cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, distanceClone3 * dezoomCoeficient, zoomSpeed * Time.deltaTime); 
		}
		else 
		{
			//revient au zoom par défaut
			cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, defautZoom, zoomSpeed * Time.deltaTime);
		}
		*/
	}

	void LateUpdate () 
	{
		//UPDATE LA VIEW AREA
		viewArea = CalculateTargetsBoundingBox();
		cam.orthographicSize = CalculateOrthographicSize (viewArea);
		Vector3 desiredPosition = target.position + offset; //la position a atteindre
		Vector3 smoothPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime); //smooth
		transform.position = new Vector3(smoothPosition.x, smoothPosition.y , smoothPosition.z); //positionnement
	}


	/// <summary>
	/// Calculates a bounding box that contains all the targets.
	/// </summary>
	/// <returns>A Rect containing all the targets.</returns>
	Rect CalculateTargetsBoundingBox()
	{
		float minX = Mathf.Infinity;
		float maxX = Mathf.NegativeInfinity;
		float minY = Mathf.Infinity;
		float maxY = Mathf.NegativeInfinity;

		foreach (Transform target in targets) {
			Vector3 position = target.position;

			minX = Mathf.Min(minX, position.x);
			minY = Mathf.Min(minY, position.y);
			maxX = Mathf.Max(maxX, position.x);
			maxY = Mathf.Max(maxY, position.y);
		}
			
		return Rect.MinMaxRect(minX - camBorder, maxY + camBorder, maxX + camBorder, minY - camBorder);
	}

	/// <summary>
	/// Calculates a new orthographic size for the camera based on the target bounding box.
	/// </summary>
	/// <param name="boundingBox">A Rect bounding box containg all targets.</param>
	/// <returns>A float for the orthographic size.</returns>
	float CalculateOrthographicSize(Rect viewArea)
	{
		float orthographicSize = cam.orthographicSize;
		Vector3 topRight = new Vector3(viewArea.x + viewArea.width, viewArea.y, 0f);
		Vector3 topRightAsViewport = cam.WorldToViewportPoint(topRight);

		if (topRightAsViewport.x >= topRightAsViewport.y)
			orthographicSize = Mathf.Abs(viewArea.width) / cam.aspect / 2f;
		else
			orthographicSize = Mathf.Abs(viewArea.height) / 2f;

		return Mathf.Clamp(Mathf.Lerp(cam.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), defautZoom, Mathf.Infinity);
	}
}
