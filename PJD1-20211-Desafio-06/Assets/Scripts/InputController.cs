using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private float Horizontal;
    [SerializeField]
    private float Vertical;
    [SerializeField]
    private bool Fire;
    [SerializeField]
    private bool Reload;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private int SelectWeapon;

    [SerializeField]
    private Vector3 MousePosition;

    private void Awake()
    {
        cam = Camera.main;
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        MousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Fire = Input.GetMouseButton(0);
        Reload = Input.GetMouseButton(1);

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectWeapon = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectWeapon = 4;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            SelectWeapon++;
        }
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            SelectWeapon--;
        }


        player.SetInput(Horizontal, Vertical, MousePosition, SelectWeapon, Fire, Reload);

    }
}
