
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSOGameobject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSOGameobject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSOGameobject kitchenObjectSOGameobject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameobject.gameObject.SetActive(false);
        }

    }


    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSOGameobject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
