using UnityEngine;

[System.Serializable]

public class towerUpgradeLevel
{
    public float range;
    public float fireRate;
    public int price;
}

public class Tower : MonoBehaviour
{
    public towerUpgradeLevel[] upgradeStages;
    public int upgradeStage;
    private SpriteRenderer sr;
    public GameObject towerUpgradeUIPrefab;
    private GameObject currentUI; 

    public float FireRate = 1f;
    public float Range = 3f;
    public GameObject projectileprefab;
    public Transform firepoint;
    public int towerPrice = 1;
    public static float YRatio = 1f;


    private float fireCooldown = 0f;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

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
    public void upgrade()
    {
        towerUpgradeLevel currentUpgradeStage = upgradeStages[upgradeStage];
        Range = currentUpgradeStage.range;
        FireRate = currentUpgradeStage.fireRate;
        CoinManager.instance.UpdateCoins(-currentUpgradeStage.price);
        upgradeStage += 1;
    }
    private void OnMouseDown()
    {
        if(currentUI == null)
        {
            currentUI = Instantiate(towerUpgradeUIPrefab, Object.FindAnyObjectByType<Canvas>().transform);
        }
        towerUpgradeUI currentUpgradeUI = currentUI.GetComponent<towerUpgradeUI>();
        currentUpgradeUI.tower = this;

        currentUI.transform.position = Input.mousePosition + new Vector3(50, -50);
        if (upgradeStage >= upgradeStages.Length) return;
        currentUpgradeUI.priceTxt.text = upgradeStages[upgradeStage].price.ToString();
    }
}
