using UnityEngine;
using UnityEngine.SceneManagement;

public static class RunManager
{
    public static int dinheiro = 100;
    public static int lutasRestantes = 3;
    public static int dealerAtual = 1;

    public static void Resetar()
    {
        dinheiro = 100;
        lutasRestantes = 3;
        dealerAtual = 1;
        PlayerStats.Resetar();
        SceneManager.LoadScene("MainMenu");
    }

    public static void Vitoria()
    {
        dinheiro = Mathf.RoundToInt(dinheiro * PlayerStats.multiplicadorVitoria);
        VerificarFimDaFase();
    }

    public static void Derrota()
    {
        VerificarFimDaFase();
    }

    private static void VerificarFimDaFase()
    {
        lutasRestantes--;

        Debug.Log("Lutas restantes: " + lutasRestantes);
        Debug.Log("Dinheiro atual: " + dinheiro);
        Debug.Log("Objetivo: " + GetDinheiroMinimoPorDealer());

        if (lutasRestantes > 0)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            int minimo = GetDinheiroMinimoPorDealer();

            if (dinheiro >= minimo)
            {
                Debug.Log("Objetivo atingido! Indo para a Shop.");
                SceneManager.LoadScene("Shop");
            }
            else
            {
                Debug.Log("Objetivo não atingido. Reiniciando run.");
                Resetar();
            }
        }
    }

    public static void AvancarDealer()
    {
        dealerAtual++;
        lutasRestantes = 3;
    }

    public static int GetDinheiroMinimoPorDealer()
    {
        switch (dealerAtual)
        {
            case 1: return 400;
            case 2: return 800;
            case 3: return 1600;
            default: return int.MaxValue;
        }
    }
}
