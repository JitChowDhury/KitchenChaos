using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter,IHasProgress
{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;


    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //theres is not kitchenobject here
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //player carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {

                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
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
            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }

        }


    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //there is a kitchenobject here and it can be cut
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {

                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                //there is a kitchenObject here
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }

        }
    }

    public bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;


    }




    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }

    }


    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}

