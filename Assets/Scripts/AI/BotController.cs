using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
	[SerializeField] Transform target;

	PatrolPath path;

	private void Update()
	{
		/*for (int i = 0; i < 10; i++) //goes from o to 9)
		{
			path.GetNextIndex(i);
			target.position = path.GetWayPoint(i);
		}
		GetComponent<NavMeshAgent>().destination = target.position;*/
	}
}
