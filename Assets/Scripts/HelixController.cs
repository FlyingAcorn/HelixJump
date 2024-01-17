using DG.Tweening;
using UnityEngine;
public class HelixController : MonoBehaviour
{
   [SerializeField] private float rotationSpeed;
   public GameObject[] allChunks;
   public GameObject[] allTriggers;
   private void Update()
   {
      HelixControls();
   }
   private void HelixControls()
   {
      if (Input.touchCount != 1) return;
      var touchInput = Input.GetTouch(0);
      if (touchInput.phase == TouchPhase.Moved)
      {transform.Rotate(new Vector3(0,touchInput.deltaPosition.x) ,rotationSpeed * Time.deltaTime);}
   }
   public void Shatter()
   {
      var soundManager = SfxManager.Instance;
      soundManager.PlaySfx(3,true);
      foreach (var t in allChunks)
      {
         t.TryGetComponent(out MeshCollider tCollider);
         tCollider.enabled = false;
         t.transform.DOScale(new Vector3(0, 0, 0),1.5f);
         t.transform.DOShakePosition(1 ,new Vector3(0, 0.4f, 0),5,20);
         t.TryGetComponent(out Renderer chunk);
         var color = chunk.material.color;
         chunk.material.DOColor(Color.red, 1);
         DOVirtual.DelayedCall(1.5f,() => chunk.material.color = color);
      }
      foreach (var t in allTriggers)
      {
         t.TryGetComponent(out MeshCollider tCollider);
         tCollider.enabled = false;
      }
      DOVirtual.DelayedCall(2,()=> this.gameObject.SetActive(false));
   }

   private void OnDisable()
   {
      foreach (var t in allChunks)
      {
         t.transform.localScale = new Vector3(150, 150, 45);
         t.TryGetComponent(out MeshCollider tCollider);
         tCollider.enabled = true;
      }
      foreach (var t in allTriggers)
      {
         t.TryGetComponent(out MeshCollider tCollider);
         tCollider.enabled = true;
      }
      if (!HelixManager.Instance.pooledHelixes.Contains(gameObject))
      {
         HelixManager.Instance.pooledHelixes.Add(this.gameObject);
      }
   }
}
