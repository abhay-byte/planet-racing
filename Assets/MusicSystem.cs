using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<AudioClip> backgroundMusic = new List<AudioClip>();
    [SerializeField] List<AudioClip> vocalMusic = new List<AudioClip>();
    [SerializeField] List<AudioClip> songs = new List<AudioClip>();
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SelectSong();
        //DontDestroyOnLoad(gameObject);

    }

    private void SelectSong()
    {
        audioSource.clip = PRUtils.GetSingle(songs);
        audioSource.loop = false;
        audioSource.Play();
    }
    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            SelectSong();
        }
    }

}
