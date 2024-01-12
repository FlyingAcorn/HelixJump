using UnityEngine;
public class Helix : MonoBehaviour
{
   [SerializeField] private float rotationSpeed;
   private void Update()
   {
      HelixController();
   }
   private void HelixController()
   {
      if (Input.touchCount != 1) return;
      var touchInput = Input.GetTouch(0);
      if (touchInput.phase == TouchPhase.Moved)
      {transform.Rotate(new Vector3(0,touchInput.deltaPosition.x) ,rotationSpeed * Time.deltaTime);}
   }
}
