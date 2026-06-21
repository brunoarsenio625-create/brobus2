using UnityEngine;

public class CairAoBater : MonoBehaviour
{
    [Header("For�a da queda")]
    public float forcaEmpurrao = 250f;

    [Header("Velocidade minima")]
    public float velocidadeMinima = 5f;

    private Rigidbody rb;
    private bool caiu = false;

    void Start()
    {
        // Garante que o Rigidbody existe para n�o dar erro na consola
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Configura��o inicial segura
        rb.isKinematic = true;
        rb.useGravity = true;
    }

    void Update()
    {
        // SEGUNDA SEGURAN�A: Se o poste atravessar o ch�o e cair no void, 
        // ele trava a queda numa altura m�nima (ajusta o -10f se o teu mapa for mais baixo)
        if (caiu && transform.position.y < -10f)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (caiu) return;

        // Verifica se foi o autocarro
        if (col.gameObject.name == "kozak_i_van")
        {
            float impacto = col.relativeVelocity.magnitude;

            if (impacto >= velocidadeMinima)
            {
                caiu = true;
                rb.isKinematic = false; // Ativa a gravidade f�sica

                Vector3 direcao = col.contacts[0].point - transform.position;
                rb.AddForce(-direcao.normalized * forcaEmpurrao);
                rb.AddTorque(Random.insideUnitSphere * 200f);

                Debug.Log("POSTE DERRUBADO!");
            }
        }
    }
}
