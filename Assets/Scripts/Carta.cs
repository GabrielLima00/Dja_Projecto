using UnityEngine;

[System.Serializable]
public class Carta
{
    public string nome;
    public int valor;
    public Sprite sprite; // opcional

    public Carta(string nome, int valor, Sprite sprite = null)
    {
        this.nome = nome;
        this.valor = valor;
        this.sprite = sprite;
    }
}
