using UnityEngine;
using System.Collections;

public class WordGen : MonoBehaviour 
{
	static int prev;
	static int modif;
	static int tempCode;
	static int[] cCodes;
	static int[] codeChance;
	static string finalWord;
	static bool allowLetter;
	
	public static string GetWord(int numLetters)
	{
		finalWord = "";
		prev = -1;
		modif = 0;
		cCodes = new int[numLetters];
		codeChance = new int[26];
		
		for(int i = 0; i < numLetters; i++)
		{
			allowLetter = false;
			foreach (int c in codeChance)
			{
				codeChance[c] = 1;
			}
			modif = Random.Range(0, 26) % 26;
			if(i > 0)
				for(int a = i - 1; a >= 0; a--)
					for(int x = 0; x < 26; x++)
						if(cCodes[a] - 65 == x)
							codeChance[x] *= 2;
							
			while(!allowLetter)
			{
				if(Random.Range(0, codeChance[modif]) == 0 && modif != prev)
					allowLetter = true;
				else
					modif = Random.Range(0, 26);
			}
			
			prev = modif;
			cCodes[i] = modif + 65;
			finalWord += (char)cCodes[i];
		}
		
		
		return finalWord;
	}
	
}
