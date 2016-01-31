using UnityEngine;
using System.Collections;

public class MainCameraBehavior : MonoBehaviour {

    public Transform targetTransform;

    public float smooth = 1.5f;         // The relative speed at which the camera will catch up.

    public float screenCenterOffsetX = 0f;
    public float screenCenterOffsetY = 0f;

    private Vector3 relCameraPos;       // The relative position of the camera from the targetTransform.
    private float relCameraPosMag;      // The distance of the camera from the targetTransform.
    private Vector3 newPos;             // The position the camera is trying to reach.


    void Awake()
    {
        // Setting the relative position as the initial relative position of the camera in the scene.
        relCameraPos = transform.position - targetTransform.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
    }


    void FixedUpdate()
    {
        // The standard position of the camera is the relative position of the camera from the targetTransform.
        Vector3 standardPos = targetTransform.position + relCameraPos;

        // The abovePos is directly above the targetTransform at the same distance as the standard position.
        Vector3 abovePos = targetTransform.position + Vector3.up * relCameraPosMag;

        // An array of 5 points to check if the camera can see the targetTransform.
        Vector3[] checkPoints = new Vector3[5];

        // The first is the standard position of the camera.
        checkPoints[0] = standardPos;

        // The next three are 25%, 50% and 75% of the distance between the standard position and abovePos.
        checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
        checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);

        // The last is the abovePos.
        checkPoints[4] = abovePos;

        // Run through the check points...
        for (int i = 0; i < checkPoints.Length; i++)
        {
            // ... if the camera can see the targetTransform...
            if (ViewingPosCheck(checkPoints[i]))
                // ... break from the loop.
                break;
        }

        // Lerp the camera's position between it's current position and it's new position.
        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);

        // Make sure the camera is looking at the targetTransform.
        SmoothLookAt();
    }


    bool ViewingPosCheck(Vector3 checkPos)
    {
        RaycastHit hit;

        // If a raycast from the check position to the targetTransform hits something...
        if (Physics.Raycast(checkPos, targetTransform.position - checkPos, out hit, relCameraPosMag))
            // ... if it is not the targetTransform...
            if (hit.transform != targetTransform)
                // This position isn't appropriate.
                return false;

        // If we haven't hit anything or we've hit the targetTransform, this is an appropriate position.
        newPos = checkPos;
        return true;
    }


    void SmoothLookAt()
    {
        // Create a vector from the camera towards the targetTransform.
        Vector3 reltargetTransformPosition = targetTransform.position - transform.position;

        // Create a rotation based on the relative position of the targetTransform being the forward vector.
        Quaternion lookAtRotation = Quaternion.LookRotation(reltargetTransformPosition, Vector3.up);

        // give an offset to look at centering
        lookAtRotation.x += screenCenterOffsetY;
        lookAtRotation.y += screenCenterOffsetX;

        // Lerp the camera's rotation between it's current rotation and the rotation that looks at the targetTransform.
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }


}
