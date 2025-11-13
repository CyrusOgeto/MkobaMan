using UnityEngine;

public class BoondocksCam : MonoBehaviour
{
    public float jamaa = 1.0f;      // Magnitude of the camera movement
    public float danceSpeed = 2.0f; // Speed of the camera movement
    public float randomness = 1.0f; // Amount of randomness in the movement

    private Vector3 originalPosition;
    public bool isDancing = false;
    private float startTime;

    void Start()
    {
        originalPosition = transform.localPosition;
        startTime = Time.time;
    }

    void Update()
    {
        if (isDancing)
        {
            float elapsedTime = Time.time - startTime;

            // Random movement offsets
            float xOffset = Mathf.PerlinNoise(elapsedTime * danceSpeed, 0) * 2 - 1;
            float yOffset = Mathf.PerlinNoise(0, elapsedTime * danceSpeed) * 2 - 1;

            // Apply randomness to the movement
            Vector3 randomMovement = new Vector3(xOffset, yOffset, 0) * jamaa;
            transform.localPosition = originalPosition + randomMovement;
        }
        else
        {
            // Return to original position if not dancing
            transform.localPosition = originalPosition;
        }
    }

    // Method to toggle dance mode from another class
    public void SetDanceMode(bool enableDance)
    {
        isDancing = enableDance;
        if (isDancing)
        {
            startTime = Time.time; // Reset the start time for randomness
         //   Debug.Log("isdancing imeekwa " + isDancing);
        }
    }
}
