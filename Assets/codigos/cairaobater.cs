using UnityEngine;

public class CairAoBater : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        // Verifica se quem bateu foi o autocarro
        if (col.gameObject.name == "kozak_i_van")
        {
            // Desliga o Kinematic para a física voltar a funcionar
            GetComponent<Rigidbody>().isKinematic = false;
            // Dá um pequeno "empurrão" extra para garantir que ele cai
            GetComponent<Rigidbody>().AddForce(col.relativeVelocity * 10f);
        }
    }
}
