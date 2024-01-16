using System;
using UnityEngine;
using DG.Tweening;
public class HelixTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = new Vector3(150, 150, 1);
    }

    public void Score()
    {
        HelixManager.Instance.pooledHelixes.Add(transform.parent.gameObject);
        Shatter();
        var sfxManager = SfxManager.Instance;
        sfxManager.PlaySfx(0);
        GameManager.Instance.ScoreCount ++;
        GameManager.Instance.ComboCount ++;
        HelixManager.Instance.SpawnHelix();
        DOVirtual.DelayedCall(2,()=> gameObject.transform.parent.gameObject.SetActive(false));
    }
    
    private void Shatter()
    {
        foreach (var t in gameObject.GetComponentInParent<HelixController>().allChunks)
        {
            t.transform.DOScale(new Vector3(0, 0, 0), 1);
        }
    }
}
