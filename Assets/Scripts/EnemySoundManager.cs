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

        // AudioSource�� �������ų� ������ �߰��մϴ�.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ���÷� ���� Ŭ������ �ε��ϰų� �����մϴ�.
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
