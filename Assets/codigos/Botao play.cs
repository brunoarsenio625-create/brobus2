using UnityEngine;

public class BotaoPlay : MonoBehaviour
{
    public GameObject canvasMenu;

    public void Jogar()
    {
        canvasMenu.SetActive(false);
    }
}