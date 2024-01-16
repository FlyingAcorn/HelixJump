using System;
using DG.Tweening;
using UnityEngine;
public class HelixController : MonoBehaviour
{
   [SerializeField] private float rotationSpeed;
   public GameObject[] allChunks;
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
         t.transform.DOScale(new Vector3(0, 0, 0), 2);
         t.transform.DOShakePosition(2 ,new Vector3(0, 0.4f, 0),5,5);
         t.gameObject.TryGetComponent(out Renderer chunk);
         chunk.material.DOColor(Color.red, 1f);
      }
      DOVirtual.DelayedCall(2,()=> this.gameObject.SetActive(false));
   }

   private void OnDisable()
   {
      foreach (var t in allChunks)
      {
         t.transform.localScale = new Vector3(150, 150, 45);
      }
      if (!HelixManager.Instance.pooledHelixes.Contains(gameObject))
      {
         HelixManager.Instance.pooledHelixes.Add(this.gameObject);
      }
   }
}
