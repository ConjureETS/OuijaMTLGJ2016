using UnityEngine;
using System.Collections;

public class RuneBehaviour : MonoBehaviour {

	public static float range;
	public SpriteRenderer symbol;
	public int letterNum;
	private float elapsedTime = 0f;
	public const float LightTime = 1.5f;
    public Color DefaultColor;

    private Color targetColor;
    private float sign = 0f;

    private Character currentCharacter;

	void Update ()
    {
        if (sign == 0f) return;

        elapsedTime += Time.deltaTime * sign;

        float ratio = elapsedTime / LightTime;

        symbol.color = Color.Lerp(DefaultColor, targetColor, ratio);

        if (ratio < 0f || ratio > 1f)
	    {
		    sign = 0f;
	    }
	}

	void Start()
	{
		float col = 80f/255f; // Random.value;
		GetComponent<Renderer>().material.color = new Color(col, col, col);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && currentCharacter == null)
        {
            currentCharacter = col.gameObject.GetComponent<Character>();

            if (currentCharacter)
            {
                symbol.color = currentCharacter.TrailColor;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" && currentCharacter != null && currentCharacter.gameObject == col.gameObject)
        {
            StartCoroutine(FadeColorOut());
        }
    }

    private IEnumerator FadeColorOut()
    {
        currentCharacter = null;

        Color startColor = symbol.color;

        float ratio = 0f;

        while (ratio < 1f)
        {
            ratio += Time.deltaTime / LightTime;
            symbol.color = Color.Lerp(startColor, DefaultColor, ratio);

            yield return null;
        }
    }

	public void SetSymbol(int letterNum)
	{
		this.letterNum = letterNum;
		symbol.sprite = Resources.Load<Sprite>("runic_" + (char)('a' + letterNum));
	}
}
