using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform pivotPoint;
    public float wobbleAmount = 0.1f;
    public float wobbleSpeed = 2f;
    public float rotationAmount = 2f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    public bool shakeEnabled = true;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (shakeEnabled == true)
        {
            float wobbleX = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
            float wobbleY = Mathf.Sin(Time.time * wobbleSpeed * 2) * (wobbleAmount / 2);
            float wobbleZ = 0f;

            Quaternion rotation = Quaternion.Euler(
                Random.Range(-rotationAmount, rotationAmount),
                Random.Range(-rotationAmount, rotationAmount),
                Random.Range(-rotationAmount, rotationAmount)
            );

            Vector3 targetPosition = pivotPoint.position + new Vector3(wobbleX, wobbleY, wobbleZ);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }
        else
        {
            transform.position = pivotPoint.position;
        }

    }

    public void ToggleShake()
    {
        shakeEnabled = !shakeEnabled;
    }
}