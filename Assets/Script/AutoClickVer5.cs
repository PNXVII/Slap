using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoClickVer5 : MonoBehaviour
{
    public int autoClicksPerSec5;
    public int minimumClickToUnlock5;
    public AudioSource upgradeSound;

    private float addClicks;
    private Manager manager;
    public TextMeshProUGUI priceText, amountText;

    private void Start()
    {
        manager = Manager.instance; ;

        if (PlayerPrefs.GetInt("autoClicksPerSec5") != 0)
        {
            autoClicksPerSec5 = PlayerPrefs.GetInt("autoClicksPerSec5");
        }
        if (PlayerPrefs.GetInt("minimumClickToUnlock5") != 0)
        {
            minimumClickToUnlock5 = PlayerPrefs.GetInt("minimumClickToUnlock5");
        }
        UpdateText();
    }

    public void BuyUpgrade()
    {

        if (manager.TotalClicks >= minimumClickToUnlock5)
        {
            manager.TotalClicks -= minimumClickToUnlock5;

            autoClicksPerSec5 += 10;
            minimumClickToUnlock5 *= 5;

            PlayerPrefs.SetInt("autoClicksPerSec5", autoClicksPerSec5);
            PlayerPrefs.SetInt("minimumClickToUnlock5", minimumClickToUnlock5);
            UpdateText();

            if (upgradeSound != null)
            {
                upgradeSound.Play();
            }
        }
    }

    private void Update()
    {
        if (autoClicksPerSec5 > 0)
        {
            addClicks += autoClicksPerSec5 * Time.deltaTime;
            int clicksToAdd = Mathf.FloorToInt(addClicks);

            Debug.Log("addClicks: " + addClicks + " | clicksToAdd: " + clicksToAdd);

            if (clicksToAdd > 0)
            {
                manager.AddClicks(clicksToAdd, true);
                addClicks -= clicksToAdd;

                Debug.Log("������ԡ: " + clicksToAdd + " | TotalClicks: " + manager.TotalClicks);
            }

        }
    }

    private void UpdateText()
    {
        string priceTextFormatted;

        // ��Ǩ�ͺ��� minimumClickToUnlock �ҡ����������ҡѺ 1000 �������
        if (minimumClickToUnlock5 >= 1000)
        {
            // �ŧ���ٻẺ "k" (�� 1000 -> 1k, 1500 -> 1.5k)
            priceTextFormatted = (minimumClickToUnlock5 / 1000f).ToString("0.0") + "k";
        }
        else
        {
            // �ʴ�����Ţ���Զ�ҹ��¡��� 1000
            priceTextFormatted = minimumClickToUnlock5.ToString("0");
        }

        // �ѻവ��ͤ��� UI
        priceText.text = "Need " + priceTextFormatted + " score";
        amountText.text = "+" + (autoClicksPerSec5 + 10).ToString() + "/s";
    }
}
