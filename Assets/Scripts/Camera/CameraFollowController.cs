using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
	public Transform objectToFollow;
	public Vector3 offset;
	public float followSpeed = 10;
	public float lookSpeed = 10;

	public enum Modes
	{
		Default,
		WorldStatic,
		World,
		PulledString,
	}

	public Modes Mode;

	Vector3 LastTargetPos;

	private void Awake()
	{
		LastTargetPos = objectToFollow.position + Vector3.up * offset.y;
	}

	public void LookAtTarget()
	{
		Vector3 _lookDirection = objectToFollow.position - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
	}

	public void MoveToTarget()
	{

		Vector3 _targetPos = offset;

		if (Mode == Modes.World)
		{
			_targetPos = objectToFollow.position + offset;
		}
		if (Mode == Modes.PulledString)
		{
			_targetPos = LastTargetPos;

			Vector3 target = objectToFollow.position + Vector3.up * offset.y;
			float distance = Vector3.Distance(objectToFollow.position + Vector3.up * offset.y, LastTargetPos);

			_targetPos = Vector3.MoveTowards(LastTargetPos, target, distance - offset.x);
		}
		else if (Mode == Modes.Default)
		{
			_targetPos = objectToFollow.position +
								objectToFollow.forward * offset.z +
								objectToFollow.right * offset.x +
								objectToFollow.up * offset.y;

		}


		Debug.DrawLine(objectToFollow.position, _targetPos);
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
		LastTargetPos = _targetPos;
	}

	private void FixedUpdate()
	{
		LookAtTarget();
		MoveToTarget();
	}
}
