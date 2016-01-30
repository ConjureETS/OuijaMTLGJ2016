using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public int playerNumber;
	public float speed = 10;
	public GameObject anchor;
	public float maxRangeFromAnchor = 3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string horizAxisString = "Horizontal"+(playerNumber==1?"":playerNumber.ToString());
		string vertAxisString = "Vertical"+(playerNumber==1?"":playerNumber.ToString());

		Vector3 movement = new Vector3 (Input.GetAxis(horizAxisString), Input.GetAxis(vertAxisString), 0) * speed;

		if (movement.magnitude > speed)
			movement = movement.normalized * speed;

		//if((transform.position + movement - anchor.transform.position).magnitude >= maxRangeFromAnchor)
		//	movement -= anchor.transform.position - transform.position

		transform.position += movement * Time.deltaTime;
	}
}
