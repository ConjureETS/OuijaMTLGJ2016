using UnityEngine;
using System.Collections;

public class MenuInteractions : MonoBehaviour
{
    public int nextLevel;
    public bool isSkippable = true;
    public bool hasAutoSkips = false;
    public int timeBeforeSkip = 10;

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
        // this won't lead anywhere... let's fix it
        if(!isSkippable && !hasAutoSkips)
        {
            isSkippable = true;
            hasAutoSkips = true;
        }

        if (hasAutoSkips)
        {
            StartCoroutine(AutoSkip());
        }
    }

    IEnumerator AutoSkip()
    {        
        yield return new WaitForSeconds(timeBeforeSkip);
        Application.LoadLevel(nextLevel);

    }
}
