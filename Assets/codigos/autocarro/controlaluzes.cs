using UnityEngine;

public class ControloLuzes : MonoBehaviour
{
    private bool luzesLigadas = false;

    void Start()
    {
        // 💡 O SEGREDO: Corre assim que dás Play e garante que tudo começa apagado
        luzesLigadas = false;
        DesligarTodasAsLuzesNoInicio();
    }

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

    // Função auxiliar que desliga tudo mal o jogo inicia
    void DesligarTodasAsLuzesNoInicio()
    {
        GameObject[] todasAsLuzes = GameObject.FindGameObjectsWithTag("Farois");

        foreach (GameObject obj in todasAsLuzes)
        {
            Light luz = obj.GetComponent<Light>();
            if (luz != null)
            {
                luz.enabled = false; // Desliga a luz à força no início
            }
        }
        Debug.Log("Sistema de Luzes: Faróis inicializados como DESLIGADOS.");
    }
}
