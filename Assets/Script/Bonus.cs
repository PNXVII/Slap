using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    private Manager manager;
    
    public int minimumClickToUnlock;
    public TextMeshProUGUI priceText;

    private bool hasUpgrade;

    [Range(3,10)]public int averageBonusClicks;

    private void Start()
    {
        manager = Manager.instance; ;

        if(PlayerPrefs.GetInt("HasBonusUpgrade") == 1)
        {
            hasUpgrade = true;
            GetComponent<Button>().interactable = false;
        }

        UpdateText();
    }

    public void BuyUpgrade()
    {
        if (manager.TotalClicks >= minimumClickToUnlock)
        {
            manager.TotalClicks -= minimumClickToUnlock;

            hasUpgrade = true;
            PlayerPrefs.SetInt("HasBonusUpgrade", 1);
            GetComponent<Button>().interactable = false;
        }
    }


    private void UpdateText()
    {
        priceText.text = "Need" + minimumClickToUnlock.ToString("0") + " Score";
    }

    public void Clicked()
    {
        if(hasUpgrade)
        {
            manager.TotalClicks += Random.Range(averageBonusClicks - 3, averageBonusClicks + 3);
        }
    }
}
