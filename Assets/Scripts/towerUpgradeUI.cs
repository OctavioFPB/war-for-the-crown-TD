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


        if (Input.GetMouseButtonDown(0) && !PointerOverThisUI())
            Destroy(gameObject);

    }

    private static readonly System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> hits = new();

    bool PointerOverThisUI()
    {
        var data = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current)
        {
            position = Input.mousePosition
        };
        hits.Clear();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(data, hits);
        foreach (var h in hits)
            if (h.gameObject.transform.IsChildOf(transform)) return true;
        return false;
    }
}
