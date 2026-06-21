using UnityEngine;

public class setamissao : MonoBehaviour
{
    public Transform autocarro;
    public Transform[] alunos;

    [Header("Comportamento da seta")]
    public float velocidadeRotacao = 6f;        // quanto maior, mais rápido a seta vira
    public float intervaloAtualizacao = 2f;     // de quanto em quanto tempo a seta "decide" o alvo (segundos)
    public float raioDeteccao = 30f;            // distância máxima a que a seta consegue "ver" um aluno

    private Transform alvoAtual;
    private Renderer[] renderers;
    private float proximaAtualizacao = 0f;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        if (autocarro != null)
        {
            transform.position = new Vector3(
                autocarro.position.x,
                autocarro.position.y + 5f,
                autocarro.position.z
            );
        }

        // Só decide um novo alvo de tempos a tempos, não a cada frame
        if (Time.time >= proximaAtualizacao)
        {
            AtualizarAlvo();
            proximaAtualizacao = Time.time + intervaloAtualizacao;
        }

        // Se o alvo escolhido já foi apanhado entretanto, força nova procura imediata
        if (alvoAtual == null)
        {
            AtualizarAlvo();
        }

        if (alvoAtual == null)
        {
            MostrarSeta(false);
            return;
        }

        MostrarSeta(true);

        Vector3 direcao = alvoAtual.position - transform.position;
        direcao.y = 0;

        if (direcao.magnitude > 0.1f)
        {
            Quaternion rotacaoDesejada = Quaternion.LookRotation(direcao) * Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacaoDesejada, Time.deltaTime * velocidadeRotacao);
        }
    }

    void AtualizarAlvo()
    {
        Transform candidato = EncontrarMaisProximoDentroDoRaio();
        alvoAtual = candidato;
    }

    Transform EncontrarMaisProximoDentroDoRaio()
    {
        Transform maisProximo = null;
        float menorDistancia = Mathf.Infinity;

        foreach (Transform aluno in alunos)
        {
            if (aluno == null) continue;

            float distancia = Vector3.Distance(transform.position, aluno.position);

            // Ignora alunos que estão fora do raio de deteção
            if (distancia > raioDeteccao) continue;

            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                maisProximo = aluno;
            }
        }

        // Se nenhum aluno está dentro do raio, aponta para o mais próximo de TODOS
        // (assim a seta nunca fica completamente "perdida" sem direção nenhuma)
        if (maisProximo == null)
        {
            foreach (Transform aluno in alunos)
            {
                if (aluno == null) continue;

                float distancia = Vector3.Distance(transform.position, aluno.position);
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    maisProximo = aluno;
                }
            }
        }

        return maisProximo;
    }

    void MostrarSeta(bool mostrar)
    {
        foreach (Renderer r in renderers)
        {
            r.enabled = mostrar;
        }
    }
}