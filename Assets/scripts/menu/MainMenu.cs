using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayStart()
    {
        SceneManager.LoadScene("SampleScene");// в кавычках название сцены на которую осуществляется переход
    }

    public void ExitGame()
    {
        Debug.Log("idi nah");
        Application.Quit();
    }
}
