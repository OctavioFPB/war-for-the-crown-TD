using UnityEngine;

public class Tower : MonoBehaviour
{
    public float FireRate = 1f;
    public float Range = 3f;
    public GameObject projectileprefab;
    public Transform firepoint;

    private float fireCooldown = 0f;

    void Update()
    {
        fireCooldown -= Time.deltaTime;
        Enemy tgt = findBestTgt();
        if (tgt != null && fireCooldown <= 0f)
        {
            Shoot(tgt);
            fireCooldown = FireRate;
        }
        
    }
    Enemy findBestTgt()
    {
        Enemy[] enemies = GameObject.FindObjectsByType<Enemy>();
        Enemy best = null;
        float bestProgress = -1f;
        foreach(Enemy e in enemies)
        {
            float dist = Vector2.Distance(transform.position, e.transform.position);
            if(dist <= Range)
            {
                if(e.currentWayPoint > bestProgress)
                {
                    bestProgress = e.currentWayPoint;
                    best = e;
                }
            }
        }
        return best;
    }
    void Shoot(Enemy tgt)
    {
        GameObject p = Instantiate(projectileprefab, firepoint.position, Quaternion.identity);
        Projectile pr = p.GetComponent<Projectile>();
        pr.tgt = tgt.transform;
    }
}
