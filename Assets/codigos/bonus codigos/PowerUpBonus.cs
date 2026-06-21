using UnityEngine;

public class PowerUpBonus : BonusBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Autocarro")) return;

        Mover mover = other.GetComponent<Mover>();
        if (mover == null) mover = other.GetComponentInParent<Mover>();
        if (mover == null) mover = other.transform.root.GetComponentInChildren<Mover>();

        if (mover != null)
        {
            Debug.Log("POWER UP APANHADO!");
            StartCoroutine(AplicarVelocidade(mover));
            Desaparecer();
        }
        else
        {
            Debug.Log("NAO ENCONTROU MOVER!");
        }
    }

    System.Collections.IEnumerator AplicarVelocidade(Mover mover)
    {
        float original = mover.velocidadeMax;
        mover.velocidadeMax = original * 1.5f;
        Debug.Log("Velocidade aumentada para: " + mover.velocidadeMax);

        yield return new WaitForSeconds(15f);

        mover.velocidadeMax = original;
        Debug.Log("Velocidade voltou ao normal!");
    }
}