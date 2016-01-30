using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
	
	public int playerNumber;
	public float speed = 10;
	public GameObject anchor;
	public float maxRangeFromAnchor = 3;
	
	public float dashDuration = 0.3f;
	public float dashCooldown = 1;
	public float dashSpeed = 30;
	
	private Vector3 _dash = new Vector3();
	private float _dashTimer = 0f;
	private float _dashCooldown = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		string horizAxisString = "Horizontal"+(playerNumber==1?"":playerNumber.ToString());
		string vertAxisString = "Vertical"+(playerNumber==1?"":playerNumber.ToString());
		string dashString = "Dash" + playerNumber;
		
		Vector3 movement = new Vector3 (Input.GetAxis(horizAxisString), Input.GetAxis(vertAxisString), 0) * speed * Time.deltaTime;
		
		if (movement.magnitude > speed)
			movement = movement.normalized * speed * Time.deltaTime;
		
		
		/*if (_dashTimer > 0) {
			movement = _dash;
			_dashTimer -= Time.deltaTime;
			
		} else if (Input.GetButton (dashString) && _dashCooldown <= 0) {
			_dashTimer = dashDuration;
			_dashCooldown = dashCooldown;
			_dash = movement.normalized * dashSpeed * Time.deltaTime;
			
		} else {
			_dashCooldown -= Time.deltaTime;
		}//*/
		
		Vector3 distanceFromAnchor = transform.position - anchor.transform.position;
		if ((distanceFromAnchor + movement).magnitude > maxRangeFromAnchor) {
			movement -= (distanceFromAnchor - distanceFromAnchor.normalized * maxRangeFromAnchor);
		}
		
		transform.position += movement;
	}
}
