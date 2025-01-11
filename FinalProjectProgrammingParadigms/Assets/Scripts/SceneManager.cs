using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ChangeScene(string nombre)
    {
        if (nombre == "MainMenu" && UIManager.instance != null)
        {
            Destroy(UIManager.instance.gameObject);
        }
        SceneManager.LoadScene(nombre);
    }

    public void RestartGame()
    {
        if (UIManager.instance != null)
        {
            Destroy(UIManager.instance.gameObject);
        }
    }
}
