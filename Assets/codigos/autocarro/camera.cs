using UnityEngine;

public class CameraFinal : MonoBehaviour
{
    public Transform alvo;
    public float distancia = 10f;
    public float altura = 2.5f;
    public float sensibilidade = 200f;
    public float suavidadeRotacao = 5f;

    private float rotX = 0f;
    private float rotY = 0f;
    private bool shiftLockAtivo = false; // começa destravado

    void LateUpdate()
    {
        if (alvo == null) return;

        // --- ALT LIGA E DESLIGA O TRAVAMENTO DO CURSOR ---
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            shiftLockAtivo = !shiftLockAtivo; // inverte o estado atual

            Cursor.lockState = shiftLockAtivo ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !shiftLockAtivo;
        }

        // --- ROTAÇÃO (só funciona se o cursor estiver travado) ---
        if (shiftLockAtivo)
        {
            rotY += Input.GetAxis("Mouse X") * sensibilidade * Time.deltaTime;
            rotX -= Input.GetAxis("Mouse Y") * sensibilidade * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, -5f, 50f);
        }

        // --- POSICIONAMENTO ---
        Quaternion rotacaoAlvo = Quaternion.Euler(rotX, rotY + alvo.eulerAngles.y, 0);
        Vector3 posicao = alvo.position - (rotacaoAlvo * Vector3.forward * distancia) + (Vector3.up * altura);

        transform.position = posicao;
        transform.LookAt(alvo.position + Vector3.up * 1.8f);
    }
}