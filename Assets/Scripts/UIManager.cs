using UnityEngine;
using UnityEngine.Audio;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private AudioMixer audioMixer;
    // bu setvolume kısmını slider yaptığında canvasa bağlayacaksın
    // value aralığı min 0.0001 ile 1
    //string olarakta mixer grubundaki adını alıcak(exposed kısmındakı sağust)
    public void SetVolume(float volume, string group)
    {
        audioMixer.SetFloat(group, Mathf.Log10(volume) * 20f);
    }
}
