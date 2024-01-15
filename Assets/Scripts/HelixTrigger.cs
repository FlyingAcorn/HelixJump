using UnityEngine;
public class HelixTrigger : MonoBehaviour
{
    public void Score()
    {
        var sfxManager = SfxManager.Instance;
        sfxManager.PlaySfx(0);
        GameManager.Instance.ScoreCount ++;
        GameManager.Instance.ComboCount ++;
        transform.parent.gameObject.SetActive(false);
        HelixManager.Instance.pooledHelixes.Add(transform.parent.gameObject);

    }
}
