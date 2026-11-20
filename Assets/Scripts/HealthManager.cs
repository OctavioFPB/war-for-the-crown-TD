using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    
    public static HealthManager Instance;
    public int health = 100;
    public TextMeshProUGUI healthTxt;
    private void Awake()
    {
        Instance = this;
        healthTxt.text = health.ToString();
    }

    public void UpdateHealth(int chaAmount)
    {
        health += chaAmount;
        healthTxt.text = health.ToString();
        
        if(health<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
