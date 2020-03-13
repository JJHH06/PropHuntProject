using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    public float sensitivity = 10f;
    public float maxYAngle = 90f;
    public float speed = 5f;
    private Vector2 currentRotation;
    private bool mouseLocked;

    private PhotonView PV;

    void Start()
    {
        mouseLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PV.IsMine)
            Move();


    }

    void Move()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        gameObject.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        if (Input.GetMouseButtonDown(0))
        {
            if (mouseLocked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;

            mouseLocked = !mouseLocked;
        }

        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }
}
