using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public float MoveSpeed;
    public float TurnSpeed;

    private Rigidbody rb;
    private Quaternion targetRot;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRot, TurnSpeed * Time.deltaTime);
    }

    public void Move(float xValue, float zValue)
    {
        Vector3 forwardDir = Camera.main.transform.forward;
        Vector3 rightDir = Camera.main.transform.right;

        forwardDir.y = 0f;
        forwardDir = forwardDir.normalized * zValue;

        rightDir.y = 0f;
        rightDir = rightDir.normalized * xValue;

        Vector3 newVelocity = (forwardDir + rightDir) * MoveSpeed;

        if (newVelocity != Vector3.zero)
        {
            // We rotate to face the new direction
            targetRot = Quaternion.LookRotation(newVelocity.normalized);
        }

        newVelocity.y = rb.velocity.y;

        rb.velocity = newVelocity;
    }
}
