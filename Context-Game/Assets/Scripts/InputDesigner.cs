using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject graplerGun;

    void Start()
    {
        graplerGun.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleGraplerGun();
        }
    }

    void ToggleGraplerGun()
    {
        // Toggle the active state of graplerGun
        graplerGun.SetActive(!graplerGun.activeSelf);
    }
}
