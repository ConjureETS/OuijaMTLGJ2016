using UnityEngine;
using System.Collections;

public class GUIGameplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnGUI()
    {
        
        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        for(int i = 0; i<GameState.Instance.players.Length;++i)
        {
            Color temp = GUI.color;
            string playerName = "";
            switch (i)
            {
                case 0:
                    GUI.color = new Color(1, 1, 0, 0.5f);
                    playerName = "Yellow beard";
                    break;
                case 1:
                    GUI.color = new Color(1,0 , 0, 0.5f);
                    playerName = "Red beard";
                    break;
                case 2:
                    GUI.color = new Color(0, 0, 1, 0.5f);
                    playerName = "Black beard";
                    break;
                default:
                    break;
            }
            
            GUI.Box(new Rect(10+ Screen.width /3 * i , 10, 200, 90), playerName);
            GUI.color = temp;
            Player p = GameState.Instance.players[i];
            
            for (int j = 0;j< p.letters.Length;++j)
            {
                char character = (char) p.letters[j];
                //Debug.Log(character);
                Texture2D rune = Resources.Load<Texture2D>("runic_" + (char)('a' + character));
               // Debug.Log(p.score);
                if (j+1 > p.index)
                {
                    
                    temp = GUI.color ;
                    GUI.color = new Color(1f, 1f, 1f, 0.50f);
                    GUI.DrawTexture(new Rect(20 + Screen.width / 3 * i + j * 30 + 20, 40, 30, 30), rune);
                    GUI.color = temp;

                }
                else
                {
                    GUI.DrawTexture(new Rect(20 + Screen.width / 3 * i + j * 30 + 20, 40, 30, 30), rune);
                }

                
            }
        }
        
    }
}
