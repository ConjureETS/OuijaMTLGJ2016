using UnityEngine;
using System.Collections;

public class RuneSoundHandler : MonoBehaviour 
{   
	public static void MakeSound()
	{
		GameObject runeSound = (GameObject)Instantiate(Resources.Load("RuneSound"));
	}
}
