using UnityEngine;

public class aluno : MonoBehaviour
{
    private logicamissao gestor;
    private bool autocarroAproximou = false;
    private bool jaFoiApanhado = false;

    void Start()
    {
        gestor = FindFirstObjectByType<logicamissao>();

        // Cria a área de aproximação ao redor do aluno
        Collider col = GetComponent<Collider>();
        if (col == null)
        {
            BoxCollider box = gameObject.AddComponent<BoxCollider>();
            col = box;
        }
        col.isTrigger = true;
    }

    void Update()
    {
        if (gestor == null || jaFoiApanhado) return;
        if (!gestor.PodeApanharAlunos()) return;

        // Se o autocarro se aproximou e o jogador carregou no ESPAÇO
        if (autocarroAproximou && Input.GetKeyDown(KeyCode.Space))
        {
            jaFoiApanhado = true;
            gestor.AvisarAutocarroAproximou(false); // Remove o texto do ecrã

            if (gestor.ApanharAluno())
            {
                Destroy(gameObject); // O aluno entra no autocarro e some do mapa
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Só ativa o aviso quando o autocarro entra na área (se aproxima e encosta)
        if (gestor != null && gestor.EhAutocarro(other))
        {
            autocarroAproximou = true;
            gestor.AvisarAutocarroAproximou(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Remove o aviso assim que o autocarro se afasta do aluno
        if (gestor != null && gestor.EhAutocarro(other))
        {
            autocarroAproximou = false;
            gestor.AvisarAutocarroAproximou(false);
        }
    }
}
