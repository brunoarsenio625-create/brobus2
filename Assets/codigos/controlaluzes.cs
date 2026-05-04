using UnityEngine;

public class ControloLuzes : MonoBehaviour
{
    private bool luzesLigadas = false;

    void Update()
    {
        // Se clicares na tecla L
        if (Input.GetKeyDown(KeyCode.L))
        {
            luzesLigadas = !luzesLigadas; // Inverte entre ligado e desligado

            // Procura todas as luzes que tenham a Tag "Farois"
            GameObject[] todasAsLuzes = GameObject.FindGameObjectsWithTag("Farois");

            foreach (GameObject obj in todasAsLuzes)
            {
                Light luz = obj.GetComponent<Light>();
                if (luz != null)
                {
                    luz.enabled = luzesLigadas;
                }
            }

            Debug.Log("Luzes: " + (luzesLigadas ? "LIGADAS" : "DESLIGADAS"));
        }
    }
}
