using UnityEngine;
using DG.Tweening;
public class HelixTrigger : MonoBehaviour
{
    public void Score()
    {
        Shrink();
        var sfxManager = SfxManager.Instance;
        sfxManager.PlaySfx(0);
        GameManager.Instance.ScoreCount ++;
        GameManager.Instance.ComboCount ++;
        HelixManager.Instance.SpawnHelix();
        DOVirtual.DelayedCall(2,()=> gameObject.transform.parent.gameObject.SetActive(false));
    }
    
    private void Shrink()
    {
        foreach (var t in gameObject.GetComponentInParent<HelixController>().allChunks)
        {
            t.TryGetComponent(out MeshCollider tCollider);
            tCollider.enabled = false;
            t.transform.DOScale(new Vector3(0, 0, 0), 1.5f);
        }

        foreach (var t in gameObject.GetComponentInParent<HelixController>().allTriggers)
        {
            t.TryGetComponent(out MeshCollider tCollider);
            tCollider.enabled = false;
        }
    }
}
