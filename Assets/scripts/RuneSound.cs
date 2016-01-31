using UnityEngine;
using System.Collections;

public class RuneSound : MonoBehaviour 
{
	float timeTillDeath = 4f;
	void Update () 
	{
		timeTillDeath -= Time.deltaTime;
		if(timeTillDeath < 0f)
			Destroy(gameObject);
	}
}
