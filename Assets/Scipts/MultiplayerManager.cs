using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    private static MultiplayerManager instance;
    public static MultiplayerManager Instance
    {
        get
        {
            return instance;
        }
    }
    private int coinCount;
    [Header("Text Object")]
    [SerializeField] TextMeshProUGUI player2TextDisplay;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else { Destroy(this); }
    }
    public void IncrementCoinUI(int amount)
    {
        coinCount += amount;
        player2TextDisplay.text = coinCount.ToString();
    }
    public int getPlayer2Score()
    {
        return coinCount;
    }

}
