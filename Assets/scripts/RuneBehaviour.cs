using UnityEngine;
using System.Collections;

public class RuneBehaviour : MonoBehaviour {

	public static float range;
	public SpriteRenderer symbol;
	public int letterNum;
	private float lightState = 0f;
	public const float LightTime = 1.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lightState > 0f)
		{
			lightState -= Time.deltaTime / LightTime;
			if (lightState < 0)
				lightState = 0;
			symbol.color = new Color(1 - lightState, 1f, 1 - lightState);

			//Mauve cool
			//symbol.color = new Color(1f - lightState * 0.5f, 1f - lightState * 0.8f, 1f);


			//symbol.color = new Color(1f - lightState * 0.4f, 1f - lightState * 0.4f, 1f);
		}

	}

	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Player")
		{
			lightState = 1f;
			GameState.Instance.currentLevel.PressTile(letterNum);
		}
	}

	public void SetSymbol(int letterNum)
	{
		this.letterNum = letterNum;
		symbol.sprite = Resources.Load<Sprite>("runic_" + (char)('a' + letterNum));
	}
}
