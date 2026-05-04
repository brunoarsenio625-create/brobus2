using UnityEngine;
using UnityEngine.UI;

public class BarraDeStress : MonoBehaviour
{
    public Slider slider;
    public Image fill; // arrasta o "Fill" aqui

    public void DefinirMaximo(float valor)
    {
        slider.maxValue = valor;
        slider.value = valor;

        AtualizarCor(valor);
    }

    public void AtualizarBarra(float valorAtual)
    {
        slider.value = valorAtual;
        AtualizarCor(valorAtual);
    }

    void AtualizarCor(float valor)
    {
        float percentagem = valor / slider.maxValue;

        // Cor dinâmica (verde → amarelo → vermelho)
        if (percentagem > 0.6f)
            fill.color = Color.green;
        else if (percentagem > 0.3f)
            fill.color = Color.yellow;
        else
            fill.color = Color.red;
    }
}