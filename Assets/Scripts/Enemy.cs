using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Atributos")]
    public float speed = 2f;
    public int health = 3;
    public int reward = 5;
    public int damage = 5;
    public Transform[] waypoints;
    public int currentWayPoint = 0;

    public static int AliveCount = 0;

    void OnEnable() { AliveCount++; }
    void OnDestroy() { AliveCount--; }

    [Header("Animacao")]
    public float frameRate = 0.15f;
    private float timer;
    private int frameIndex = 0;
    private SpriteRenderer spriteRenderer;

    [Header("Sprites por direcao na tela")]
    public Sprite[] walkRight;
    public Sprite[] walkUp;
    public Sprite[] walkDown;
    public Sprite[] walkLeft;

    [Header("Debug")]
    public bool logDirection = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;
        if (currentWayPoint >= waypoints.Length) return;

        Transform target = waypoints[currentWayPoint];
        Vector3 dir = (target.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (dir.sqrMagnitude > 0.0001f)
            UpdateAnimation(dir);

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length)
            {
                if (HealthManager.Instance != null)
                    HealthManager.Instance.UpdateHealth(-damage);

                Destroy(gameObject);
                return;
            }
        }
    }

    void UpdateAnimation(Vector3 worldDir)
    {
        Vector3 screenDir;
        if (Camera.main != null)
        {
            Vector3 p0 = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 p1 = Camera.main.WorldToScreenPoint(transform.position + worldDir);
            screenDir = p1 - p0;
        }
        else
        {
            screenDir = worldDir;
        }

        Sprite[] current;
        string nome;
        if (screenDir.y >= 0)
        {
            if (screenDir.x >= 0) { current = walkRight; nome = "Direita"; }
            else { current = walkUp; nome = "Cima"; }
        }
        else
        {
            if (screenDir.x >= 0) { current = walkDown; nome = "Baixo"; }
            else { current = walkLeft; nome = "Esquerda"; }
        }

        if (logDirection) Debug.Log($"Direcao: {nome}", this);

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer = 0f;
            frameIndex = (frameIndex + 1) % 2;
        }

        if (current != null && current.Length > frameIndex)
            spriteRenderer.sprite = current[frameIndex];
    }
}