using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform target;
	public Vector3 cameraOffset;
	public float followSpeed = 10f;
	public float xMin = 0f;
	public float xMax = 100f;
	Vector3 velocity = Vector3.zero;
	public static CameraController instance;

	//public float aheadAmount, aheadSpeed;

	void Start()
	{

		target = FindObjectOfType<PlayerController>().transform;
	}


	void Update()
	{

		//if (Input.GetAxisRaw("Horizontal") != 0)
		//{
		//	target.localPosition = new Vector3(Mathf.Lerp(target.localPosition.x, aheadAmount * Input.GetAxisRaw("Horizontal"), aheadSpeed * Time.deltaTime), target.localPosition.y, target.localPosition.z);
		//}

	}

	void FixedUpdate()
	{
		Vector3 targetPos = target.position + cameraOffset;
		Vector3 clampedPos = new Vector3(Mathf.Clamp(targetPos.x, xMin, float.MaxValue), targetPos.y, targetPos.z);
		Vector3 smoothPos = Vector3.SmoothDamp(transform.position, clampedPos, ref velocity, followSpeed * Time.fixedDeltaTime);
		transform.position = smoothPos;
	}
}