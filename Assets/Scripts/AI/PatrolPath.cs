using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
	const float gizmoRadius = 0.4f;

	private void OnDrawGizmos()
	{
		for(int i = 0; i < transform.childCount; i++) //goes from o to Child Count - 1 (0, 1, 2, etc...)
		{
			int j = GetNextIndex(i);

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