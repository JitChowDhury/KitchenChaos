using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is not carrying anything
            Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            KitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);//instantly set aprent to player
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);//invoke and play animation   
        }
    }

}
