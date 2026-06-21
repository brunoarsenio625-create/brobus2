using UnityEngine;

public class HealBonus : BonusBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Autocarro")) return;

        LogicaDoAutocarro vida = other.GetComponent<LogicaDoAutocarro>();
        if (vida == null) vida = other.GetComponentInParent<LogicaDoAutocarro>();

        if (vida != null)
        {
            vida.vidaAtual = vida.vidaMaxima;
            vida.barra.AtualizarBarra(vida.vidaAtual);
        }

        Desaparecer();
    }
}