using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartaUI : MonoBehaviour
{
    public Image imagemCarta;
    public TextMeshProUGUI textoNome;
    public TextMeshProUGUI textoPreco;
    public Button botaoComprar;
    public Outline outline; 

    private SpecialCard carta;
    public ShopController shopController;

    public SpecialCard Carta => carta;

    public void DefinirCarta(SpecialCard cartaParaMostrar)
    {
        carta = cartaParaMostrar;
        textoNome.text = carta.nome;
        textoPreco.text = "$" + carta.preco.ToString();
        imagemCarta.sprite = carta.sprite;

        if (carta.sprite != null)
            imagemCarta.color = Color.white;  
        else
            imagemCarta.color = new Color(1, 1, 1, 0);  

        botaoComprar.onClick.RemoveAllListeners();
        botaoComprar.onClick.AddListener(() => SelecionarCarta());

        SetSelecionado(false);
    }

    public void SelecionarCarta()
    {
        shopController.SelecionarCarta(this);
        Debug.Log("Carta selecionada: " + carta.nome);
    }

    public void SetSelecionado(bool selecionado)
    {
        if (outline != null)
            outline.enabled = selecionado;
    }
}
