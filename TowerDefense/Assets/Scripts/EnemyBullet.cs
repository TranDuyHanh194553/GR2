using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

	private Transform target;

	public float speed = 70f;

	public int damage = 50;

	public float explosionRadius = 0f;
	public GameObject impactEffect;

	private Vector3 startPosition;

	private Vector3 instantTargetPosition;

	private Vector3 initialDirection;
	AudioManager audioManager;
	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}
	// private bool hasReachedTarget = false;
	void Start()
	{
		startPosition = transform.position;

		initialDirection = (instantTargetPosition - startPosition).normalized;
	}



	public void Seek(Transform _target)
	{
		target = _target;

		if (target != null)
		{
			instantTargetPosition = target.position;
		}
	}

	// Update is called once per frame
	void Update()
	{

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}


		// transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.position += initialDirection * distanceThisFrame;
		// // Di chuyển đạn theo hướng thẳng đến mục tiêu
		// transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		// Quay đạn để hướng về mục tiêu
		// transform.LookAt(target.position);
		// Hủy đối tượng nếu di chuyển quá xa
		DestroyIfOutOfRange();

	}

	void DestroyIfOutOfRange()
	{
		// Kiểm tra nếu viên đạn đã đi quá xa (ví dụ: 1000 đơn vị)
		if (Vector3.Distance(transform.position, startPosition) > 200f)
		{
			Destroy(gameObject);
		}
	}

	void HitTarget()
	{
		GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 5f);

		if (explosionRadius > 0f)
		{
			Explode();
		}
		else
		{
			Damage(target);
		}

		Destroy(gameObject);
	}

	void Explode()
	{
		audioManager.PlaySFX(audioManager.bulletShoot);

		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag == "Ally")
			{
				Damage(collider.transform);
			}
		}
	}

	void Damage(Transform enemy)
	{
		Drone e = enemy.GetComponent<Drone>();

		if (e != null)
		{
			e.TakeDamage(damage);

			Debug.Log("Exist enemy");
		}
		else
		{
			Debug.Log("No damaged Enemy");
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
