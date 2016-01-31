using UnityEngine;
using System.Collections;

public class SoundGen : MonoBehaviour 
{
	float[] ratios = new float[14];
	AudioSource audio;
	float sec;
	float[] scale = new float[5];
	float[] noteTimes = new float[6];
	float currNoTime;
	
	float BASE_NOTE;
	int modif;
	public int SilentInB;
	public int minBeat;
	public int maxBeat;
	public int noteChanger;
	public float slowerEffect;
	void Awake()
	{
		modif = 0;
		ratios[0]  = 1f; 		 ratios[1]  = 55f/51.91f; ratios[2]  = 55f/48.99f; ratios[3]  = 55f/46.25f; ratios[4]  = 55f/43.65f; ratios[5]  = 55f/41.20f;
		ratios[6]  = 55f/38.89f; ratios[7]  = 55f/36.71f; ratios[8]  = 55f/34.65f; ratios[9]  = 55f/32.70f; ratios[10] = 55f/30.87f; ratios[11] = 55f/29.14f;
		ratios[12] = 2f;		 ratios[13] = 2f * ratios[1];
		
		BASE_NOTE = 1f * ratios[noteChanger] / slowerEffect;
		
		scale[0] = ratios[0];
		scale[1] = ratios[3];
		scale[2] = ratios[5];
		scale[3] = ratios[7];
		scale[4] = ratios[10];
		//scale[5] = ratios[9];
		//scale[6] = ratios[11];
		//scale[7] = ratios[11];
		//scale[8] = ratios[13];
		
		noteTimes[0] = 4f; 
		noteTimes[1] = noteTimes[0] / 2; 
		noteTimes[2] = noteTimes[0] / 4; 
		noteTimes[3] = noteTimes[0] / 8; 
		noteTimes[4] = noteTimes[0] / 16; 
		
		currNoTime = noteTimes[Random.Range(minBeat, maxBeat)];
		audio = GetComponent<AudioSource>();
		audio.pitch = BASE_NOTE; //G#
		sec = 0f;

	}
	
	void Update()
	{
		
		sec += Time.deltaTime;
		
		if(sec > currNoTime * (7f / 8f))
		{	
			audio.volume -= Time.deltaTime * (currNoTime * (1f / 8f) * (8f / currNoTime));
		}
		if(sec > currNoTime)
		{
			
			//silent = (Random.Range(0,2) == 0) ? true : false;
			audio.volume = 0f;
			audio.Stop();
			//if(!silent)
			//{
			sec = 0f;
			if(Random.Range(0, SilentInB) == 0)
				audio.volume = 1f;
			currNoTime = noteTimes[Random.Range(minBeat, maxBeat)];
			
		//	if(Random.Range(0,5) == 0){}
		//	else
	//		{
				modif += Random.Range(1, scale.Length);
			audio.pitch =  BASE_NOTE * scale[modif % scale.Length];
	//		}
			
			//audio.time = currNoTime;
			audio.Play();
			//restart = true;
		}
		
		//Debug.Log(sec);
		//Debug.Log(Time.deltaTime + " reg");
		//audio.pitch += (Time.deltaTime * 1/100);
		
	}

}
