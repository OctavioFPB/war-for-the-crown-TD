using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    public Transform tgt;

    public float hitRadius = 0.3f;
    public float orphanRange = 3f;
    private Vector3 dir = Vector3.right;
    private Vector3 lastPos;
    private bool hasPos;
    private bool orphaned;
    private float orphanTravelled;

    void Update()
    {
        if (tgt != null)
        {
            lastPos = tgt.position;
            hasPos = true;
        }
        else if (hasPos)
        {
            orphaned = true;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (!orphaned) dir = (lastPos - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        if (!orphaned)
        {
            if (Vector2.Distance(transform.position, tgt.position) < 0.15f)
                Hit(tgt.GetComponent<Enemy>());
            return;
        }

        foreach (Enemy e in GameObject.FindObjectsByType<Enemy>())
        {
            if (Vector2.Distance(transform.position, e.transform.position) < hitRadius)
            {
                Hit(e);
                return;
            }
        }

        orphanTravelled += speed * Time.deltaTime;
        if (orphanTravelled >= orphanRange) Destroy(gameObject);
    }

    void Hit(Enemy e)
    {
        if (e != null)
        {
            e.health -= 1;
            if (e.health <= 0) Destroy(e.gameObject);
        }
        CoinManager.instance.UpdateCoins(5);
        Destroy(gameObject);
    }
}
