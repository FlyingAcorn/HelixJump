using UnityEngine;
using Random = UnityEngine.Random;

public class MusicManager : Singleton<MusicManager>
{
    private AudioSource _soundFX;
    [SerializeField] private AudioClip[] music;
     
    protected override void Awake()
    {
        base.Awake();
        _soundFX = GetComponent<AudioSource>();
    }
    public void PlayMusic()
    {
        if (_soundFX.isPlaying) return;
        var randomMusic = Random.Range(0, music.Length);
        _soundFX.clip = music[randomMusic];
        _soundFX.Play();
    }
}
