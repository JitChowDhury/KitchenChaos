using System;
using UnityEngine;

public class ClearCounter : MonoBehaviour,IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter secondClearCounter;


    private KitchenObject kitchenObject;


    public void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            Debug.Log("Interact");
            Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            KitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
         
        }

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject= kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject !=null;
        
    }
}
