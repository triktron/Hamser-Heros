using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollowController : MonoBehaviour
{
	Transform Target;
	public Vector3 offset;
	public float followSpeed = 10;
	public float lookSpeed = 10;
	public float ManualRotateSpeed = 50;

	public enum Modes
	{
		Default,
		WorldStatic,
		World,
		PulledString,
	}

	public bool Stationary;

	public Modes Mode;

	Vector3 LastTargetPos;


	public void Awaken(Transform target)
	{
		Target = target;
		target.GetComponent<PlayerController>().Cam = this;
		LastTargetPos = transform.position;
		enabled = true;
	}

	public void Sleep(Transform target)
	{
		enabled = false;
	}

	public void LookAtTarget()
	{
		Vector3 _lookDirection = Target.position - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
	}

	public void MoveToTarget()
	{

		Vector3 _targetPos = offset;

		if (Mode == Modes.World)
		{
			_targetPos = Target.position + offset;
		}
		if (Mode == Modes.PulledString)
		{
			Vector3 target = Target.position + Vector3.up * offset.y;
			float distance = Vector3.Distance(Target.position + Vector3.up * offset.y, LastTargetPos);

			_targetPos = Vector3.MoveTowards(LastTargetPos, target, distance - offset.x);
			_targetPos.y = Target.position.y + offset.y;
		}
		else if (Mode == Modes.Default)
		{
			_targetPos = Target.position +
								Target.forward * offset.z +
								Target.right * offset.x +
								Target.up * offset.y;

		}


		Debug.DrawLine(Target.position, _targetPos);
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
		LastTargetPos = _targetPos;
	}

	public void Look(InputAction.CallbackContext context)
	{
		var input = context.ReadValue<Vector2>();
		RotateCamereAngle(input.x);
	}

	void RotateCamereAngle(float angle)
	{
		//transform.Rotate(0, angle, 0);
		
		if (enabled && !PauseMenu.Paused)
		{
			Vector3 center = Target.position;
			center.y = transform.transform.position.y;

			LastTargetPos = RotatePointAroundPivot(transform.transform.position, center, Vector3.up * angle);
			transform.transform.position = LastTargetPos;

			transform.LookAt(Target);
		}
		
	}

	Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
	{
		return Quaternion.Euler(angles) * (point - pivot) + pivot;
	}

	private void FixedUpdate()
	{
		LookAtTarget();
		if (!Stationary) MoveToTarget();
	}
}
