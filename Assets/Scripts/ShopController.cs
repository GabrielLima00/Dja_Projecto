using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopController : MonoBehaviour
{
    public List<CartaUI> cartasUI;
    public TextMeshProUGUI txtDinheiro;
    public SpecialCardsInventory inventarioCartas;
    public List<SpecialCard> cartasParaVenda;

    public GameObject prefabCarta;
    public Transform painelDeCartas;
    public int numCartasNaLoja = 3;
    public SpecialCard cartaSelecionada;

    void Start()
    {
        AtualizarDinheiroUI();
        MostrarCartasAleatorias();
    }

    public void AtualizarDinheiroUI()
    {
        txtDinheiro.text = "Dinheiro: $" + RunManager.dinheiro;
    }

    void MostrarCartasAleatorias()
    {

        foreach (Transform child in painelDeCartas)
        {
            Destroy(child.gameObject);
        }
        cartasUI.Clear();  

        for (int i = 0; i < numCartasNaLoja; i++)
        {
            int index = Random.Range(0, cartasParaVenda.Count);
            SpecialCard cartaEscolhida = cartasParaVenda[index];

            GameObject novaCarta = Instantiate(prefabCarta, painelDeCartas);
            CartaUI cartaUI = novaCarta.GetComponent<CartaUI>();
            RectTransform rt = novaCarta.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(i * 400, 0);
            if (cartaUI != null)
            {
                cartaUI.DefinirCarta(cartaEscolhida);
                cartaUI.shopController = this;

                cartasUI.Add(cartaUI);  
            }

            Debug.Log("Carta criada: " + cartaEscolhida.nome + " no índice " + i);
        }
    }

    public void ComprarCarta(SpecialCard cartaParaComprar)
    {
        if (RunManager.dinheiro >= cartaParaComprar.preco)
        {
            RunManager.dinheiro -= cartaParaComprar.preco;
            inventarioCartas.AdicionarCarta(cartaParaComprar); 

            AtualizarDinheiroUI();

            Debug.Log("Carta comprada: " + cartaParaComprar.nome);

            MostrarCartasAleatorias();
        }
        else
        {
            Debug.Log("Dinheiro insuficiente para comprar a carta.");
        }
    }

    public void SelecionarCarta(CartaUI cartaSelecionadaUI)
    {
        foreach (var cartaUI in cartasUI)
        {
            cartaUI.SetSelecionado(false); 
        }

        cartaSelecionadaUI.SetSelecionado(true); 

        cartaSelecionada = cartaSelecionadaUI.Carta; 
    }

    public void ComprarCartaSelecionada()
    {
        if (cartaSelecionada != null && RunManager.dinheiro >= cartaSelecionada.preco)
        {
            RunManager.dinheiro -= cartaSelecionada.preco;
            inventarioCartas.AdicionarCarta(cartaSelecionada);
            AtualizarDinheiroUI();
        }
    }

    public void UpgradeMultiplicador()
    {
        int custo = 100; 
        if (RunManager.dinheiro >= custo)
        {
            RunManager.dinheiro -= custo;
            PlayerStats.multiplicadorVitoria += 0.1f; 
            AtualizarDinheiroUI();
            Debug.Log("Upgrade comprado!");
        }
        else
        {
            Debug.Log("Dinheiro insuficiente para upgrade.");
        }
    }

    public void Continuar()
    {
        RunManager.AvancarDealer();
        SceneManager.LoadScene("GameScene");
    }
}
