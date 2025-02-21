using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public Button myButton;  
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3; 
    public AudioSource audioSource4; 
    public AudioSource audioSource5;
    public Animator animator;
    private bool lastPlayed = false;

    void Start()
    {
        if (audioSource != null) audioSource2.playOnAwake = false;
        if (audioSource2 != null) audioSource2.playOnAwake = false;
        if (audioSource3 != null) audioSource3.playOnAwake = false;
        if (audioSource4 != null) audioSource4.playOnAwake = false;
        if (audioSource5 != null) audioSource5.playOnAwake = false;

        myButton.onClick.AddListener(PlaySoundAndAnim);
    }

    void PlaySoundAndAnim()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        bool playRandomSound = Random.value < 0.2f;
        if (playRandomSound && !lastPlayed)
        {
            // สุ่มเลือกเสียงจาก audioSource2, 3, 4, หรือ 5
            AudioSource[] randomSounds = { audioSource2, audioSource3, audioSource4, audioSource5 };
            int randomIndex = Random.Range(0, randomSounds.Length);

            if (randomSounds[randomIndex] != null)
            {
                randomSounds[randomIndex].Play();
                lastPlayed = true; // ป้องกันไม่ให้เสียงสุ่มเล่นซ้ำในครั้งถัดไป
            }
        }
        else
        {
            lastPlayed = false; // รีเซ็ตสถานะ
        }

        if (animator != null)
        {
            animator.CrossFade("Slap", 0f);
        }
    }
}
