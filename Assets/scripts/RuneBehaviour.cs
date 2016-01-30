using UnityEngine;
using System.Collections;

public class RuneBehaviour : MonoBehaviour {

	public static float range;
	public SpriteRenderer symbol;
	public int letterNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetSymbol(int letterNum)
	{
		this.letterNum = letterNum;
		Debug.Log("runic_" + ('a' + letterNum));
		symbol.sprite = Resources.Load<Sprite>("runic_" + (char)('a' + letterNum));
	}
}
