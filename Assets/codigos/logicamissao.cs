using UnityEngine;
using TMPro;
using System;

public class MissaoManager : MonoBehaviour
{
    [Header("Configurações de UI")]
    public TextMeshProUGUI textoHUD;

    [Header("Zonas do Jogo")]
    public GameObject zonaEscola;   // O quadrado verde (Zonaparagem)
    public GameObject grupoAlunos;  // O objeto pai que guarda os alunos na Hierarchy

    [Header("Configurações da Rota")]
    public float tempoLimite = 90f;

    private float tempoRestante;
    private int alunosTotaisNoMapa = 0;
    private int alunosApanhados = 0;
    private bool missaoAtiva = true;
    private bool autocarroNaEscola = false;
    private float cronometroEscola = 0f;

    void Start()
    {
        missaoAtiva = true;
        tempoRestante = tempoLimite;

        // A escola começa escondida até apanhares os alunos
        if (zonaEscola != null) zonaEscola.SetActive(false);

        // Conta quantos alunos puseste na rua dentro do grupo
        if (grupoAlunos != null) alunosTotaisNoMapa = grupoAlunos.transform.childCount;
        if (alunosTotaisNoMapa == 0) alunosTotaisNoMapa = 4; // Segurança

        textoHUD.text = "Tempo: " + (int)tempoRestante + "s | Alunos: 0 / " + alunosTotaisNoMapa;
    }

    void Update()
    {
        if (missaoAtiva)
        {
            tempoRestante -= Time.deltaTime;
            textoHUD.text = "Tempo: " + (int)tempoRestante + "s | Alunos: " + alunosApanhados + " / " + alunosTotaisNoMapa;

            if (tempoRestante <= 0) FalhouMissao();
        }

        // Lógica de entrega na Escola (Parte Final)
        if (!missaoAtiva && zonaEscola.activeSelf && autocarroNaEscola)
        {
            cronometroEscola += Time.deltaTime;
            textoHUD.text = "A deixar os alunos na escola... " + (int)(3f - cronometroEscola) + "s";

            if (cronometroEscola >= 3f)
            {
                textoHUD.text = "MISSÃO CONCLUÍDA!";
                zonaEscola.SetActive(false);
            }
        }
    }

    // Chamado pelo script do aluno quando usas o ESPAÇO
    public void RecolherAlunoIndividual()
    {
        if (missaoAtiva)
        {
            alunosApanhados++;

            // Mensagem de feedback gostoso que pediste!
            textoHUD.text = "Aluno recolhido! (" + alunosApanhados + "/" + alunosTotaisNoMapa + ")";

            if (alunosApanhados >= alunosTotaisNoMapa)
            {
                missaoAtiva = false;

                // ATIVA A PARTE FINAL DO TEU COMENTÁRIO
                textoHUD.text = "Vai para a parte final e leva à escola!";

                if (zonaEscola != null) zonaEscola.SetActive(true); // Faz aparecer o quadrado verde
                Debug.Log("Todos os alunos recolhidos. Escola ativada!");
            }
        }
    }

    void FalhouMissao()
    {
        missaoAtiva = false;
        textoHUD.text = "O TEMPO ESGOTOU! Missão Falhada.";
        // Adiciona aqui o teletransporte se quiseres
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Zonaparagem") autocarroNaEscola = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Zonaparagem") { autocarroNaEscola = false; cronometroEscola = 0f; }
    }

    internal bool MissaoAtiva()
    {
        throw new NotImplementedException();
    }

    internal void AdicionarAlunosDaParagem(int v)
    {
        throw new NotImplementedException();
    }
}
