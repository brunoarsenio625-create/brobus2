using UnityEngine;

public class CairAoBater : MonoBehaviour
{
    [Header("Força da queda")]
    public float forcaEmpurrao = 250f;

    [Header("Velocidade minima")]
    public float velocidadeMinima = 5f;

    private Rigidbody rb;
    private bool caiu = false;

    void Start()
    {
        // Guarda o Rigidbody
        rb = GetComponent<Rigidbody>();

        // Segurança
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        // Evita cair duas vezes
        if (caiu)
            return;

        // Verifica se foi o autocarro
        if (col.gameObject.name == "kozak_i_van")
        {
            // Velocidade do impacto
            float impacto = col.relativeVelocity.magnitude;

            // Só cai se bater forte
            if (impacto >= velocidadeMinima)
            {
                caiu = true;

                // Liga física
                rb.isKinematic = false;

                // Direção do impacto
                Vector3 direcao = col.contacts[0].point - transform.position;

                // Faz cair realisticamente
                rb.AddForce(-direcao.normalized * forcaEmpurrao);

                // Faz rodar
                rb.AddTorque(Random.insideUnitSphere * 200f);

                Debug.Log("POSTE DERRUBADO!");
            }
        }
    }
}