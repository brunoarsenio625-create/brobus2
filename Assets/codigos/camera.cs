using UnityEngine;

public class CameraFinal : MonoBehaviour
{
    public Transform alvo;
    public float distancia = 10f;
    public float altura = 2.5f;
    public float sensibilidade = 200f;
    public float suavidadeRotacao = 5f; // Ajuda a câmara a seguir a curva

    private float rotX = 0f;
    private float rotY = 0f;
    private bool shiftLockAtivo = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        // --- SISTEMA DE CLIQUE ---
        if (Input.GetMouseButtonDown(0))
        {
            shiftLockAtivo = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.Escape))
        {
            shiftLockAtivo = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // --- ROTAÇÃO ---
        if (shiftLockAtivo)
        {
            // Captura o movimento do rato
            rotY += Input.GetAxis("Mouse X") * sensibilidade * Time.deltaTime;
            rotX -= Input.GetAxis("Mouse Y") * sensibilidade * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, -5f, 50f);
        }

        // --- O SEGREDO: Somar a rotação do ALVO (Autocarro) ---
        // Isto faz com que a câmara acompanhe a traseira do carro quando ele curva
        Quaternion rotacaoAlvo = Quaternion.Euler(rotX, rotY + alvo.eulerAngles.y, 0);

        // --- POSICIONAMENTO ---
        Vector3 posicao = alvo.position - (rotacaoAlvo * Vector3.forward * distancia) + (Vector3.up * altura);

        transform.position = posicao;
        transform.LookAt(alvo.position + Vector3.up * 1.8f);
    }
}
