using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
	private Transform target;
	private NavMeshAgent agent;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		if (target != null)
		{
			agent.SetDestination(target.transform.position);
			FaceTarget();
		}
	}

	public void MoveToPoint(Vector3 point)
	{
		agent.SetDestination(point);
	}

	public void FollowTarget(Interactable newTarget)
	{
		agent.stoppingDistance = newTarget.radius * 0.8f;
		agent.updateRotation = false;

		target = newTarget.interactionTransform;
	}

	public void StopFollowingTarget()
	{
		agent.stoppingDistance = 0f;
		agent.updateRotation = true;

		target = null;
	}

	private void FaceTarget()
	{
		var direction = (target.position - transform.position).normalized;
		var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}
