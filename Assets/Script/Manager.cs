using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI ClickTotalText;
    public TextMeshProUGUI randomText; // UI ������ʴ���ͤ�������
    public float textDisplayTime = 1.5f; // ���ҷ���ͤ��������ʴ���͹����
    public float textShowChance;
    public AudioSource textSound1;
    public AudioSource textSound2;
    public AudioSource textSound3;
    public AudioSource textSound4;

    private float totalClicks;
    private List<string> randomMessages = new List<string> { "Nice!", "Awesome", "Wow!", "Boom!", "Harder!", "Ahh!" };


    public float TotalClicks
    {
        set
        {
            totalClicks = value;
            PlayerPrefs.SetFloat("TotalClicks", value);
            UpdateText();
        }
        get
        {
            return totalClicks;
        }
    }

    public static Manager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        TotalClicks = PlayerPrefs.GetFloat("TotalClicks");
        randomText.gameObject.SetActive(false); // ��͹��ͤ��������͹�������
    }

    public void AddClicks(int amount = 1, bool isAutoClick = false)
    {
        Debug.Log("Manager: Add Click " + amount);
        TotalClicks += amount;

        Bonus bonus = FindObjectOfType<Bonus>();
        if (bonus != null)
        {
            bonus.Clicked();
            Debug.Log("Bonus Activate");
        }

        // �ʴ���ͤ�������੾�������������ԡ�ѵ��ѵ�
        if (!isAutoClick && Random.value < textShowChance)
        {
            ShowRandomText();
        }
    }
    private void UpdateText()
    {
        if (TotalClicks >= 1000)
        {
            // �ŧ���ٻẺ "k" ����ͨӹǹ��ԡ�ҡ����������ҡѺ 1000
            ClickTotalText.text = (TotalClicks / 1000).ToString("0.0") + "k";
        }
        else
        {
            // �ʴ�����Ţ��������ͨӹǹ��ԡ���¡��� 1000
            ClickTotalText.text = TotalClicks.ToString("0");
        }   
    }

    private void ShowRandomText()
    {
        if (randomMessages.Count > 0)
        {
            string selectedMessage = randomMessages[Random.Range(0, randomMessages.Count)];
            randomText.text = selectedMessage;
            randomText.gameObject.SetActive(true);

            if (Random.value < 0.2f && textSound1 != null)
            {
                textSound1.Play();
            }
            else if (Random.value < 0.3f && textSound2 != null)
            {
                textSound2.Play();
            }
            else if (Random.value < 0.3f && textSound3 != null)
            {
                textSound3.Play();
            }
            else if (textSound3 != null)
            {
                textSound4.Play();
            }

            // ����ͤ�������������ѧ�ҡ���ҷ���˹�
            StopCoroutine("HideText");
            StartCoroutine(HideText());

        }
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(textDisplayTime);
        randomText.gameObject.SetActive(false);
    }
}
