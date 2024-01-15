using UnityEngine;
public class SfxManager : Singleton<SfxManager>
{
    private AudioSource _soundFX;
    private int _lastFx;
    public AudioClip[] fX;
    private void Start()
    {
        _soundFX = GetComponent<AudioSource>();
    }
    public void PlaySfx(int sfx, bool reset = false)
    {
        if (_soundFX.isPlaying && sfx != 3 && sfx != 0) return;
        var sound = fX[sfx];
        if (reset)
        {
            _soundFX.pitch = 1f;
        }
        if (sfx == _lastFx)
        {
            _soundFX.pitch += GameManager.Instance.ComboCount * 0.1f;
        }
        else
        {
            _soundFX.pitch = 1f;
        }
        _soundFX.PlayOneShot(sound);
        _lastFx = sfx;
    }
}

    
    
/*public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
{
    if (_instantiatedFx != null && audioClip == _instantiatedFx.clip) return;
    _instantiatedFx = Instantiate(soundFXObject, spawnTransform.position,Quaternion.identity);
    _instantiatedFx.clip = audioClip;
    _instantiatedFx.volume = volume;
    if (_instantiatedFx.clip != fX[3])
    {
        _instantiatedFx.pitch += GameManager.Instance.ComboCount * 0.1f;
    }
    _instantiatedFx.Play();
    float clipLength = _instantiatedFx.clip.length;
    Destroy(_instantiatedFx.gameObject, clipLength);
}
public void PlaySoundMusicClip(AudioClip[] audioClips, Transform spawnTransform, float volume)
{
    if (_instantiatedMusic != null) return;
    var randomMusic = Random.Range(0, music.Length);
    _instantiatedMusic = Instantiate(soundMusicObject, spawnTransform.position,Quaternion.identity);
    _instantiatedMusic.clip = audioClips[randomMusic];
    _instantiatedMusic.volume = volume;
    _instantiatedMusic.Play();
    if ( GameManager.Instance.state != GameManager.GameState.Play) Destroy(_instantiatedMusic.gameObject);
}*/
