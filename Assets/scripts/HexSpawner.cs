using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class HexSpawner : MonoBehaviour {

	public int numRows = 4;
	public int numColumns = 3;
	public float xScale = 1f;
	public float yScale = 0.8f;
	public float dimension = 0.6f;
	public GameObject hexagon;
	private float ratio = Mathf.Sqrt(1 - 0.5f * 0.5f);

	private List<RuneBehaviour> runes = new List<RuneBehaviour>();

	// Use this for initialization
	void Start () 
	{
		int[] runeNums = new int[numColumns * numRows];
		for (int i = 0; i < numRows * numColumns; i++)
		{
			runeNums[i] = i % 26;
		}

		System.Random rnd = new System.Random();
		var randomNums = runeNums.OrderBy(r => rnd.Next())
								.ToArray();

		GameObject hex;
		RuneBehaviour rune;
		int index = 0;

		float xOffset = numColumns * 1.5f * dimension / 2f;
		float yOffset = numRows * 1f * ratio * dimension / 2f;

		for (int row = 0; row < numRows; row++)
		{
			for (int col = 0; col < numColumns; col++)
			{
				hex = GameObject.Instantiate(hexagon) as GameObject;
				hex.transform.parent = transform;
				hex.transform.localScale = new Vector3(xScale, yScale, 1f);

				hex.transform.localPosition = new Vector3(
						(3f * dimension * col + 1.5f * dimension * (row % 2)) * xScale - xOffset,
						(row * ratio * dimension) * yScale - yOffset, 0f);

				hex.transform.localRotation = Quaternion.identity;

				rune = hex.GetComponent<RuneBehaviour>();
				rune.SetSymbol(randomNums[index++]);
				runes.Add(rune);
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
	
	}
}
