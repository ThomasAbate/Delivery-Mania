using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] PatrolPath patrolPath;

    [SerializeField] float wayPointTolerance = 1f;

	Vector3 botPosition;
	int currentWayPointIndex = 0;
	
	BotController botController;

	private Rigidbody BotRb;

	public float speed = 4f;

	//private Vector2 direction;

	[SerializeField] private float smoothTime = 0.05f;
	private Vector3 Velocity;

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
		transform.forward = nextPosition;

		BotRb.MovePosition(Vector3.MoveTowards(transform.position, nextPosition, speed * Time.fixedDeltaTime));

		transform.forward = Vector3.SmoothDamp(transform.forward, nextPosition, ref Velocity, smoothTime);
	}
}
