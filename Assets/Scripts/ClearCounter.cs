using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player)
    {
       if (!HasKitchenObject())
       {
            //theres is not kitchenobject here
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //player has noting ( not carrying anything)
            }
       }
       else
       {
            //there is a kitchenobject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }

       }

    }
}
