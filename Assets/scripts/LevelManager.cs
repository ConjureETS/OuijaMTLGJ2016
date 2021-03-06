﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using InputHandler;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float xScale = 1f;
	public float yScale = 0.8f;
	public float colSpacing = 0f;
	public float rowSpacing = 0f;
	public Camera camera;
	//public Transform darkness;

	public float dimension = 0.6f;
	public GameObject hexagon;
    public Color[] PlayerColors;
	private float ratio = Mathf.Sqrt(1 - 0.5f * 0.5f);
	private GameState state;
	private List<RuneBehaviour> runes = new List<RuneBehaviour>();


    private SelectorWithBolts Selector;
    private GameObject PhysicsContainer;
    private int playerId;

    public Image Icon1;
    public Image Icon2;
    public Image Icon3;
    public Image IconGo;

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(FadeOutNumber(Icon3));
        yield return StartCoroutine(FadeOutNumber(Icon2));
        yield return StartCoroutine(FadeOutNumber(Icon1));

        for (int i = 0; i < 3; i++)
        {
            InputManager.Instance.PushActiveContext("Normal", i);
        }

        EnableKinematics(false);

        //play start sound
        SoundManager.Instance.PlayShortHorn();
    }

    private IEnumerator FadeOutNumber(Image number)
    {
        number.gameObject.SetActive(true);

        number.rectTransform.offsetMax = Vector2.zero;
        number.rectTransform.offsetMin = Vector2.zero;

        Vector2 initialAnchorMin = new Vector2(0.4f, 0.3f);
        Vector2 initialAnchorMax = new Vector2(0.6f, 0.7f);

        Vector2 finalAnchor = new Vector2(0.5f, 0.5f);

        float ratio = 0f;

        Color initialColor = number.color;
        Color finalColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / 1f;

            number.rectTransform.anchorMin = Vector2.Lerp(initialAnchorMin, finalAnchor, ratio);
            number.rectTransform.anchorMax = Vector2.Lerp(initialAnchorMax, finalAnchor, ratio);

            number.color = Color.Lerp(initialColor, finalColor, ratio);

            yield return null;
        }

        number.gameObject.SetActive(false);
    }





	// Use this for initialization
	void Start()
    {
        state = GameState.Instance;
		state.currentLevel = this;
		int numColumns = state.numColumns;
		int numRows = state.numRows;

		int[] runeNums = new int[numColumns * numRows];
		for (int i = 0; i < numRows * numColumns; i++)
		{
			runeNums[i] = i % 26;
		}

		System.Random rnd = new System.Random();
		var randomNums = runeNums.OrderBy(r => rnd.Next())
								.ToArray();

		GameObject hex;
		RuneBehaviour rune;
		int index = 0;

		float xOffset = numColumns * 1.5f * dimension / 2f;
		float yOffset = numRows * 1f * ratio * dimension / 2f;

		for (int row = 0; row < numRows; row++)
		{
			for (int col = 0; col < numColumns; col++)
			{
				hex = GameObject.Instantiate(hexagon) as GameObject;
				hex.transform.parent = transform;
				hex.transform.localScale = new Vector3(xScale, yScale, 1f);

				hex.transform.localPosition = new Vector3(
						((3f+colSpacing) * dimension * col + 1.5f * dimension * (row % 2)) * xScale - xOffset,
						(row * (ratio * dimension + rowSpacing)) * yScale - yOffset, 0f);

				hex.transform.localRotation = Quaternion.identity;

				rune = hex.GetComponent<RuneBehaviour>();
				rune.SetSymbol(randomNums[index++]);
				runes.Add(rune);
			}
		}

		int numLetters = state.wordLength;
		foreach (Player player in state.players)
		{
			player.SetWord(WordGen.GetWord(numLetters));
		}

        Selector = GameObject.FindObjectOfType<SelectorWithBolts>();
        PhysicsContainer = GameObject.Find("PhysicsContainer");


        EnableKinematics(true);
        for (int i = 0; i < 3; i++)
        {
            InputManager.Instance.PushActiveContext("CinematicEvent", i);
        }

        Icon1.gameObject.SetActive(false);
        Icon2.gameObject.SetActive(false);
        Icon3.gameObject.SetActive(false);
        IconGo.gameObject.SetActive(false);

        StartCoroutine(CountDown());
	}

    
	void Update()
	{
        /* hacking skillz
        if (Input.GetKeyDown(KeyCode.Space))
		{
			StartCoroutine(SendWinnerToTheSky(state.players[1]));
			StartCoroutine(FadeToDark());
		}
        */
	}

	public void PressTile(int letterNum, RuneBehaviour tile)
	{
		foreach (Player player in state.players)
		{
			if (player.hasNextLetter(letterNum))
			{
                playerId = player.num - 1;

                tile.symbol.color = PlayerColors[playerId];
                StartCoroutine(MoveSelectorToTile(tile));
                
                SoundManager.Instance.PlayRunePickup();
			}
		}
	}

    private IEnumerator MoveSelectorToTile(RuneBehaviour tile)
    {
        for (int i = 0; i < 3; i++)
		{
            InputManager.Instance.PushActiveContext("CinematicEvent", i);
		}

        // We don't want the color to lerp back to white while we are moving to the rune
        tile.GetComponent<RuneBehaviour>().enabled = false;

        tile.GetComponent<CapsuleCollider>().enabled = false;
        tile.GetComponent<SpriteRenderer>().sortingOrder = 100;
        tile.symbol.sortingOrder = 101;

        EnableKinematics(true);

        Transform trans = PhysicsContainer.GetComponent<Transform>();

        Vector3 startPos = trans.position;
        Vector3 endPos = startPos + tile.GetComponent<Transform>().position - Selector.GetComponent<Transform>().position; ;
        endPos.y = startPos.y;

        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / 0.25f;

            trans.position = Vector3.Lerp(startPos, endPos, ratio);

            yield return null;
        }

        StartCoroutine(LiftTileInTheAir(tile));
    }

    private IEnumerator LiftTileInTheAir(RuneBehaviour tile)
    {
        float ratio = 0f;

        Transform trans = tile.GetComponent<Transform>();
        Transform cameraTrans = Camera.main.GetComponent<Transform>();

        Vector3 startPos = trans.position;
        

        Quaternion startRot = trans.rotation;
        

        while (ratio < 1f)
        {
            // The camera might move, so we recalculate the end position every frame
            Vector3 endPos = cameraTrans.position + cameraTrans.forward * 10f;//trans.position + new Vector3(0f, 5f, 0f);
            Quaternion endRot = Quaternion.LookRotation(cameraTrans.forward);

            ratio += Time.deltaTime / 1.5f;

            trans.position = Vector3.Lerp(startPos, endPos, ratio);
            trans.rotation = Quaternion.Slerp(startRot, endRot, ratio);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(SendTileToUI(tile));
    }

    private IEnumerator SendTileToUI(RuneBehaviour tile)
    {
        Transform trans = tile.GetComponent<Transform>();

        Vector3 startPos = trans.position;

        Vector3 startScale = trans.localScale;
        Vector3 endScale = Vector3.one * 0.03f;

        float ratio = 0f;
        while (ratio < 1f)
        {
            ratio += Time.deltaTime / 1f;

            float viewportX = 0.2f + playerId * 0.3f;

            Vector3 endPos = Camera.main.ViewportToWorldPoint(new Vector3(viewportX, 0.8f, Camera.main.nearClipPlane + 0.1f));

            trans.position = Vector3.Lerp(startPos, endPos, ratio);
            trans.localScale = Vector3.Lerp(startScale, endScale, ratio);

            yield return null;
        }

		bool gameWasWon = false;

		foreach (Player player in state.players)
		{
			if (player.hasWon())
			{
				gameWasWon = true;
				StartCoroutine(SendWinnerToTheSky(player));
			}
		}

		if (gameWasWon)
		{
			StartCoroutine(FadeToDark());
		}
		else
		{
			// At the end of it all, we re-enabled the controls
			for (int i = 0; i < 3; i++)
			{
				InputManager.Instance.PushActiveContext("Normal", i);
			}

			EnableKinematics(false);
			Destroy(tile.gameObject);
		}
    }

	private IEnumerator SendWinnerToTheSky(Player player)
	{

        SoundManager.Instance.PlayWinning();

		GameObject soul = GameObject.Instantiate(player.character.gameObject, player.character.transform.position, player.character.transform.rotation) as GameObject;

		Color c;
		Material m = Resources.Load<Material>("soul");
		foreach (Renderer r in soul.GetComponentsInChildren<Renderer>())
		{
			r.material = m;
		}

		Vector3 startPos = soul.transform.position;
		Vector3 endPos = startPos + Vector3.up * 20;

		float ratio = 0f;
		while (ratio < 1f)
		{
			ratio += Time.deltaTime / 4f;

			soul.transform.position = Vector3.Lerp(startPos, endPos, ratio);

			yield return null;
		}
	}

	private IEnumerator FadeToDark()
	{
		//GameObject darkness = Instantiate(Resources.Load<GameObject>("Darkness"), new Vector3(0,15,0), Quaternion.Euler(90f,0f,0f)) as GameObject;
		//SpriteRenderer s = darkness.GetComponent<SpriteRenderer>();

		Vector3 startPos = camera.transform.position;
		Vector3 endPos = startPos + Vector3.up * 20;
		//Vector3 darkOffset = darkness.position - camera.transform.position;
		camera.GetComponent<MainCameraBehavior>().enabled = false;

		ratio = 0f;

		while (ratio < 1f)
		{
			ratio += Time.deltaTime / 4f;

			camera.transform.position = Vector3.Lerp(startPos, endPos, ratio);
			//darkness.transform.position = Vector3.Lerp(darkOffset + startPos, darkOffset + endPos, ratio);

			/*if (ratio > 0.5f)
			{
				s.color = new Color(0f, 0f, 0f, (ratio-0.5f)*2);
			}*/


			yield return null;
		}
		Application.LoadLevel(1); //TODO Check this
	}

    private void EnableKinematics(bool state)
    {
        Rigidbody[] rbs = PhysicsContainer.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = state;
        }
    }
}
