using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileRotate : MonoBehaviour {

	private Camera cameraCache;
	private bool isRotating = false;

	void Start()
	{
		cameraCache = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1)
		{
			switch (Input.touches[0].phase)
			{
			case TouchPhase.Began:
				if (CheckCollidersTouched (GetComponent<Collider> ()))
					isRotating = true;
					break;
				case TouchPhase.Stationary:
				case TouchPhase.Moved:
					transform.Rotate(Vector3.down * ( - Input.touches[0].deltaPosition.x), Space.World);
					break;
				case TouchPhase.Ended:
					if (isRotating)
						isRotating = false;
						break;
			}
		}
	}

	private bool CheckCollidersTouched(Collider collider)
	{
		RaycastHit hit;
		Ray ray = cameraCache.ScreenPointToRay(Input.GetTouch(0).position);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.collider == collider)
				return true;
			else
				return false;
		}
		return false;
	}
}
