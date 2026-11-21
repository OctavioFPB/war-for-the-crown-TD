using UnityEngine;
using UnityEngine.UI;



public class SpeedManager : MonoBehaviour
{

    public bool speedUP = false;
    public Image buttonImage;

    public void ToggleSpeedUP()
    {
        speedUP = !speedUP;
        Color c = buttonImage.color;

        if (speedUP)
        {
            Time.timeScale = 2;
            c.a = 0.5f;
        } else
        {
            Time.timeScale = 1;
            c.a = 0.9f;
        }

        buttonImage.color = c;
    }

}
