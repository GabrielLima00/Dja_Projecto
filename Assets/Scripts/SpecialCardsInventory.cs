using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialCardsInventory", menuName = "Inventario Cartas Especiais")]
public class SpecialCardsInventory : ScriptableObject
{
    public List<SpecialCard> cartasEspeciais = new List<SpecialCard>();

    public void UsarCarta(SpecialCard carta)
    {
        if (cartasEspeciais.Contains(carta))
        {
            carta.UsarCarta();
            cartasEspeciais.Remove(carta);
        }
    }

    public void AdicionarCarta(SpecialCard carta)
    {
        cartasEspeciais.Add(carta);
    }
}
