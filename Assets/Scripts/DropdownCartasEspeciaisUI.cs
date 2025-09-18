using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventarioCartasEspeciais : MonoBehaviour
{
    public List<CartaEspecial> cartasEspeciais;
}

public class CartaEspecial
{
    public string nome;
}

public class DropdownCartasEspeciaisUI : MonoBehaviour
{
    public InventarioCartasEspeciais inventarioCartas;
    public Dropdown dropdown;

    void Start()
    {
        AtualizarDropdown();
    }

    public void AtualizarDropdown()
    {
        List<string> opcoes = new List<string>();
        foreach (var carta in inventarioCartas.cartasEspeciais)
        {
            opcoes.Add(carta.nome);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(opcoes);
    }
}