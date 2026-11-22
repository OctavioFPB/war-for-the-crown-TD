using UnityEngine;
using System.Collections;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    public int coins;
    public TextMeshProUGUI coinTxt;

    public int coinsPerTick = 2;
    public float tickInterval = 3f;

    private void Awake()
    {
        instance = this;
        UpdateCoins(0);
    }

    private void Start()
    {
        StartCoroutine(GeneratePassiveIncome());
    }

    public void UpdateCoins(int changeAmount)
    {
        coins += changeAmount;
        coinTxt.text = coins.ToString();
    }

    private IEnumerator GeneratePassiveIncome()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickInterval);
            
            UpdateCoins(coinsPerTick);
        }
    }
}