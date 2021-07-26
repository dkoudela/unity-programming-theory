using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocketController : MonoBehaviour
{
    // ENCAPSULATION
    public GameObject Enemy { get; set; }  = null;
    public bool Activated { get; set; } = false;
    private float speed = 40.0f;
    private float rotationSpeed = 5.0f;
    private float timer = 0;
    private float ttl = 10;
    private Rigidbody homingRocketRb;

    public static HomingRocketController GetHomingRocketController(Vector3 source, Vector3 target, GameObject projectilePrefab)
    {
        Vector3 positionDiff = target - source;
        Quaternion newRotation = Quaternion.FromToRotation(projectilePrefab.transform.rotation.eulerAngles, positionDiff);
        GameObject projectile = GameObject.Instantiate(projectilePrefab, source, newRotation);
        HomingRocketController homingRocketController = projectile.GetComponent<HomingRocketController>();
        return homingRocketController;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        homingRocketRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (null != Enemy)
        {
            Activated = true;
            timer += Time.deltaTime;

            // Adapt direction
            Vector3 lookDirection = (Enemy.transform.position - transform.position).normalized;
            homingRocketRb.AddForce(lookDirection * speed);

            if (timer > ttl)
            {
                Destroy(gameObject);
            }
        }
        else if (Activated)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            if (0 == other.gameObject.name.CompareTo(Enemy.name))
            {
                Destroy(gameObject);
                Health health = other.gameObject.GetComponent<Health>();
                health.DecreaseHealth();
            }
        }
    }

    void FixedUpdate()
    {
        if (null != Enemy)
        {
            // Adapt rotation
            Vector3 targetDir = Enemy.transform.position - transform.position;
            Vector3 forward = transform.forward;
            Vector3 localTarget = transform.InverseTransformPoint(Enemy.transform.position);

            float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

            Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime * rotationSpeed);
            homingRocketRb.MoveRotation(homingRocketRb.rotation * deltaRotation);
        }
    }
}
