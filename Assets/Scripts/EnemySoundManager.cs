using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    public static EnemySoundManager instance;

    public AudioClip roarSound;
    public AudioClip runSound;
    public AudioClip deathSound;
    public AudioClip gethitSound;
    public AudioClip idleSound;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // AudioSource를 가져오거나 없으면 추가합니다.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 예시로 사운드 클립들을 로드하거나 설정합니다.
        roarSound = Resources.Load<AudioClip>("RoarSound");
        runSound = Resources.Load<AudioClip>("RunSound");
        deathSound = Resources.Load<AudioClip>("DeathSound");
    }

    public void PlayIdleSound()
    {
        if (idleSound != null)
        {
            audioSource.Stop();
            audioSource.clip = idleSound;
            audioSource.Play();
        }
    }

    public void PlayRoarSound()
    {
        if (roarSound != null)
        {
            audioSource.Stop();
            audioSource.clip = roarSound;
            audioSource.Play();
        }
    }

    public void PlayGetHitSound()
    {
        if (gethitSound != null)
        {
            audioSource.Stop();
            audioSource.clip = gethitSound;
            audioSource.Play();
        }
    }

    public void PlayRunSound()
    {
        if (runSound != null)
        {
            audioSource.Stop();
            audioSource.clip = runSound;
            audioSource.Play();
        }
    }

    public void PlayDeathSound()
    {
        if (deathSound != null)
        {
            audioSource.Stop();
            audioSource.clip = deathSound;
            audioSource.Play();
        }
    }
}
