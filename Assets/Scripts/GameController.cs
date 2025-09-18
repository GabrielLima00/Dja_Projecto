using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 
using UnityEngine.UI;             


public class GameController : MonoBehaviour
{
    public GameObject pausePanel;
    private bool jogoPausado = false;
    public List<Sprite> dealerImages; 
    public Image dealerImage;         
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            jogoPausado = !jogoPausado;
            pausePanel.SetActive(jogoPausado);

            Time.timeScale = jogoPausado ? 0 : 1;
        }
    }

    public void Menu()
    {
        Time.timeScale = 1; // despausa antes de trocar de cena
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
