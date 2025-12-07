using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI objectiveText;
    public bool hasKey = false;


    public int coins = 0;

    void Awake()
    {
        instance = this;
    }

    public void AddCoin()
    {
        coins++;
        coinText.text = "Coins: " + coins;
    }

    public void UpdateObjective(string msg)
    {
        objectiveText.text = msg;
    }
}
