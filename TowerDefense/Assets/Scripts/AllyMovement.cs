using UnityEngine;

[RequireComponent(typeof(Ally))]
public class AllyMovement : MonoBehaviour
{
	private Transform target;
	private int wavepointIndex = 0;

	private Ally ally;

	public float rotationSpeed = 5f;

	void Start()
	{
		ally = GetComponent<Ally>();
		target = AllyWaypoints.points[0];
	}

	void Update()
	{
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * ally.speed * Time.deltaTime, Space.World);

		// Calculate quaternion
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (Vector3.Distance(transform.position, target.position) <= 0.4f)
		{
			GetNextWaypoint();
		}

		ally.speed = ally.startSpeed;
	}

	void GetNextWaypoint()
	{
		if (wavepointIndex >= AllyWaypoints.points.Length - 1)
		{
			EndPath();
			return;
		}

		wavepointIndex++;
		target = AllyWaypoints.points[wavepointIndex];
	}
	void EndPath()
	{
		ally.Die();
	}
}
