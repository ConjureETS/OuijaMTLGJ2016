using UnityEngine;
using System.Collections;

public class DragSound : MonoBehaviour 
{
	public static AudioSource drag;
	
	void Awake()
	{
		drag = GetComponent<AudioSource>();
	}

    public void Start()
    {
        PlayDrag();
    }

	public static void PlayDrag()
	{
		drag.Play();
	}
	public static void StopDrag()
	{
		drag.Stop();
	}
}
