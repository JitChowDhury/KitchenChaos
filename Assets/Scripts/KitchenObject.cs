using UnityEngine;

public class KitchenObject : MonoBehaviour
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;


    private IKitchenObjectParent KitchenObjectParent;//current parent ( like player or counter)

    public KitchenObjectSO GetKitchenObjectSO()
  {
    return kitchenObjectSO;
    }

    //it assigns the kitchen object to new parent
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.KitchenObjectParent != null)//check if it has a parent and if then clears it
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }

        this.KitchenObjectParent = kitchenObjectParent;//asigns the new parent

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("ALready has a kitchen object");
            //return;
        }
        kitchenObjectParent.SetKitchenObject(this);//sets this object as parent kitchenobject
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }


    //returns the current Parent
  public IKitchenObjectParent GetKitchenObjectParent()
  {
    return KitchenObjectParent;
  }

    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject() ;
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject=null;
            return false;
        }
    }


    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;

    }
}
