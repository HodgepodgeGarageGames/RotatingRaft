using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleToon : MonoBehaviour
{
    [SerializeField] private AudioClip[] aClip = new AudioClip[0];
    [SerializeField] private AudioClip[] startClip = new AudioClip[0];
    private AudioSource audioSource = null;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        audioSource.Stop();
    }

    public void playASound()
    {
        audioSource.Stop();
        audioSource.clip = aClip[Random.Range(0, aClip.Length)];
        audioSource.Play();
    }

    public void playStartSound()
    {
        audioSource.Stop();
        audioSource.clip = startClip[Random.Range(0, startClip.Length)];
        audioSource.Play();
    }
}
