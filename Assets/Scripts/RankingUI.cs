using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RankingUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI listText;
    public string menuSceneName = "Menu Principal";
    public string defaultDifficulty = "PrincipalGameSceneNormal";

    void Start()
    {
        Show(defaultDifficulty);
    }

    // Hooked to the Easy / Normal / Hard buttons (string = game scene name).
    public void Show(string sceneName)
    {
        if (titleText != null)
            titleText.text = "Ranking - " + sceneName.Replace("PrincipalGameScene", "");

        var list = RankingManager.Load(sceneName);
        var sb = new StringBuilder();

        for (int i = 0; i < list.Count; i++)
            sb.AppendLine((i + 1) + ".  " + list[i].name + "  -  " + list[i].score + "   (" + list[i].date + ")");

        listText.text = list.Count > 0 ? sb.ToString() : "No scores yet";
    }

    public void Back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}
