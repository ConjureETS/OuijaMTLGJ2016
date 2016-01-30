using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SelectorWithBolts : MonoBehaviour
{
    public Transform[] Bolts;
    public Transform[] RootCylinders;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        for (int i = 0; i < RootCylinders.Length; i++)
		{
            Vector3 constraintPos = Bolts[i].position;
            RootCylinders[i].position = constraintPos;
		}
    }

    public void ApplyForce(int playerID, Vector3 force)
    {
        rb.AddForceAtPosition(force, Bolts[playerID].position);
    }
}
