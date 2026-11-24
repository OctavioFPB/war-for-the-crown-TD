using UnityEngine;
using UnityEngine.SceneManagement;

public class EscToMenu : MonoBehaviour
{
    const string TargetScene = "Menu Principal";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void install()
    {
        if (FindAnyObjectByType<EscToMenu>() != null) return;
        var go = new GameObject("EscToMenu");
        go.AddComponent<EscToMenu>();
        DontDestroyOnLoad(go);
    }

    void update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (SceneManager.GetActiveScene().name == TargetScene) return;

        Time.timeScale = 1f;
        SceneManager.LoadScene(TargetScene);
    }
}
