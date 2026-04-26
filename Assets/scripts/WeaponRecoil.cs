using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public Vector3 recoilPosition = new Vector3(0, 0, -0.05f); // cofniecie broni
    public Vector3 recoilRotation = new Vector3(-6.5f, 3f, 0);  // podbicie lufy i lekki obrót broni

    public float recoilSpeed = 6f; 
    public float returnSpeed = 4f;

    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    void Update()
    {
        targetPosition = Vector3.Lerp(targetPosition, Vector3.zero, returnSpeed * Time.deltaTime);
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);

        currentPosition = Vector3.Lerp(currentPosition, targetPosition, recoilSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, recoilSpeed * Time.deltaTime);

        transform.localPosition = currentPosition;
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void FireWeaponRecoil()
    {
        targetPosition += new Vector3(
            Random.Range(-recoilPosition.x, recoilPosition.x), 
            Random.Range(-recoilPosition.y, recoilPosition.y), 
            recoilPosition.z
        );

        targetRotation += new Vector3(
            recoilRotation.x, 
            Random.Range(-recoilRotation.y, recoilRotation.y), 
            Random.Range(-recoilRotation.z, recoilRotation.z)
        );
    }
}