using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class logicamissao : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI textoHUD;

    [Header("Missão")]
    public float tempoMissao = 90f;

    [Header("Alunos")]
    public int totalAlunos = 9;

    [Header("Explosão")]
    public GameObject explosao;

    [Header("Autocarro")]
    public GameObject autocarro;

    private float tempoRestante;
    private int alunosApanhados = 0;
    private bool missaoComecou = false;
    private bool missaoFalhou = false;
    public bool missaoConcluida = false;
    private bool mostrarMensagemInicial = false;
    private bool alunoEncostado = false;

    void Start()
    {
        if (explosao != null)
            explosao.SetActive(false);

        textoHUD.text = "<color=yellow>Estás pronto? Carrega M para arrancar!</color>";
    }

    void Update()
    {
        if (!missaoComecou && Input.GetKeyDown(KeyCode.M))
            ComecarMissao();

        if (!missaoComecou || missaoFalhou || missaoConcluida)
            return;

        tempoRestante -= Time.deltaTime;

        if (tempoRestante <= 0)
        {
            FalharMissao();
            return;
        }

        if (!mostrarMensagemInicial)
            AtualizarHUD();
    }

    void ComecarMissao()
    {
        missaoComecou = true;
        tempoRestante = tempoMissao;
        mostrarMensagemInicial = true;
        textoHUD.text = "<color=white>Os alunos estão à tua espera! Vai buscá-los!</color>";
        Invoke(nameof(AtivarHUDNormal), 2f);
    }

    void AtivarHUDNormal()
    {
        mostrarMensagemInicial = false;
        AtualizarHUD();
    }

    void AtualizarHUD()
    {
        if (textoHUD == null) return;

        if (missaoConcluida)
        {
            textoHUD.text = "<color=green>muito bom😁😁 Apanhaste todos os alunos!</color>";
            return;
        }

        if (alunoEncostado)
        {
            textoHUD.text = "<color=white>Carrega espaço para o aluno entrar!</color>";
            return;
        }

        string barraAlunos = "";
        for (int i = 0; i < totalAlunos; i++)
        {
            if (i < alunosApanhados)
                barraAlunos += "<color=green>■</color>";
            else
                barraAlunos += "<color=grey>□</color>";
        }

        textoHUD.text =
            "<color=yellow>Tempo: " + Mathf.CeilToInt(tempoRestante) + "s</color>\n" +
            "<color=white>Alunos: " + barraAlunos + " " + alunosApanhados + "/" + totalAlunos + "</color>";
    }

    public bool ApanharAluno()
    {
        if (!PodeApanharAlunos()) return false;

        alunosApanhados++;

        if (alunosApanhados >= totalAlunos)
            ConcluirMissao();
        else
            AtualizarHUD();

        return true;
    }

    public void AvisarAutocarroAproximou(bool valor)
    {
        alunoEncostado = valor;
        if (!missaoFalhou && !missaoConcluida)
            AtualizarHUD();
    }

    public bool PodeApanharAlunos()
    {
        return missaoComecou && !missaoFalhou && !missaoConcluida;
    }

    public bool EhAutocarro(Collider other)
    {
        if (other == null) return false;
        return other.gameObject.name == "kozak_i_van" ||
               other.transform.root.name == "kozak_i_van";
    }

    void ConcluirMissao()
    {
        missaoConcluida = true;
        textoHUD.text = "<color=green>Boa! Apanhaste todos os alunos!</color>";
    }

    void FalharMissao()
    {
        missaoFalhou = true;

        textoHUD.text =
            "<color=red>O tempo acabou perdeste.</color>\n" +
            "<color=white>Não desistas, tenta outra vez!</color>";

        // Guarda o centro visual real do autocarro antes de o esconder
        Vector3 posicaoExplosao = Vector3.zero;

        if (autocarro != null)
        {
            posicaoExplosao = CentroVisual(autocarro);

            // Esconde o autocarro antes da explosão
            autocarro.SetActive(false);
        }

        // Mostra a explosão exatamente no centro do autocarro
        if (explosao != null)
        {
            explosao.transform.position = posicaoExplosao;
            explosao.SetActive(true);

            ParticleSystem[] particulas =
                explosao.GetComponentsInChildren<ParticleSystem>(true);

            foreach (ParticleSystem p in particulas)
            {
                p.Clear();
                p.Play();
            }
        }

        // Reinicia a cena após 2 segundos
        Invoke(nameof(ReiniciarJogo), 2f);
    }

    // Calcula o centro real do modelo 3D (usando os Renderers),
    // em vez de confiar no pivô do objeto, que pode estar desalinhado
    Vector3 CentroVisual(GameObject alvo)
    {
        Renderer[] renderers = alvo.GetComponentsInChildren<Renderer>();

        if (renderers.Length == 0)
            return alvo.transform.position;

        Bounds bounds = renderers[0].bounds;

        foreach (Renderer r in renderers)
        {
            bounds.Encapsulate(r.bounds);
        }

        return bounds.center;
    }

    void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}