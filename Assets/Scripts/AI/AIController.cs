using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] PatrolPath patrolPath;

    [SerializeField] float wayPointTolerance = 1f;

	Vector3 botPosition;

	int currentWayPointIndex = 0;

	public Rigidbody BotRb;

	public float speed = 4f;

	[SerializeField] private float smoothTime = 0.5f;
	private Vector3 Velocity;

	private void Update()
	{
		PatrolBehaviour();
	}

	public void PatrolBehaviour()
	{
		Vector3 nextPosition = botPosition;

		if(patrolPath != null)
		{
			if (AtWayPoint())
			{
				CycleWaypoint();
			}

			nextPosition = GetCurrentWaypoint();
		}

		BotMove(nextPosition);
	}

	private bool AtWayPoint()
	{
		float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());

		return distanceToWaypoint < wayPointTolerance;
	}
	
	private void CycleWaypoint()
	{
		currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
	}

	private Vector3 GetCurrentWaypoint()
	{
		return patrolPath.GetWayPoint(currentWayPointIndex);
	}

	public void BotMove(Vector3 nextPosition)
	{
		BotRb.MovePosition(Vector3.MoveTowards(transform.position, nextPosition, speed * Time.fixedDeltaTime));

		transform.forward = Vector3.SmoothDamp(transform.forward, nextPosition, ref Velocity, smoothTime);

		transform.forward = nextPosition;
	}
}
