using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SelectorWithBolts : MonoBehaviour
{
    public Transform[] Bolts;
    public Transform[] RootCylinders;
    public GameObject[] Ropes;
    public Color[] DashColors;

    private const float DASH_COOLDOWN = 6f;

    private Rigidbody rb;
    private MeshRenderer[][] playerCylinders;

    private SoundManager sm;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerCylinders = new MeshRenderer[Ropes.Length][];

        for (int i = 0; i < Ropes.Length; i++)
        {
            playerCylinders[i] = Ropes[i].GetComponent<Transform>().GetComponentsInChildren<MeshRenderer>();
        }

        sm = SoundManager.Instance;
    }

    void Start()
    {
        for (int i = 0; i < Ropes.Length; i++)
        {
            ReplenishPlayerDashMeter(i);
        }
    }

    void Update()
    {
        for (int i = 0; i < RootCylinders.Length; i++)
		{
            Vector3 constraintPos = Bolts[i].position;
            RootCylinders[i].position = constraintPos;
		}

        if( rb.velocity.sqrMagnitude > 0.7f)
        {
            if (sm.Scrapping.isPlaying)
            {
                sm.UpdateScrappingVolume( Mathf.Clamp(rb.velocity.sqrMagnitude * 0.05f , 0f, 1f));
            }
            else
            {
                sm.PlayScrappingSound(Mathf.Clamp(rb.velocity.sqrMagnitude * 0.05f, 0f, 1f));
            }
        }
        else
        {
            sm.StopScrappingSound();
        }
    }

    public void ReplenishPlayerDashMeter(int playerId)
    {
        ResetRope(playerId);

        StartCoroutine(ReplenishPlayerDashMeterCoroutine(playerId));
    }

    private void ResetRope(int playerId)
    {
        foreach (MeshRenderer renderer in playerCylinders[playerId])
        {
            renderer.material.color = Color.gray;
        }
    }

    private IEnumerator ReplenishPlayerDashMeterCoroutine(int playerId)
    {
        float elapsedTime = 0f;

        int cylindersCount = playerCylinders[playerId].Length;

        while (elapsedTime < DASH_COOLDOWN)
        {
            
            elapsedTime += Time.deltaTime;
            int cylinderIndex = (int)(elapsedTime * cylindersCount / DASH_COOLDOWN - 1);

            playerCylinders[playerId][cylinderIndex].material.color = DashColors[playerId];

            yield return null;
        }
    }
}
