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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is holding a plate

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {

                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //player is not carriyng a plate but something else
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //counter is holding a plate
                      if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
                
            }

       }

    }
}
