using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Drone : MonoBehaviour
{
    public float startSpeed = .1f;
    [HideInInspector]
    public float moveSpeed;
    public float rotationSpeed = 5f;

    private GameObject targetTurret;
    private Turret turretScript;
    private bool isReloading = false;
    private float reloadTime = 0f;
    public float startHealth = 100;
    private float health;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        moveSpeed = startSpeed;
        health = startHealth;
        EnemyBullet[] bullets = FindObjectsOfType<EnemyBullet>();
    }

    public void TakeDamage(float amount)
    {
        // healthBar.gameObject.SetActive(true);
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        // speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        audioManager.PlaySFX(audioManager.missleCrash);
        isDead = true;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }

    void Update()
    {
        if (!isReloading)
        {
            FindTargetTurret();
            if (targetTurret != null)
            {
                StartCoroutine(MoveAndReload());
            }
        }
    }

    void FindTargetTurret()
    {
        Turret[] turrets = FindObjectsOfType<Turret>();

        if (turrets.Length > 0)
        {
            turrets = turrets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).ToArray();

            foreach (Turret turret in turrets)
            {
                if (turret.bulletNumber == 0)
                {
                    targetTurret = turret.gameObject;
                    turretScript = turret;
                    return;
                }
            }
        }

        targetTurret = null;
        turretScript = null;
    }

    IEnumerator MoveAndReload()
    {
        if (targetTurret == null || turretScript == null)
        {
            yield break;
        }

        Vector3 targetPosition = targetTurret.transform.position;
        targetPosition.y = transform.position.y;

        float startTime = Time.time;
        float journeyLength = Vector3.Distance(transform.position, targetPosition);
        // float smoothTime = 0.05f; // Giảm giá trị để làm cho di chuyển mượt mà hơn
        Vector3 velocity = Vector3.zero;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            Vector3 dir = targetPosition - transform.position;
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);

            // Calculate quaternion
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            // float distCovered = (Time.time - startTime) * moveSpeed;
            // float fracJourney = distCovered / journeyLength;

            // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            // // Lấy hướng từ vị trí hiện tại đến vị trí đích
            // Vector3 lookDirection = (targetPosition - transform.position).normalized;

            // // Áp dụng xoay cho hướng nhìn
            // transform.forward = lookDirection;

            // Né tránh đạn
            EvadeBullets();

            yield return null;
        }

        yield return new WaitForSeconds(reloadTime);

        if (targetTurret != null && turretScript != null)
        {
            turretScript.Reload();
            targetTurret = null;
            turretScript = null;
            isReloading = false;
        }
    }

    void EvadeBullets()
    {
        EnemyBullet[] bullets = FindObjectsOfType<EnemyBullet>();

        foreach (EnemyBullet bullet in bullets)
        {
            float distanceToBullet = Vector3.Distance(transform.position, bullet.transform.position);
            float evasionThreshold = 10f;

            if (distanceToBullet < evasionThreshold)
            {
                Vector3 evadeDirection = (transform.position - bullet.transform.position).normalized;
                transform.position += evadeDirection * Time.deltaTime * moveSpeed;
            }
        }
    }
}
