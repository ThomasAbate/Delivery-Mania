using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.XR;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] private Transform hands;
    void Start()
    {
        Instance = this;
    }

    public void PickupKeyItem(GameObject carton)
    {
        if (PlayerController.instance.freeHand)
        {
            PlayerController.instance.freeHand = false;
            GameObject cartonInstance = Instantiate(, hand);
            _foundKeys.Add(keyItem);
            usableItems.Add(keyInstance.GetComponent<KeyItem>());
            //Utilise le dernier trouvé
            HoldItem(usableItems.Count - 1);
        }
    }
    public void RemoveFromInventory(GameObject carton)
    {
        if (!PlayerController.instance.freeHand)
        {
            _foundKeys.Remove(keyItem);
            int place = FindItemInList(keyItem.ID);
            if (place != -1)
            {
                KeyItem key = usableItems[place];
                usableItems.RemoveAt(place);
                Destroy(key.gameObject);
            }
        }
    }
}
