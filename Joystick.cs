using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Image backgroundImage; // The stationary background image
    public Image arrowImage;      // The arrow image that moves and rotates
    public float moveSensitivity = 1.0f; // Sensitivity of the arrow movement
    public float rotateSpeed = 100f; // Speed of rotation

    private Vector2 inputVector;
    private Vector2 originalArrowPosition; // Store the original position of the arrow

    private void Start()
    {
        if (backgroundImage == null || arrowImage == null)
        {
            Debug.LogError("backgroundImage or arrowImage is not assigned in the inspector.");
            return;
        }
        originalArrowPosition = arrowImage.rectTransform.anchoredPosition; // Store the original position
    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / backgroundImage.rectTransform.sizeDelta.x) * 2;
            pos.y = (pos.y / backgroundImage.rectTransform.sizeDelta.y) * 2;

            inputVector = new Vector2(pos.x, pos.y);
            if (inputVector.magnitude > 1.0f)
            {
                inputVector = inputVector.normalized; // Normalize if the magnitude is greater than 1
            }

            // Move Arrow Image based on inputVector
            arrowImage.rectTransform.anchoredPosition = originalArrowPosition + inputVector * moveSensitivity * (backgroundImage.rectTransform.sizeDelta.x / 2);

            // Rotate Background Image
            float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
            backgroundImage.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped); // Handle pointer down as a drag
    }

    public void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        arrowImage.rectTransform.anchoredPosition = originalArrowPosition; // Reset arrow position to original
        backgroundImage.rectTransform.rotation = Quaternion.identity; // Reset background rotation
    }

    public float Horizontal()
    {
        return inputVector.x;
    }

    public float Vertical()
    {
        return inputVector.y;
    }

    public Vector2 Direction()
    {
        return new Vector2(Horizontal(), Vertical());
    }
}
