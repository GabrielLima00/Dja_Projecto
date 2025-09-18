using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DealerManager : MonoBehaviour
{
    public GameObject painelEscolhaDealer; // Painel do menu de dealers
    public List<Button> dealerButtons; // Lista de bot�es dos dealers
    public Button btnConfirmar; // Bot�o para confirmar a escolha
    public List<Sprite> dealerImages; // Lista de imagens dos dealers
    private int dealerEscolhido = -1; // �ndice do dealer escolhido
    public static int dealerEscolhidoIndex = -1;


    void Start()
    {
        // Garantir que os bot�es t�m eventos atribu�dos
        for (int i = 0; i < dealerButtons.Count; i++)
        {
            int index = i; // Necess�rio para evitar closures estranhos no delegate
            dealerButtons[i].onClick.AddListener(() => SelecionarDealer(index));
        }

        btnConfirmar.onClick.AddListener(ConfirmarEscolha);
    }

    void SelecionarDealer(int index)
{
    dealerEscolhido = index;
    dealerEscolhidoIndex = index; // ATUALIZA O INDEX GLOBAL

    // Reset das cores dos botões
    foreach (Button btn in dealerButtons)
    {
        btn.GetComponent<Image>().color = Color.white;
    }

    // Destacar o dealer escolhido
    dealerButtons[index].GetComponent<Image>().color = Color.green;
}

   

    void ConfirmarEscolha()
    {
        if (dealerEscolhido == -1)
        {
            Debug.Log("Nenhum dealer selecionado!");
            return;
        }

        switch (dealerEscolhido)
        {
            case 0: // Dealer 1: Blackjack com 24
                DealerInfo.dealerSelecionado = new DealerProfile("Dealer 1", 24);
                break;
            case 1: // Dealer 2: Vence entre 19 e 21
                DealerInfo.dealerSelecionado = new DealerProfile("Dealer 2", 21, true);
                break;
            case 2: // Dealer 3: Normal
                DealerInfo.dealerSelecionado = new DealerProfile("Dealer 3", 21);
                break;
        }


        Debug.Log("Dealer Escolhido: " + DealerInfo.dealerSelecionado.nome + " com limite de " + DealerInfo.dealerSelecionado.pontuacaoMaxima);
        SceneManager.LoadScene("GameScene");

    }

    public void AbrirMenuDealers()
    {
        painelEscolhaDealer.SetActive(true);
    }
}
