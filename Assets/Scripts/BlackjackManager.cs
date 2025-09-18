using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BlackjackManager : MonoBehaviour
{
    public DeckManager deckManager;
    public TextMeshProUGUI txtDinheiro;
    public TextMeshProUGUI txtBid;
    public Transform cartasDealerContainer;
    public Transform cartasJogadorContainer;

    public GameObject cartaPrefab; 
    public Sprite spriteCartaVirada; 

    public TextMeshProUGUI txtDealerPontos;
    public TextMeshProUGUI txtJogadorPontos;
    public TextMeshProUGUI txtResultado;

    public Button btnHit, btnStand;

    private List<Carta> cartasDealer = new List<Carta>();
    private List<Carta> cartasJogador = new List<Carta>();

    private bool jogoTerminado = false;
    private int pontuacaoLimite = 21;
    private bool dealerBlackjackEntre19e21 = false;

    void Start()
    {
        btnHit.onClick.AddListener(Hit);
        btnStand.onClick.AddListener(Stand);
        btnHit.interactable = true;
        btnStand.interactable = true;
        IniciarJogo();
    }

    public void IniciarJogo()
    {
        jogoTerminado = false;
        txtResultado.text = "";
        LimparMesa();
        AtualizarDinheiroUI();
        AtualizarBidUI();
        cartasDealer.Clear();
        cartasJogador.Clear();

        deckManager.PrepararBaralho();

        if (DealerInfo.dealerSelecionado != null)
        {
            pontuacaoLimite = DealerInfo.dealerSelecionado.pontuacaoMaxima;
            dealerBlackjackEntre19e21 = DealerInfo.dealerSelecionado.dealerBlackjackEntre19e21;
        }

        for (int i = 0; i < 2; i++)
        {
            var carta = deckManager.SacarCarta();
            if (carta != null)
                cartasJogador.Add(carta);
        }
        for (int i = 0; i < 2; i++)
        {
            var carta = deckManager.SacarCarta();
            if (carta != null)
                cartasDealer.Add(carta);
        }

        AtualizarInterface(inicio: true);
    }

    void AtualizarBidUI()
    {
        if (txtBid != null)
        {
            int objetivo = RunManager.GetDinheiroMinimoPorDealer();
            txtBid.text = "Objetivo: $" + objetivo;
        }
    }

    void AtualizarDinheiroUI()
    {
        if (txtDinheiro != null)
            txtDinheiro.text = "Dinheiro: $" + RunManager.dinheiro;
    }

    private bool bloqueioDeDebug = false;

    public void Hit()
    {
        if (bloqueioDeDebug)
        {
            Debug.Log("Segunda chamada ignorada");
            return;
        }

        bloqueioDeDebug = true;
        Debug.Log("Hit clicado");

        if (jogoTerminado) return;

        var carta = deckManager.SacarCarta();
        if (carta != null)
            cartasJogador.Add(carta);
        else
            Debug.LogWarning("Tentaste sacar carta mas o baralho está vazio!");

        AtualizarInterface();

        int total = CalcularTotal(cartasJogador);
        Debug.Log("Pontuação do jogador: " + total);

        if (total > pontuacaoLimite)
            TerminarJogo("Ficaste acima de " + pontuacaoLimite + "! Perdeste!");

        Invoke(nameof(DesbloquearDebug), 0.2f);
    }

    void DesbloquearDebug()
    {
        bloqueioDeDebug = false;
    }

    public void Stand()
    {
        if (jogoTerminado) return;

        while (CalcularTotal(cartasDealer) < pontuacaoLimite - 4)
        {
            var carta = deckManager.SacarCarta();
            if (carta != null)
                cartasDealer.Add(carta);
            else
                break;
        }

        AtualizarInterface();

        bool dealerTemBlackjack = false;
        bool jogadorTemBlackjack = CalcularTotal(cartasJogador) == 21;
        int totalJogador = CalcularTotal(cartasJogador);
        int totalDealer = CalcularTotal(cartasDealer);

        if (dealerBlackjackEntre19e21 && totalDealer >= 19 && totalDealer <= 21)
        {
            dealerTemBlackjack = true;
        }

        if (dealerTemBlackjack)
        {
            if (jogadorTemBlackjack)
                TerminarJogo("Ambos fizeram Blackjack! Empate!");
            else
                TerminarJogo("O dealer fez Blackjack entre 19-21! Perdeste!");
        }
        else
        {
            if (totalDealer > pontuacaoLimite || totalJogador > totalDealer)
                TerminarJogo("Ganhaste!");
            else if (totalJogador == totalDealer)
                TerminarJogo("Empate!");
            else
                TerminarJogo("Perdeste!");
        }
    }

    void TerminarJogo(string mensagem)
    {
        btnHit.interactable = false;
        btnStand.interactable = false;
        jogoTerminado = true;
        AtualizarInterface();
        txtResultado.text = mensagem;

        if (mensagem.ToLower().Contains("ganhaste"))
            Invoke(nameof(TratarVitoria), 2f);
        else
            Invoke(nameof(TratarDerrota), 2f);
    }

    void TratarVitoria()
    {
        RunManager.Vitoria();
    }

    void TratarDerrota()
    {
        RunManager.Derrota();
    }

    int CalcularTotal(List<Carta> cartas)
    {
        int total = 0;
        int ases = 0;

        foreach (var carta in cartas)
        {
            if (carta == null) continue;
            total += carta.valor;
            if (carta.valor == 1)
                ases++;
        }

        while (total <= 11 && ases > 0)
        {
            total += 10;
            ases--;
        }

        return total;
    }

    void AtualizarInterface(bool inicio = false)
{
    txtJogadorPontos.text = "Tu: " + CalcularTotal(cartasJogador);

        if (!jogoTerminado)
        {
            if (cartasDealer.Count > 0 && cartasDealer[0] != null)
                txtDealerPontos.text = "Dealer: " + cartasDealer[0].valor;
            else
                txtDealerPontos.text = "Dealer: ?";

            AtualizarCartasUI(cartasDealer, cartasDealerContainer, esconderSegunda: true);
        }
        else
        {
            txtDealerPontos.text = "Dealer: " + CalcularTotal(cartasDealer);
            AtualizarCartasUI(cartasDealer, cartasDealerContainer, esconderSegunda: false);
        }

        AtualizarCartasUI(cartasJogador, cartasJogadorContainer, esconderSegunda: false);
}

    
    void AtualizarCartasUI(List<Carta> cartas, Transform container, bool esconderSegunda = false)
    {
        foreach (Transform filho in container)
            Destroy(filho.gameObject);

        for (int i = 0; i < cartas.Count; i++)
        {
            var carta = cartas[i];
            GameObject novaCarta = Instantiate(cartaPrefab, container);

            Image imagem = novaCarta.GetComponent<Image>();

            
            if (esconderSegunda && i == 1)
            {
                imagem.sprite = spriteCartaVirada; 
            }
            else if (carta != null)
            {
                imagem.sprite = carta.sprite;
            }
        }
    }

    void LimparMesa()
    {
        foreach (Transform t in cartasDealerContainer)
            Destroy(t.gameObject);

        foreach (Transform t in cartasJogadorContainer)
            Destroy(t.gameObject);
    }

    public void ReiniciarJogo()
    {
        btnHit.interactable = true;
        btnStand.interactable = true;
        IniciarJogo();
    }
}
