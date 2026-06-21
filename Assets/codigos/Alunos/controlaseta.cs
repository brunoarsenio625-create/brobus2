using UnityEngine;

public class controlarseta : MonoBehaviour
{
    public GameObject seta;
    private logicamissao gestor;

    void Start()
    {
        gestor = FindFirstObjectByType<logicamissao>();
        if (seta != null)
            seta.SetActive(true); // Começa visível
    }

    void Update()
    {
        if (gestor == null) return;

        // Só esconde quando a missão CONCLUÍDA (todos apanhados)
        if (gestor.missaoConcluida)
            seta.SetActive(false);
        else
            seta.SetActive(true);
    }
}