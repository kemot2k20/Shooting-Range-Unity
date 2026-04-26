using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class gun : MonoBehaviour
{
    public bool infinityAmmo = false;
    public float damage = 10f;
    public float range = 10000f;

    public int maxAmmo = 30;
    private int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    public TextMeshProUGUI ammoText;

    public Camera Cam;
    public ParticleSystem flash;

    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip reloadSound;

    public AudioClip buttonSound;

    public Recoil recoilScript;

    public WeaponRecoil weaponRecoilScript;

    public ChallengeManager challengeManager;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void OnEnable()
    {
        isReloading = false;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (infinityAmmo)
        {
            currentAmmo = maxAmmo;
        }

        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && !infinityAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        //Debug.Log("Reload...");
        if (audioSource != null && reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);
        }

        ammoText.color = Color.orange;
        ammoText.text = "..."; 

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        
        UpdateAmmoUI();
        //Debug.Log("Reload Finnished");
    }

    void Shoot()
    {
        if (!infinityAmmo) currentAmmo--;
        UpdateAmmoUI();

        if (recoilScript != null)
        {
            recoilScript.RecoilFire();
        }

        if (weaponRecoilScript != null)
        {
            weaponRecoilScript.FireWeaponRecoil();
        }

        flash.Play();
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        if (challengeManager != null)
        {
            challengeManager.StartTimer();
        }

        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, range))
        {
            StartButton button = hit.transform.GetComponent<StartButton>();
            if (button != null)
            {
                button.Hit();
                audioSource.PlayOneShot(buttonSound);
                return;
            }

            InfinityAmmoButton infinityButton = hit.transform.GetComponent<InfinityAmmoButton>();
            if (infinityButton != null)
            {
                infinityButton.Hit();
                audioSource.PlayOneShot(buttonSound);
                UpdateAmmoUI();
                return;
            } 

            target t = hit.transform.GetComponentInParent<target>();
            if (t != null)
            {
                if (audioSource != null && hitSound != null)
                {
                    audioSource.PlayOneShot(hitSound);
                }
                t.Die();
            }
        }
    }
    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            if (infinityAmmo)
            {
                ammoText.text = "∞";
                return;
            }
            if (currentAmmo == 0)
            {
                ammoText.color = Color.red;
            } 
            else
            {
                ammoText.color = Color.cyan;
            }
            ammoText.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
        }
    }
}