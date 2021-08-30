using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoves : MonoBehaviour
{
	public float sensitivityX = 1.5F;
	public float sensitivityY = 1.5F;
	float sensitivityMultiplier = 120F;

	public Transform player;

	float xRotation = 0f;

	void Start ()
	{		
        Cursor.lockState = CursorLockMode.Locked;
	}
 
	void Update ()
	{
		float mouseX = Input.GetAxis("Mouse X") * (sensitivityX * sensitivityMultiplier) * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * (sensitivityY * sensitivityMultiplier) * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		player.Rotate(Vector3.up * mouseX);
	}
}
