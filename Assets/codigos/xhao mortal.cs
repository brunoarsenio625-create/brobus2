using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaoMortal : MonoBehaviour
{
    [Header("Reiniciar jogo (se não houver lógica de explosão)")]
    public float tempoAteReiniciar = 1f;

    // Cobre objetos com Collider normal (física real, ex: autocarro a cair)
    void OnCollisionEnter(Collision col)
    {
        ProcessarQueda(col.gameObject);
    }

    // Cobre objetos com Collider em modo Trigger (ex: alunos, se não tiverem física real)
    void OnTriggerEnter(Collider other)
    {
        ProcessarQueda(other.gameObject);
    }

    void ProcessarQueda(GameObject objQueCaiu)
    {
        // Se foi o autocarro a cair fora do mapa
        if (objQueCaiu.name == "kozak_i_van" || objQueCaiu.transform.root.name == "kozak_i_van")
        {
            MorrerAutocarro(objQueCaiu.transform.root.gameObject);
            return;
        }

        // Se foi um aluno a cair fora do mapa
        aluno alunoScript = objQueCaiu.GetComponent<aluno>();
        if (alunoScript != null)
        {
            Debug.Log("Aluno caiu do mapa e foi removido.");
            Destroy(objQueCaiu);
            return;
        }
    }

    void MorrerAutocarro(GameObject autocarroObj)
    {
        LogicaDoAutocarro logica = autocarroObj.GetComponent<LogicaDoAutocarro>();

        if (logica != null)
        {
            // Leva a vida a zero; o LogicaDoAutocarro já trata da explosão e do reinício sozinho
            logica.vidaAtual = 0;
            logica.SendMessage("Explodir", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            // Segurança: se por algum motivo não encontrar o script de vida, reinicia na mesma
            Invoke(nameof(ReiniciarJogo), tempoAteReiniciar);
        }
    }

    void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}