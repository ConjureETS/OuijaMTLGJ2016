using UnityEngine;
using System.Collections;

public class PlayerControllerSR : MonoBehaviour {

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.A))
			rb.AddForce(-transform.right * 5);
		if (Input.GetKey(KeyCode.D))
			rb.AddForce(transform.right * 5);
		if (Input.GetKey(KeyCode.S))
			rb.AddForce(transform.forward * 5);
		if (Input.GetKey(KeyCode.W))
			rb.AddForce(-transform.forward * 5);
	}

	void OnTriggerEnter(Collider col)
	{
		//Remove this: Debug.Log(col.gameObject.GetComponent<RuneBehaviour>().letterNum);
	}
}
