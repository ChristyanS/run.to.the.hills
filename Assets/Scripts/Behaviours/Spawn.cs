using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public List<GameObject> inimigosTerrestres;
    public List<GameObject> inimigosAereos;
    public Vector2 posicaoInimigosAereos;
    public Vector2 posicaoInimigosTerrestres;
    public float intervaloDeSpawn;

    private float contadorTempo;

    // Update is called once per frame
    private void FixedUpdate()
    {
        contadorTempo += Time.deltaTime;

        if (!(contadorTempo >= Fase.Instance.AjustaVelocidadeDiminui(intervaloDeSpawn))) return;

        contadorTempo = 0;

        if (Random.Range(1, 100) > 50)
            InstanciaInimigosTerrestres();
        else
            InstanciaInimigosAereos();
    }

    /// <summary>
    /// Responsável por instanciar os inimigos Terrestres
    /// </summary>
    public void InstanciaInimigosTerrestres()
    {
        InstanciaInimigo(inimigosTerrestres).transform.position = posicaoInimigosTerrestres;
    }

    /// <summary>
    /// Responsável por instanciar os inimigos Aereos
    /// </summary>
    public void InstanciaInimigosAereos()
    {
        InstanciaInimigo(inimigosAereos).transform.position = posicaoInimigosAereos;
    }

    /// <summary>
    /// Responsável por instanciar os inimigos
    /// </summary>
    private GameObject InstanciaInimigo(IReadOnlyList<GameObject> inimigos)
    {
        return Instantiate(inimigos[Random.Range(0, inimigos.Count)]);
    }
}