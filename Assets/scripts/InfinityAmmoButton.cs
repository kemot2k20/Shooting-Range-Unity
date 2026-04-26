using System;
using UnityEngine;

public class InfinityAmmoButton : MonoBehaviour
{
    public gun gun;
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
    public void Hit()
    {
        Debug.Log("infinity button hit!");
        if (gun != null)
        {
            if (gun.infinityAmmo == false) gun.infinityAmmo = true;
            else gun.infinityAmmo = false;
        }
        
        GetComponent<Renderer>().material.color = Color.red;
        Invoke("ResetColor", 0.5f);
    }

    void ResetColor()
    {
        GetComponent<Renderer>().material.color = Color.green;
    }
}
