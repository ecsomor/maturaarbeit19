using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAtIt : MonoBehaviour
{
	// Setzen der Grenzen.
	public float minimumX = -60f;
	public float maximumX = 60f;
	public float minimumY = -360f;
	public float maximumY = 360;

	public float sensitivityX = 15f;
	public float sensitivityY = 15f;

	// Verbinden des Kameraobjekts
	public Camera cam;

	float rotationY = 0f;

	float rotationX = 0f;

	// Use this for initialization
	void Start ()
	{
		// Mauscursor festhalten
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update ()
	{
		// Maus Koordinaten kriegen
		rotationY += Input.GetAxis ("Mouse X") * sensitivityY;
		rotationX += Input.GetAxis ("Mouse Y") * sensitivityX;

		rotationX = Mathf.Clamp (rotationX, minimumX, maximumX);
		// Bewegen des GameObjekts
		transform.localEulerAngles = new Vector3 (0, rotationY, 0);
		cam.transform.localEulerAngles = new Vector3 (-rotationX, 0, 0);
	}
}
