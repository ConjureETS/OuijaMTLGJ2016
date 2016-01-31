using UnityEngine;
using System.Collections;
using InputHandler;
using System;

public class MenuFunctions : MonoBehaviour {
    public GameObject[] players;
    Animator[] anims = {null,null,null };
    public int levelIndex;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 3; i++)
        {
            InputManager.Instance.PushActiveContext("Menu", i);
            InputManager.Instance.AddCallback(i, HandleButtons);
        }
        
        for (int i = 0; i < players.Length; i++)
        {
            anims[i] = players[i].GetComponent<Animator>();
        }
        
        
	    
	}

    private void HandleButtons(MappedInput obj)
    {
        if (obj.Actions.Contains("Dash"))
        {       
                anims[obj.PlayerIndex].SetBool("Dashing", !anims[obj.PlayerIndex].GetBool("Dashing"));
        }
    }

    // Update is called once per frame
    void Update () {
        int compteur = 0;
        for (int i = 0; i < anims.Length; i++)
        {
            if (anims[i].GetBool("Dashing"))
            {
                ++compteur;
            }
            
        }
        if (compteur == 3)
        {
            Application.LoadLevel(levelIndex);
        }


    }
}
