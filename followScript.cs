using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followScript : MonoBehaviour
{
    public float followSpeed = 2.0f; // Speed of the camera's following movement
    public GameObject target; // The target object to follow
    public float distance = 10.0f; // Distance from the target

    private Vector3 offset; // The offset from the target
    private float initialHeight; // Initial height of the camera
    public bool CanFollow;

    void Start()
    {
        if (target != null)
        {
            // Calculate the initial offset from the target and distance
            offset = transform.position - target.transform.position;
            offset = offset.normalized * distance;

            // Save the initial height of the camera
            initialHeight = transform.position.y;
        }
    }

    void Update()
    {
        if (CanFollow)
        {
            if (target != null)
            {
                FollowTarget();
            }
        }
    }

    void FollowTarget()
    {
        // Calculate the target position while maintaining the same distance
        Vector3 desiredPosition = target.transform.position + offset;

        // Interpolate smoothly to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);

        // Maintain the initial height
        transform.position = new Vector3(transform.position.x, initialHeight, transform.position.z);

        // Make the camera look at the target
        transform.LookAt(target.transform);
    }
}
