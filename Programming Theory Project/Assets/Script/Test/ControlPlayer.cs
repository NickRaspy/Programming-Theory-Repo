using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPlayer : MonoBehaviour
{
    public float speed = 1f;
    private float _currentVerticalAngle;
    public float sensitivity = 1f;

    private bool isPaused = false;

    private GameObject ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal")*Time.fixedDeltaTime*speed, 0, Input.GetAxis("Vertical") * Time.fixedDeltaTime * speed);
        if(transform.Find("Main Camera"))
        {
            transform.Find("Main Camera").Rotate(-Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, Input.GetAxis("Mouse X")*sensitivity*Time.deltaTime, 0);
            transform.Rotate(0, Input.GetAxis("Mouse X")*sensitivity*Time.deltaTime, 0);
            _currentVerticalAngle = Mathf.Clamp(_currentVerticalAngle - Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, -80, 80);
            transform.Find("Main Camera").localRotation = Quaternion.Euler(_currentVerticalAngle, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                ui.SetActive(true);
                sensitivity = GameObject.Find("Canvas").transform.Find("Sensitivity").GetComponent<Slider>().value;
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                ui.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
