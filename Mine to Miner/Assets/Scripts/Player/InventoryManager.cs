using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; private set;}

    [SerializeField] private int stoneIngot;
    [SerializeField] private int coalIngot;
    [SerializeField] private int ironIngot;
    [SerializeField] private int goldIngot;
    [SerializeField] private int emeraldIngot;
    [SerializeField] private int diamondIngot;

    [SerializeField] Canvas inventoryCanvas;
    [SerializeField] TextMeshProUGUI stoneText;
    [SerializeField] TextMeshProUGUI coalText;
    [SerializeField] TextMeshProUGUI ironText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI emeraldText;
    [SerializeField] TextMeshProUGUI diamondText;

    public int StoneAmount
    {
        get { return stoneIngot; }
    }

    public int CoalAmount
    {
        get { return coalIngot; }
    }

    public int IronAmount
    {
        get { return ironIngot; }
    }

    public int GoldAmount
    {
        get { return goldIngot; }
    }

    public int EmeraldAmount
    {
        get { return emeraldIngot; }
    }

    public int DiamondAmount
    {
        get { return diamondIngot; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        stoneIngot = 0;
        coalIngot = 0;
        ironIngot = 0;
        goldIngot = 0;
        emeraldIngot = 0;
        diamondIngot = 0;

        inventoryCanvas.enabled = false;
    }

    private void Update()
    {
        MineIngotAmountController();
        OpenCloseInventory();
        SourceTextController();
    }

    void OpenCloseInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryCanvas.enabled = !inventoryCanvas.enabled;
            //CanvasController.ToggleCanvas(inventoryCanvas.enabled);
        }
    }

    void SourceTextController()
    {
        stoneText.text = $"Stone : {stoneIngot.ToString()}";
        coalText.text = $"Coal : {coalIngot.ToString()}";
        ironText.text = $"Iron : {ironIngot.ToString()}";
        goldText.text = $"Gold : {goldIngot.ToString()}";
        emeraldText.text = $"Emerald : {emeraldIngot.ToString()}";
        diamondText.text = $"Diamond : {diamondIngot.ToString()}";
    }

    void MineIngotAmountController()
    {
        if (stoneIngot < 0)
        {
            stoneIngot = 0;
        }
        if (coalIngot < 0)
        {
            coalIngot = 0;
        }
        if (ironIngot < 0)
        {
            ironIngot = 0;
        }
        if (goldIngot < 0)
        {
            goldIngot = 0;
        }
        if (emeraldIngot < 0)
        {
            emeraldIngot = 0;
        }
        if (diamondIngot < 0)
        {
            diamondIngot = 0;
        }
    }

    ///////////////////////////////////

    public void StoneIncrease(int amount)
    {
        stoneIngot += amount;
    }

    public void StoneDecrease(int amount) 
    { 
        stoneIngot -= amount;
    }

    ///////////////////////////////////

    public void CoalIncrease(int amount)
    {
        coalIngot += amount;
    }

    public void CoalDecrease(int amount) 
    { 
        coalIngot -= amount;
    }

    ///////////////////////////////////

    public void IronIncrease(int amount)
    {
        ironIngot += amount;
    }

    public void IronDecrease(int amount) 
    {
        ironIngot -= amount;
    }

    ///////////////////////////////////

    public void GoldIncrease(int amount)
    {
        goldIngot += amount;
    }

    public void GoldDecrease(int amount)
    {
        goldIngot -= amount;
    }

    ///////////////////////////////////

    public void EmeraldIncrease(int amount)
    {
        emeraldIngot += amount;
    }

    public void EmeraldDecrease(int amount)
    {
        emeraldIngot -= amount;
    }

    ///////////////////////////////////

    public void DiamondIncrease(int amount)
    {
        diamondIngot += amount;
    }

    public void DiamondDecrease(int amount)
    {
        diamondIngot -= amount;
    }
}
