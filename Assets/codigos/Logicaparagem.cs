using UnityEngine;

public class LogicaParagem : MonoBehaviour
{
    public float tempoNecessario = 3f;

    private float cronometro = 0f;
    private bool recolhido = false;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "kozak_i_van" && !recolhido)
        {
            // Procura o gestor da missão
            MissaoManager gestor = FindFirstObjectByType<MissaoManager>();

            // Se a missão acabou, não faz nada
            if (gestor == null || !gestor.MissaoAtiva())
            {
                return;
            }

            cronometro += Time.deltaTime;

            // Mensagem enquanto espera
            gestor.textoHUD.text =
                "A apanhar alunos... " +
                Mathf.Ceil(tempoNecessario - cronometro) + "s";

            if (cronometro >= tempoNecessario)
            {
                RecolherAlunos();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "kozak_i_van" && !recolhido)
        {
            cronometro = 0f;

            MissaoManager gestor = FindFirstObjectByType<MissaoManager>();

            if (gestor != null && gestor.MissaoAtiva())
            {
                gestor.textoHUD.text =
                    "Nao apanhaste os alunos!";
            }
        }
    }

    void RecolherAlunos()
    {
        recolhido = true;

        Debug.Log("Alunos recolhidos!");

        MissaoManager gestor = FindFirstObjectByType<MissaoManager>();

        if (gestor != null)
        {
            gestor.AdicionarAlunosDaParagem(5);
        }

        gameObject.SetActive(false);
    }
}