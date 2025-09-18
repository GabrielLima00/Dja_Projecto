using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("DealerMenu");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options"); // a Cena das opçoes deve ser criada no Unity depois
    }


    public void Exit()
    {
        Application.Quit(); 
    }
}
