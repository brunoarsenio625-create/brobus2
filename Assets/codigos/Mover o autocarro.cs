
using UnityEngine;

public class mover : MonoBehaviour
{
    public float velocidade = 15f;
    public float velocidadeVirar = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Não deixa o autocarro tombar
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // IMPORTANTE para colisões funcionarem bem
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void FixedUpdate()
    {
        float frente = Input.GetAxis("Vertical"); // W/S

        // Movimento usando física (correto)
        Vector3 movimento = transform.forward * frente * velocidade;
        rb.linearVelocity = new Vector3(movimento.x, rb.linearVelocity.y, movimento.z);

        // Travar quando não há input
        if (Mathf.Abs(frente) < 0.1f)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        // Virar
        float virar = Input.GetAxis("Horizontal"); // A/D

        if (Mathf.Abs(virar) > 0.1f)
        {
            Quaternion rotacao = Quaternion.Euler(0, virar * velocidadeVirar * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * rotacao);
        }
    }
}