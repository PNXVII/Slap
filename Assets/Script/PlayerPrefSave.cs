using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefSave : MonoBehaviour
{
    int highScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("Hightscore", 0);
        Debug.Log(highScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            highScore += 1;
            Debug.Log("Score" + highScore);
            Save();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            highScore = 0;
            Debug.Log("HighScore : " + highScore);

            PlayerPrefs.DeleteKey("HighScore");
            PlayerPrefs.DeleteAll();
            Save();
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
