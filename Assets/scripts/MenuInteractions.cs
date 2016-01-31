using UnityEngine;
using System.Collections;

public class MenuInteractions : MonoBehaviour
{
    public int nextLevel;
    public bool hasAutoSkips = false;
    public int timeBeforeAutoSkip = 10;
    public int timeBeforeSkippable = 0;
    private bool isSkippable = false;

    // Update is called once per frame
    void Update()
    {
        if (isSkippable)
        {
            if (Input.anyKeyDown)
            {
                Application.LoadLevel(nextLevel);
            }
        }
        
    }

    void Start()
    {   
        
        StartCoroutine(EnableSkip());

        if (hasAutoSkips)
        {
            StartCoroutine(AutoSkip());
        }
    }

    IEnumerator AutoSkip()
    {        
        yield return new WaitForSeconds(timeBeforeAutoSkip);
        Application.LoadLevel(nextLevel);

    }

    IEnumerator EnableSkip()
    {
        yield return new WaitForSeconds(timeBeforeSkippable);
        isSkippable = true;

    }
}
