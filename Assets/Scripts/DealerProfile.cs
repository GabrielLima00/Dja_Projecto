[System.Serializable]
public class DealerProfile
{
    public string nome;
    public int pontuacaoMaxima = 21;
    public bool dealerBlackjackEntre19e21 = false;

    public DealerProfile(string nome, int pontuacaoMaxima, bool dealerBlackjackEntre19e21 = false)
    {
        this.nome = nome;
        this.pontuacaoMaxima = pontuacaoMaxima;
        this.dealerBlackjackEntre19e21 = dealerBlackjackEntre19e21;
    }
}
