using UnityEngine;

public class Tower : MonoBehaviour
{
    public float FireRate = 1f;
    public float Range = 3f;
    public GameObject projectileprefab;
    public Transform firepoint;
    public int towerPrice = 1;
    public static float YRatio = 1f;


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
            Vector2 d = e.transform.position - transform.position;
            d.y /= YRatio;
            float dist = d.magnitude;
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
