using UnityEngine;

public class Mover : MonoBehaviour
{
    public float velocidadeMax = 15f;
    public float aceleracao = 10f;
    public float velocidadeVirar = 100f;

    private float velocidadeAtual = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // CONFIGURAÇÃO ANT-ATRAVESSAR
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        // ContinuousDynamic é o modo mais forte para não atravessar paredes
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // Peso de um autocarro real para não ser empurrado por bugs
        rb.mass = 2000f;
        rb.linearDamping = 2f;
        rb.angularDamping = 5f;

        // Impede de capotar mas permite girar no Y
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float frente = Input.GetAxis("Vertical");
        float virar = Input.GetAxis("Horizontal");

        // 1. LÓGICA DE ACELERAÇÃO
        if (Mathf.Abs(frente) > 0.1f)
        {
            velocidadeAtual += aceleracao * Time.fixedDeltaTime;
        }
        else
        {
            velocidadeAtual = Mathf.MoveTowards(velocidadeAtual, 0, aceleracao * Time.fixedDeltaTime * 2f);
        }
        velocidadeAtual = Mathf.Clamp(velocidadeAtual, 0, velocidadeMax);

        // 2. MOVIMENTO USANDO FORÇA (FORÇA É MELHOR QUE VELOCIDADE DIRETA PARA NÃO ATRAVESSAR)
        Vector3 direcaoDesejada = transform.forward * frente * velocidadeAtual;

        // Mantemos o Y da gravidade mas aplicamos a força no X e Z
        Vector3 novaVelocidade = new Vector3(direcaoDesejada.x, rb.linearVelocity.y, direcaoDesejada.z);
        rb.linearVelocity = novaVelocidade;

        // 3. ROTAÇÃO SUAVE
        if (Mathf.Abs(virar) > 0.1f && velocidadeAtual > 0.1f)
        {
            // O autocarro só vira se estiver em movimento (mais realista)
            float fatorVelocidade = Mathf.Clamp01(velocidadeAtual / 5f);
            float rot = virar * velocidadeVirar * Time.fixedDeltaTime * fatorVelocidade;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rot, 0));
        }
    }
}
