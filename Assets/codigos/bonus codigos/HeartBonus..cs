using UnityEngine;

public class HeartBonus : BonusBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Autocarro")) return;

        LogicaDoAutocarro vida = other.GetComponent<LogicaDoAutocarro>();
        if (vida == null) vida = other.GetComponentInParent<LogicaDoAutocarro>();

        if (vida != null)
        {
            vida.vidaAtual += 25f;
            if (vida.vidaAtual > vida.vidaMaxima) vida.vidaAtual = vida.vidaMaxima;
            vida.barra.AtualizarBarra(vida.vidaAtual);
        }

        Desaparecer();
    }
}