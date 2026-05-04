using UnityEngine;

public class ControladorDiaNoite : MonoBehaviour
{
    public Material ceuDia;
    public Material ceuNoite;
    public Light luzSol; // Arraste a Directional Light aqui

    private bool eDia = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            eDia = !eDia;

            // Muda o céu e a luz do sol
            RenderSettings.skybox = eDia ? ceuDia : ceuNoite;
            luzSol.intensity = eDia ? 1f : 0.1f;

            // --- PROCURA OS FARÓIS ---
            // Isto procura todas as luzes que marcaste com a Tag "farois"
            GameObject[] farois = GameObject.FindGameObjectsWithTag("farois");

            foreach (GameObject farol in farois)
            {
                Light luz = farol.GetComponent<Light>();
                if (luz != null)
                {
                    luz.enabled = !eDia; // Liga se for noite, desliga se for dia
                }
            }

            DynamicGI.UpdateEnvironment(); // Atualiza a iluminação global
        }
    }
}
