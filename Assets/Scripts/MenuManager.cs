using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    static public string diffLevel = "PrincipalGameSceneNormal";
    
    public void updateDiff(string input)
    {
        diffLevel = input;
    }

    public void LoadStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(diffLevel);
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
        
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
