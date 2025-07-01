using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();//return the position where the kitchen object should be placed
    public void SetKitchenObject(KitchenObject kitchenObject);//assigns a kitchen object to this parent
    public KitchenObject GetKitchenObject();//gets the current kitchen object
    public void ClearKitchenObject();//removes the kitchen object
    public bool HasKitchenObject();//checks if a kitchen object is currently held

}
