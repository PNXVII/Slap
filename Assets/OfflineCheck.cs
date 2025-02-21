using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CheckDifferentTime();
    }

    private void CheckDifferentTime()
    {
        string lastPlayTime = PlayerPrefs.GetString("LastPlayedTime");
        DateTime lastTime = DateTime.Parse(lastPlayTime);
        TimeSpan different = DateTime.UtcNow - lastTime;
        Debug.Log("Different in second : " + different.TotalSeconds);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LlasrPlayedTime", DateTime.UtcNow.ToString());
        Debug.Log(DateTime.UtcNow.ToString());
        PlayerPrefs.Save();
    }
}
