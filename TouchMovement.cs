using UnityEngine;

public class TouchMovement : MonoBehaviour
{
    public float moveSpeed = 5f;           // Speed at which the character moves
    public float rotationSpeed = 720f;     // Speed at which the character rotates
    public float swipeThreshold = 50f;     // Minimum swipe distance to register a move

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isDragging = false;

    private void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            HandleTouch(touch.phase, touch.position);
        }
        // Handle mouse input
        else if (Input.GetMouseButtonDown(0))
        {
            // Start of mouse drag
            startTouchPosition = Input.mousePosition;
            isDragging = true;
            Debug.Log("Mouse drag started");
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            // Continue mouse drag
            currentTouchPosition = (Vector2)Input.mousePosition;
            Vector2 distance = currentTouchPosition - startTouchPosition;

            if (distance.magnitude > swipeThreshold)
            {
                Vector3 moveDirection = new Vector3(distance.x, 0, distance.y).normalized;
                MoveCharacter(moveDirection);
                startTouchPosition = currentTouchPosition; // Update start position for continuous dragging
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // End of mouse drag
            isDragging = false;
            Debug.Log("Mouse drag ended");
        }
    }

    private void HandleTouch(TouchPhase phase, Vector2 touchPosition)
    {
        switch (phase)
        {
            case TouchPhase.Began:
                startTouchPosition = touchPosition;
                isDragging = true;
                Debug.Log("Touch began");
                break;

            case TouchPhase.Moved:
                if (isDragging)
                {
                    currentTouchPosition = touchPosition;
                    Vector2 distance = currentTouchPosition - startTouchPosition;

                    if (distance.magnitude > swipeThreshold)
                    {
                        Vector3 moveDirection = new Vector3(distance.x, 0, distance.y).normalized;
                        MoveCharacter(moveDirection);
                        startTouchPosition = currentTouchPosition; // Update start position for continuous dragging
                    }
                }
                break;

            case TouchPhase.Ended:
                isDragging = false;
                Debug.Log("Touch ended");
                break;
        }
    }

    private void MoveCharacter(Vector3 direction)
    {
        if (direction.magnitude > 0)
        {
            // Rotate the character towards the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move the character forward in the direction it's facing
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
