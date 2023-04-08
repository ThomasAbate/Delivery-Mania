using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
	Transform target;

	PatrolPath path;

	private void Update()
	{
		//path.GetNextIndex(i);
		//target.position = path.GetWayPoint(i);

		GetComponent<NavMeshAgent>().destination = target.position;
	}
}
