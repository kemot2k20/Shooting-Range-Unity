using UnityEngine;

public class kolysanie : MonoBehaviour
{
    public float amount = 0.05f;
    public float smoothness = 6f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        Vector3 movementVec = new Vector3(movementX, movementY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, movementVec + initialPosition, Time.deltaTime * smoothness);
    }
}
