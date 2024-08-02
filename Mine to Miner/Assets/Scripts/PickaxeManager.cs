using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickaxeManager : MonoBehaviour
{
    public static PickaxeManager Instance {  get; private set; }

    [SerializeField] private int pickaxeValue;
    [SerializeField] private int pickaxeDamage;

    [SerializeField] TextMeshProUGUI pickaxeLevelText;

    public int PickaxeValue
    {
        get { return pickaxeValue; }
    }

    public int PickaxeDamage
    {
        get { return pickaxeDamage; }
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
        pickaxeValue = 1;
    }

    private void Update()
    {
        PickaxeDamageController();
        PickaxeLevelController();
    }

    public void PickaxeDamageController()
    {
        switch (pickaxeValue)
        {
            case 1: pickaxeDamage = 5; break;
            case 2: pickaxeDamage = 15; break;
            case 3: pickaxeDamage = 30; break;
            case 4: pickaxeDamage = 50; break;
            case 5: pickaxeDamage = 85; break;
        }
    }

    public void PickaxeLevelController()
    {
        switch (pickaxeValue)
        {
            case 1: pickaxeLevelText.text = "Pickaxe Level = 1"; break;
            case 2: pickaxeLevelText.text = "Pickaxe Level = 2"; break;
            case 3: pickaxeLevelText.text = "Pickaxe Level = 3"; break;
            case 4: pickaxeLevelText.text = "Pickaxe Level = 4"; break;
            case 5: pickaxeLevelText.text = "Pickaxe Level = 5"; break;
        }
    }

    public void IncreasePickaxeValue()
    {
        if (pickaxeValue < 5)
        {
            pickaxeValue += 1;
        }
        else
        {
            Debug.Log("Kazma son seviyede.");
        }
    }

    public void DecreasePickaxeValue()
    {
        if (pickaxeValue > 1)
        {
            pickaxeValue -= 1;
        }
    }
}
