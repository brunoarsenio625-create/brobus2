using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaDoAutocarro : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaAtual;

    public BarraDeStress barra;

    [Header("Explosão")]
    public GameObject explosao;

    [Header("Dano de colisão (bater de lado/frente)")]
    public float danoPorColisao = 10f;
    public float velocidadeMinimaColisao = 2f;

    [Header("Dano de queda")]
    public float velocidadeQuedaMinima = 8f;   // a partir desta velocidade vertical é que começa a doer
    public float multiplicadorDanoQueda = 2f;  // quanto mais forte a queda, mais dano

    [Header("Tags que NUNCA dão dano de colisão (estrada/chão)")]
    public string[] tagsSemDano = { "Estrada", "Chao" };

    private Rigidbody rb;
    private float velocidadeQuedaAnterior = 0f;

    void Start()
    {
        vidaAtual = vidaMaxima;
        barra.DefinirMaximo(vidaMaxima);

        rb = GetComponent<Rigidbody>();

        if (explosao != null)
            explosao.SetActive(false);
    }

    void FixedUpdate()
    {
        // Guarda a velocidade de queda antes da física resolver a colisão,
        // para sabermos com que força batemos no chão quando aterrarmos
        if (rb != null && rb.linearVelocity.y < 0f)
        {
            velocidadeQuedaAnterior = Mathf.Abs(rb.linearVelocity.y);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // IGNORA OS BÓNUS
        if (col.gameObject.CompareTag("Bonus"))
            return;

        bool eEstrada = EhEstradaOuChao(col.gameObject);

        // --- DANO DE QUEDA: acontece em qualquer superfície, mesmo na estrada ---
        if (velocidadeQuedaAnterior >= velocidadeQuedaMinima)
        {
            float danoQueda = (velocidadeQuedaAnterior - velocidadeQuedaMinima) * multiplicadorDanoQueda;
            AplicarDano(danoQueda);
            Debug.Log("Dano de queda: " + danoQueda);
        }

        velocidadeQuedaAnterior = 0f; // reset depois de aterrar

        // --- DANO DE COLISÃO NORMAL: nunca acontece em estradas/chão ---
        if (eEstrada)
            return;

        if (col.relativeVelocity.magnitude > velocidadeMinimaColisao)
        {
            AplicarDano(danoPorColisao);
            Debug.Log("Dano de colisão: " + danoPorColisao);
        }
    }

    bool EhEstradaOuChao(GameObject obj)
    {
        foreach (string tag in tagsSemDano)
        {
            if (obj.CompareTag(tag))
                return true;
        }

        // Segurança extra: cobre os nomes dos teus prefabs de estrada,
        // mesmo que ainda não tenhas posto a tag certa em todos
        string nome = obj.name.ToLower();
        return nome.Contains("street") || nome.Contains("sideway") ||
               nome.Contains("road") || nome.Contains("estrada") || nome.Contains("plane");
    }

    void AplicarDano(float quantidade)
    {
        vidaAtual -= quantidade;

        if (vidaAtual < 0)
            vidaAtual = 0;

        barra.AtualizarBarra(vidaAtual);

        if (vidaAtual <= 0)
        {
            Explodir();
        }
    }

    void Explodir()
    {
        if (explosao != null)
        {
            explosao.transform.position = CentroVisual(gameObject);
            explosao.SetActive(true);

            ParticleSystem[] particulas =
                explosao.GetComponentsInChildren<ParticleSystem>(true);

            foreach (ParticleSystem p in particulas)
            {
                p.Clear();
                p.Play();
            }
        }

        gameObject.SetActive(false);

        Invoke(nameof(ReiniciarJogo), 2f);
    }

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