using UnityEngine;

public class mover : MonoBehaviour
{
    public float velocidadeMax = 15f;
    public float aceleracao = 10f;
    public float velocidadeVirar = 100f;

    private float velocidadeAtual = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Não capotar
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Física estável (SUPER IMPORTANTE)
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        rb.mass = 1500f;
        rb.linearDamping = 1.5f;
        rb.angularDamping = 3f;
    }

    void FixedUpdate()
    {
        float frente = Input.GetAxis("Vertical");     // W/S + Setas
        float virar = Input.GetAxis("Horizontal");    // A/D + Setas

        // 🚗 ACELERAÇÃO REAL
        if (Mathf.Abs(frente) > 0.1f)
        {
            velocidadeAtual += aceleracao * Time.fixedDeltaTime;
            velocidadeAtual = Mathf.Clamp(velocidadeAtual, 0, velocidadeMax);
        }
        else
        {
            // trava suave
            velocidadeAtual = Mathf.Lerp(velocidadeAtual, 0, 3f * Time.fixedDeltaTime);
        }

        // 🚗 MOVIMENTO COM FÍSICA (SEM BUGAR COLISÃO)
        Vector3 movimento = transform.forward * frente * velocidadeAtual;

        // substitui uso obsoleto de rb.velocity por rb.linearVelocity
        Vector3 currentLinear = rb.linearVelocity;
        rb.linearVelocity = new Vector3(
            movimento.x,
            currentLinear.y,
            movimento.z
        );

        // 🔄 ROTAÇÃO
        if (Mathf.Abs(virar) > 0.1f)
        {
            float rot = virar * velocidadeVirar * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rot, 0));
        }
    }
}