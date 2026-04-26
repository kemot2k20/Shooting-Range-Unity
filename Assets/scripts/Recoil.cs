using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float recoilX = -2.5f; 
    public float recoilY = 0.2f;
    public float recoilZ = 0.1f;

    public float recoilSpeed = 6f;
    public float returnSpeed = 4f;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);

        currentRotation = Vector3.Slerp(currentRotation, targetRotation, recoilSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}