using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
public class towerUpgradeUI : MonoBehaviour
{
    public Tower tower;
    public TextMeshProUGUI priceTxt;
    private bool fresh = true;
    public Button upgradeButton;


    private void Awake()
    {
        upgradeButton.onClick.AddListener(TryUpgrade);
    }

    private void TryUpgrade()
    {
        if (CoinManager.instance.coins < tower.upgradeStages[tower.upgradeStage].price) return;
        tower.upgrade();
        Destroy(gameObject);

    }


    void Update()
    {
        if (fresh)
        {
            fresh = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Destroy(gameObject);
        }
        
    }
 }
