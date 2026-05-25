using UnityEngine;

public class Aluno : MonoBehaviour
{
    private bool autocarroPerto = false;
    private bool jaEntrou = false; // O TRINCO: Impede de contar 4 de uma vez!
    private MissaoManager gestorMissao;

    void Start()
    {
        // Procura a tua Lógica de Missão no autocarro automaticamente
        gestorMissao = FindFirstObjectByType<MissaoManager>();

        // Configura o colisor do aluno automaticamente
        BoxCollider bc = GetComponent<BoxCollider>();
        if (bc == null) bc = gameObject.AddComponent<BoxCollider>();
        bc.isTrigger = true;
        bc.size = new Vector3(4f, 2f, 4f); // Área de deteção
    }

    void Update()
    {
        // Só funciona se o autocarro estiver perto, clicares em ESPAÇO e se ainda NÃO entrou
        if (autocarroPerto && Input.GetKeyDown(KeyCode.Space) && !jaEntrou)
        {
            jaEntrou = true; // Tranca imediatamente para não repetir com as rodas!

            if (gestorMissao != null)
            {
                // Chama a tua função da missão para somar APENAS 1 aluno de cada vez
                gestorMissao.AdicionarAlunosDaParagem(1);
            }

            Destroy(gameObject); // O boneco desaparece instantaneamente do passeio
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se quem encostou foi o teu autocarro real
        if (other.gameObject.name == "kozak_i_van" && !jaEntrou)
        {
            autocarroPerto = true;

            // Muda o único texto do teu HUD
            if (gestorMissao != null && gestorMissao.textoHUD != null)
            {
                gestorMissao.textoHUD.text = "Carrega ESPAÇO para apanhar aluno";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "kozak_i_van" && !jaEntrou)
        {
            autocarroPerto = false;

            // Força a tua lógica de missão a restaurar o texto original dela (Tempo/Contador)
            if (gestorMissao != null)
            {
                gestorMissao.textoHUD.text = "Tempo: " + (int)gestorMissao.tempoLimite + "s | Procura os alunos!";
            }
        }
    }
}
