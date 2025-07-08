using UnityEngine;

public class DoorSc : MonoBehaviour
{
    private bool isOpening = false;
    private bool isOpened = false;
    private float currentAngle = 0f;
    [SerializeField] private float openAngle = 90f;     // �J���p�x
    [SerializeField] private float openSpeed = 90f;     // ��]���x
    [SerializeField] private bool openOutward = true;   // �O�J�� or ���J��


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening && !isOpened)
        {
            float angleStep = openSpeed * Time.deltaTime;
            float remaining = openAngle - currentAngle;

            if (angleStep >= remaining)
            {
                angleStep = remaining;
                isOpened = true;
            }

            float direction = openOutward ? 1f : -1f;
            transform.Rotate(Vector3.up, angleStep * direction);

            currentAngle += angleStep;
        }
    }

    public void OpenDoor()
    {
        if (!isOpening)
        {
            isOpening = true;
            Debug.Log("�h�A���J���܂�");
        }
    }

}
