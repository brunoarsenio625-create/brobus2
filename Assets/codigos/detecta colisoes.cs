using UnityEngine;

public class LogicaDoAutocarro : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float vidaAtual;

    public BarraDeStress barra;

    void Start()
    {
        vidaAtual = vidaMaxima;
        barra.DefinirMaximo(vidaMaxima);
    }

    void OnCollisionEnter(Collision col)
    {
        // IGNORA O CHÃO (MUDA O NOME SE FOR DIFERENTE)
        if (col.gameObject.name == "Plane" || col.gameObject.name == "Estrada")
            return;

        // só conta pancadas reais (evita bug de encostar)
        if (col.relativeVelocity.magnitude > 2f)
        {
            vidaAtual -= 5f;

            if (vidaAtual < 0)
                vidaAtual = 0;

            barra.AtualizarBarra(vidaAtual);

            Debug.Log("Vida: " + vidaAtual);
        }
    }
}