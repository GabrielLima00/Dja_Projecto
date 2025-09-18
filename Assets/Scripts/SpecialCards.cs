using UnityEngine;

[CreateAssetMenu(fileName = "New Special Card", menuName = "Special Card")]
public class SpecialCard : ScriptableObject
{
    public string nome;
    public Sprite sprite;
    public int preco;
    public virtual void UsarCarta()
    {
        Debug.Log(nome + " foi usada!");
    }
}
