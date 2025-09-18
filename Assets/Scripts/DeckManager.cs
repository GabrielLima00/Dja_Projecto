using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public List<Carta> cartasDisponiveis = new List<Carta>();
    private Queue<Carta> baralho;

    private string[] naipes = { "Copas", "Espadas", "Ouros", "Paus" };
    private string[] valores = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

    void Awake()
    {
        CriarBaralhoComSprites();
    }

    public void CriarBaralhoComSprites()
    {
        cartasDisponiveis.Clear();

        foreach (string naipe in naipes)
        {
            for (int i = 0; i < valores.Length; i++)
            {
                string valorTexto = valores[i];
                string nome = valorTexto + "de" + naipe;

                int valor;
                if (i == 0) valor = 1;            // Ás
                else if (i >= 10) valor = 10;     // J, Q, K
                else valor = i + 1;

                Sprite sprite = Resources.Load<Sprite>("Deck/" + nome);
                if (sprite == null)
                    Debug.LogWarning("Sprite não encontrado para: " + nome);

                Carta novaCarta = new Carta(nome, valor, sprite);
                cartasDisponiveis.Add(novaCarta);
            }
        }

        Debug.Log("Baralho criado com " + cartasDisponiveis.Count + " cartas.");
    }

    public void PrepararBaralho()
    {
        List<Carta> novoBaralho = new List<Carta>(cartasDisponiveis);
        novoBaralho = novoBaralho.OrderBy(x => Random.value).ToList();
        baralho = new Queue<Carta>(novoBaralho);
    }

    public Carta SacarCarta()
    {
        if (baralho == null || baralho.Count == 0)
        {
            Debug.LogWarning("O baralho está vazio!");
            return null;
        }
        return baralho.Dequeue();
    }
}
