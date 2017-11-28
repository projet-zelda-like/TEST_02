using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneTrailRenderer : MonoBehaviour {

	public LineRenderer line;
	public GameObject clone;
	private int nbrPosition;
	// Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		int nbrPositionClone = clone.GetComponent<TimeControllerClone> ().playerClonePositions.Count;

		if (nbrPosition != nbrPositionClone) 
		{
			//récupère le nombre de position
			nbrPosition = nbrPositionClone;
			//définie le nombre de point
			line.positionCount = nbrPosition;
			//boucle qui définie les position
			for (int i = 0; i < nbrPosition; i++) 
			{
				line.SetPosition (i, (Vector3)clone.GetComponent<TimeControllerClone> ().playerClonePositions [i]);
			}
		}

	}
}
