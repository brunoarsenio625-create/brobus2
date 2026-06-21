using UnityEngine;
using System.Collections;

public class BonusBase : MonoBehaviour
{
    [Header("Respawn")]
    public float tempoRespawn = 20f;

    [Header("Flutuar")]
    public float altura = 0.3f;
    public float velocidadeFlutuar = 2f;

    private Vector3 posicaoInicial;

    protected virtual void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * velocidadeFlutuar) * altura;

        transform.position =
            new Vector3(
                posicaoInicial.x,
                posicaoInicial.y + y,
                posicaoInicial.z
            );
    }

    protected void Desaparecer()
    {
        StartCoroutine(Reaparecer());
    }

    IEnumerator Reaparecer()
    {
        gameObject.SetActive(false);

        yield return new WaitForSeconds(tempoRespawn);

        GameObject[] spawns =
            GameObject.FindGameObjectsWithTag("SpawnBonus");

        if (spawns.Length > 0)
        {
            Transform spawn =
                spawns[Random.Range(0, spawns.Length)].transform;

            transform.position = spawn.position;
            posicaoInicial = spawn.position;
        }

        gameObject.SetActive(true);
    }
}