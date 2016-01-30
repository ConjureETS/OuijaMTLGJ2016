using UnityEngine;
using System.Collections;

public class SelectorBehaviour : MonoBehaviour {

	public GameObject[] follows;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 averagePosition = new Vector3 (0, 0, 0);
		for (int i = 0, j = follows.Length; i<j; i++) {
			averagePosition += follows[i].transform.position / j;
		}
		transform.position = averagePosition;
	}
}
