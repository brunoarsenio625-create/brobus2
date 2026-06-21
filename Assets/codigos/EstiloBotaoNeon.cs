using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EstiloBotaoNeon : MonoBehaviour
{
    [Header("Referências")]
    public Image bordaCiano;        // o Image do próprio Button
    public Image fundoInterior;     // o Image do FundoInterior
    public TextMeshProUGUI texto;   // o Text (TMP) com o "PLAY"

    [Header("Cores")]
    public Color corCiano = new Color(41f / 255f, 217f / 255f, 243f / 255f, 1f);       // borda
    public Color corFundoPreto = new Color(13f / 255f, 27f / 255f, 42f / 255f, 170f / 255f); // interior
    public Color corTexto = Color.white;

    void Start()
    {
        AplicarEstilo();
    }

    [ContextMenu("Aplicar Estilo Agora")]
    void AplicarEstilo()
    {
        if (bordaCiano != null)
            bordaCiano.color = corCiano;

        if (fundoInterior != null)
            fundoInterior.color = corFundoPreto;

        if (texto != null)
            texto.color = corTexto;
    }
}