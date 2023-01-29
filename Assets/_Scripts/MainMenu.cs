using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public bool fadeOut = false;

    public Image black; 
    public float fadeOutSpeed;
    public float counter = 0;

    private void Update()
    {
        if (fadeOut)
        {
            counter += fadeOutSpeed * Time.deltaTime;

            black.color = new Color(black.color.r, black.color.g, black.color.b, counter);
        }
        if (counter >= 1)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void StartGame()
    {
        fadeOut = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
