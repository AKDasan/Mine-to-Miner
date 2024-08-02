using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Anvil : MonoBehaviour
{
    public static event Action Pickaxeupgrade;

    // CanvasSourceInfo
    [SerializeField] private GameObject AnvilCanvas;
    private bool isPlayerNearby;
    [SerializeField] Button pickaxeUpgradeBTN;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] TextMeshProUGUI sourceText;

    private void Start()
    {
        AnvilCanvas.SetActive(false);
        isPlayerNearby = false;
    }

    private void Update()
    {
        ToggleAnvilCanvas();
        PickaxeUpgradeController();
    }

    public void PickaxeUpgrade()
    {
        switch (PickaxeManager.Instance.PickaxeValue)
        {
            case 1:
                InventoryManager.Instance.StoneDecrease(15);
                break;
            case 2:
                InventoryManager.Instance.StoneDecrease(15);
                InventoryManager.Instance.IronDecrease(35);
                break;
            case 3:
                InventoryManager.Instance.IronDecrease(15);
                InventoryManager.Instance.GoldDecrease(35);
                break;
            case 4:
                InventoryManager.Instance.GoldDecrease(15);
                InventoryManager.Instance.EmeraldDecrease(35);
                break;
            default:
                break;
        }

        Pickaxeupgrade?.Invoke();
        PickaxeManager.Instance.IncreasePickaxeValue();       
    }

    void PickaxeUpgradeController()
    {
        switch (PickaxeManager.Instance.PickaxeValue)
        {
            case 1:
                sourceText.text = "15 stones required.";
                if (InventoryManager.Instance.StoneAmount >= 15)
                {
                    pickaxeUpgradeBTN.interactable = true;
                }
                else
                {
                    pickaxeUpgradeBTN.interactable = false;
                }
                break;
            case 2:
                sourceText.text = "15 stone and 35 iron required.";
                if (InventoryManager.Instance.StoneAmount >= 15 && InventoryManager.Instance.IronAmount >= 35)
                {
                    pickaxeUpgradeBTN.interactable = true;
                }
                else
                {
                    pickaxeUpgradeBTN.interactable = false;
                }
                break;
            case 3:
                sourceText.text = "15 iron and 35 gold required.";
                if (InventoryManager.Instance.IronAmount >= 15 && InventoryManager.Instance.GoldAmount >= 35)
                {
                    pickaxeUpgradeBTN.interactable = true;
                }
                else
                {
                    pickaxeUpgradeBTN.interactable = false;
                }
                break;
            case 4:
                sourceText.text = "15 gold and 45 emerald required.";
                if (InventoryManager.Instance.GoldAmount >= 15 && InventoryManager.Instance.EmeraldAmount >= 40)
                {
                    pickaxeUpgradeBTN.interactable = true;
                }
                else
                {
                    pickaxeUpgradeBTN.interactable = false;
                }
                break;
            case 5:
                sourceText.text = "";
                buttonText.text = "Pickaxe Max Level";
                pickaxeUpgradeBTN.interactable = false;
                break;
            default:
                break;
        }
    }

    private void ToggleAnvilCanvas()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            bool isActive = AnvilCanvas.activeSelf;
            AnvilCanvas.SetActive(!isActive);
            CanvasController.ToggleCanvas(!isActive);
        }      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearby = false;
            AnvilCanvas.SetActive(false);
            CanvasController.ToggleCanvas(false);
        }
    }
}
