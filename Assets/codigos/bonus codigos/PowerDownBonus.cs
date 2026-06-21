using UnityEngine;

public class PowerDownBonus : BonusBase
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Autocarro")) return;

        Mover mover = other.GetComponent<Mover>();
        if (mover == null) mover = other.GetComponentInParent<Mover>();
        if (mover == null) mover = other.transform.root.GetComponentInChildren<Mover>();

        if (mover != null)
        {
            Debug.Log("POWER DOWN APANHADO!");
            StartCoroutine(AplicarPeso(mover));
            Desaparecer();
        }
        else
        {
            Debug.Log("NAO ENCONTROU MOVER!");
        }
    }

    System.Collections.IEnumerator AplicarPeso(Mover mover)
    {
        float velOriginal = mover.velocidadeMax;
        float acelOriginal = mover.aceleracao;
        float virarOriginal = mover.velocidadeVirar;

        mover.velocidadeMax = velOriginal * 0.5f;
        mover.aceleracao = acelOriginal * 0.3f;
        mover.velocidadeVirar = virarOriginal * 0.4f;
        Debug.Log("Autocarro ficou pesado!");

        yield return new WaitForSeconds(15f);

        mover.velocidadeMax = velOriginal;
        mover.aceleracao = acelOriginal;
        mover.velocidadeVirar = virarOriginal;
        Debug.Log("Autocarro voltou ao normal!");
    }
}