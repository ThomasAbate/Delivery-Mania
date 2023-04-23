using UnityEngine;

public class PatrolPath : MonoBehaviour
{
	const float gizmoRadius = 0.4f;

	private void OnDrawGizmos()
	{
		for(int i = 0; i < transform.childCount; i++) //goes from 0 to Child Count - 1 (0, 1, 2, ..., ChildCount-1)
		{
			int j = GetNextIndex(i);

			//To Vizualize The Patrol Path
			Gizmos.DrawSphere(GetWayPoint(i), gizmoRadius);
			Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
		}
	}

	public int GetNextIndex(int i)
	{
		if(i+1 == transform.childCount)
		{
			return 0;
		}

		return i + 1;
	}

	public Vector3 GetWayPoint(int i)
	{
		return transform.GetChild(i).position;
	}
}
