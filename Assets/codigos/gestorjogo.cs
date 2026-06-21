using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorJogo : MonoBehaviour
{
    public GameObject menuPrincipal;

    void Start()
    {
        Time.timeScale = 0f; // jogo parado
        menuPrincipal.SetActive(true);
    }

    public void Jogar()
    {
        Time.timeScale = 1f;
        menuPrincipal.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}