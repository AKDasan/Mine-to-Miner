using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineManager : MonoBehaviour
{
    [SerializeField] private MineSO mineSo;

    public float mineHealth;
    private bool isDiggible;
    private bool isDug = false;
    private bool isCoroutineOneRunning = false;

    private bool isDamageable = true;
    private bool isDestroy = false;

    // HealthBar
    private Camera mCam;
    [SerializeField] private Vector3 mHealthBarPos;
    [SerializeField] private float maxMineHealth;
    [SerializeField] private Image mineHealthBGBar;
    [SerializeField] private Image mineHealthBar;

    // Events
    public static event Action mineDamaged;
    public static event Action mineNotDamageable;

    private void Awake()
    {
        mCam = Camera.main;
    }

    private void LateUpdate()
    {
        mineHealthBGBar.transform.position = transform.position + mHealthBarPos;
        mineHealthBar.transform.position = transform.position + mHealthBarPos;
        
        mineHealthBar.transform.rotation = mCam.transform.rotation;
        mineHealthBGBar.transform.rotation = mCam.transform.rotation;
    }

    private void Start()
    { 
        MineController(mineSo.mineType);
        maxMineHealth = mineHealth;
    }

    private void Update()
    {
        MineHealthController();
    }

    void MineController(MineType minetype)
    {
        switch (minetype)
        {
            case MineType.Stone:
                mineHealth = 100;
                Debug.Log($"Maden Tipi: {minetype} - Maden Caný: {mineHealth}");
                break;

            case MineType.Coal:
                mineHealth = 125;
                Debug.Log($"Maden Tipi: {minetype} - Maden Caný: {mineHealth}");
                break;

            case MineType.Iron:
                mineHealth = 300;
                Debug.Log($"Maden Tipi: {minetype} - Maden Caný: {mineHealth}");
                break;

            case MineType.Gold:
                mineHealth = 250;
                Debug.Log($"Maden Tipi: {minetype} - Maden Caný: {mineHealth}");
                break;

            case MineType.Emerald:
                mineHealth = 500;
                Debug.Log($"Maden Tipi: {minetype} - Maden Caný: {mineHealth}");
                break;

            case MineType.Diamond:
                mineHealth = 1000;
                Debug.Log($"Maden Tipi: {minetype} - Maden Caný: {mineHealth}");
                break;

            default:
                Debug.Log("Böyle bir maden bulunamadý!");
                break;
        }
    }

    void DigController(MineValue mineValue)
    {
        //Debug.Log($"MineValue: {mineValue}");

        if (PickaxeManager.Instance.PickaxeValue >= (int)mineValue)
        {
            isDiggible = true;
            //Debug.Log("Þu an kazýlabilir.");
        }
        else
        {
            isDiggible = false;
            //Debug.Log("Þu an kazýlamaz.");
        }
    }

    void DigMine()
    {
        if (isDiggible && isDamageable)
        {
            isDug = true;
            mineHealth -= PickaxeManager.Instance.PickaxeDamage;
            mineDamaged?.Invoke();
            StartCoroutine(IsDamageable());
            Debug.Log("Hasar verildi.");
        }
        else
        {
            if (isDamageable)
            {
                mineNotDamageable?.Invoke();
                Debug.Log("Kazmaný güçlendir.");
            }          
        }
    }

    void MineHealthController()
    {
        if (mineHealth > maxMineHealth)
        {
            mineHealth = maxMineHealth;
        }
        if (mineHealth <= 0)
        {
            StartCoroutine(MineDestroy());
        }

        mineHealthBar.fillAmount = mineHealth / maxMineHealth;

        HealthBarStatus();
    }

    IEnumerator MineDestroy()
    {
        if (isDestroy)
        {
            yield break;
        }

        isDestroy = true;

        Vector3 spawnPosition = transform.position + new Vector3(0, 0.5f, 0);

        yield return new WaitForSeconds(0.01f); // Burada amacým resource prefablarýnýn mine prefabý yok olmadan çýkmasýný engellemek(Collider sorunu için).
        Destroy(gameObject);
        
        for (int i = 0; i < 10; i++)
        {
            GameObject resource = Instantiate(mineSo.resourcePiece, spawnPosition, Quaternion.identity);
        }
    }

    void HealthBarStatus()
    {
        if (isDug)
        {
            mineHealthBar.enabled = true;
            mineHealthBGBar.enabled = true;
            if (!isCoroutineOneRunning)
            {
                StartCoroutine(HealthBarCheck());
            }
        }
        else
        {
            mineHealthBar.enabled = false;
            mineHealthBGBar.enabled = false;
            isCoroutineOneRunning = false;
        }
    }

    IEnumerator HealthBarCheck()
    {
        isCoroutineOneRunning = true;
        yield return new WaitForSeconds(20);
        isDug = false;
        isCoroutineOneRunning = false;
    }

    IEnumerator IsDamageable()
    {
        isDamageable = false;
        yield return new WaitForSeconds(1);
        isDamageable = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickaxe"))
        {
            DigController(mineSo.mineValue);
            DigMine();
            Debug.Log("Kazma ile temasa geçildi.");
        }
    }    
}
