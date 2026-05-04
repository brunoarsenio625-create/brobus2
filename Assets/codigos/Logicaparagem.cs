using UnityEngine;

public class LogicaParagem : MonoBehaviour
{
    public float tempoNecessario = 3f;
    private float cronometro = 0f;
    private bool autocarroEstaAqui = false;

    void OnTriggerStay(Collider other)
    {
        // Verifica se é o autocarro que está na paragem
        if (other.gameObject.name == "kozak_i_van")
        {
            autocarroEstaAqui = true;
            cronometro += Time.deltaTime;

            if (cronometro >= tempoNecessario)
            {
                RecolherAlunos();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "kozak_i_van")
        {
            autocarroEstaAqui = false;
            cronometro = 0f; // Reset se sair antes do tempo
        }
    }

    void RecolherAlunos()
    {
        Debug.Log("Alunos recolhidos com sucesso!");
        cronometro = -999f; // Impede de ganhar pontos infinitos na mesma paragem
        // Aqui podemos apagar os bonecos da paragem depois
        gameObject.SetActive(false); // A paragem desaparece ou muda de cor
    }
}
