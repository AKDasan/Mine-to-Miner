using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("mineIngots"))
       {
           MineIngotSO mineIngot = other.GetComponent<MineParticles>().mineIngotSO;

           if (mineIngot != null) 
           {
                switch (mineIngot.mineType)
                {
                    case MineType.Stone:
                        InventoryManager.Instance.StoneIncrease(5);
                        break;
                    case MineType.Coal:
                        InventoryManager.Instance.CoalIncrease(5);
                        break;
                    case MineType.Iron:
                        InventoryManager.Instance.IronIncrease(5);
                        break;
                    case MineType.Gold:
                        InventoryManager.Instance.GoldIncrease(5);
                        break;
                    case MineType.Emerald:
                        InventoryManager.Instance.EmeraldIncrease(5);
                        break;
                    case MineType.Diamond:
                        InventoryManager.Instance.DiamondIncrease(5);
                        break;
                    default:
                        break;
                }
           }
       }
    }
}
