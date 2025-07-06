using UnityEngine;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;


    public static DeliveryManager instance {get; private set;}
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfulRecipesAmount = 0;

    private void Awake()
    {
        instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }



    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.name);
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }

            }
        }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO=waitingRecipeSOList[i];


            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //has the same number of ingredients
                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //cykubg through all ingredients in the recipe
                    bool ingredinetFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //in the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredinetFound = true;
                            break;
                        }
                    }
                    if (!ingredinetFound)
                    {
                        //this recipe was not found in the palte
                        plateContentMatchesRecipe=false;
                    }
                }
                if (plateContentMatchesRecipe)
                {
                    successfulRecipesAmount++;
                    //player delivered correct one
                    Debug.Log("Player Delivered the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this,EventArgs.Empty);
                    return;
                }
            }
        }
        //no matches found
        OnRecipeFailed?.Invoke(this,EventArgs.Empty);
        Debug.Log("Player Didnt Deliver a correct Recipe");
    }
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }



}