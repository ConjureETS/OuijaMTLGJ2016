using UnityEngine;

public class Player {

	public int num;
	public int[] letters;
	public int index = 0;
	public int score = 0;
	public Character character = null;

	public Player(int num)
	{
		this.num = num;
	}

	public void SetWord(string str)
	{
		letters = new int[str.Length];
		for (int i = 0; i < str.Length; i++)
		{
			letters[i] = (int)(str[i] - 'A');
		}
        /*Debug.Log("Player " + num + " has:");
		foreach (int i in letters)
		{
			Debug.Log(i);
		}*/

        index = 0;
	}

	public void SetWord(int[] letters)
	{
		this.letters = letters;
	}

	public bool hasNextLetter(int letterNum)
	{
        if (index < 0 || index >= letters.Length) return false;

		if (letters[index] == letterNum)
		{
			index++;
			Debug.Log("Player " + num + " uncovered their " + index + "th letter");
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool hasWon()
	{
		return index >= letters.Length;
	}
}
