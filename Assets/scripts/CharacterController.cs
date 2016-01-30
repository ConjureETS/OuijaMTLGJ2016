using UnityEngine;
using System.Collections;
using InputHandler;

public class CharacterController : MonoBehaviour
{
    public enum PlayerNumber { One, Two, Three }

    public Character character;
    public PlayerNumber playerNumber;

	// Use this for initialization
	void Start ()
    {
        InputManager.Instance.PushActiveContext("Normal", (int)playerNumber);
        InputManager.Instance.AddCallback((int)playerNumber, HandlePlayerButtons);
        InputManager.Instance.AddCallback((int)playerNumber, HandlePlayerAxis);
	}

    private void HandlePlayerAxis(MappedInput input)
    {
        if (character == null) return;

        float xValue = 0f;

        if (input.Ranges.ContainsKey("MoveLeft"))
        {
            xValue = -input.Ranges["MoveLeft"];
        }
        else if (input.Ranges.ContainsKey("MoveRight"))
        {
            xValue = input.Ranges["MoveRight"];
        }

        float zValue = 0f;

        if (input.Ranges.ContainsKey("MoveForward"))
        {
            zValue = input.Ranges["MoveForward"];
        }
        else if (input.Ranges.ContainsKey("MoveBackward"))
        {
            zValue = -input.Ranges["MoveBackward"];
        }

        character.Move(xValue, zValue);
    }

    private void HandlePlayerButtons(MappedInput input)
    {
        if (character == null) return;


    }
}
