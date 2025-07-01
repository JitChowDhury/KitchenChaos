using UnityEngine;

public class KitchenObject : MonoBehaviour
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;


    private IKitchenObjectParent KitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
  {
    return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }
        this.KitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("ALready has a kitchen object");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }



    public IKitchenObjectParent GetKitchenObjectParent()
  {
    return KitchenObjectParent;
  }
}
