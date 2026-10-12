using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;

    private bool isOpen;

    [SerializeField] private Quaternion closeRotation;
    [SerializeField] private Quaternion openRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        closeRotation = transform.localRotation;
        openRotation = closeRotation * Quaternion.Euler(0, openAngle, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation;

        if (isOpen)
        {
            targetRotation = openRotation;
        }
        else
        {
            targetRotation = closeRotation;
        }

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, openSpeed * Time.deltaTime);
    }

    public void Interact()
    {
        isOpen = !isOpen;
    }

    public string GetGuideText()
    {
        if (isOpen)
        {
            return "[E] 문 닫기";
        }
        return "[E] 문 열기";
    }
}
