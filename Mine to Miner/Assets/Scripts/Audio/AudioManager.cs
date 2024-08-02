using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip MineDamaged;
    [SerializeField] private AudioClip NotDamageable;


    [SerializeField] private AudioClip PickaxeUpgrade;

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

        MineManager.mineDamaged += PlayMineDamagedSound;
        MineManager.mineNotDamageable += PlayMineNotDamageableSound;

        Anvil.Pickaxeupgrade += PlayPickaxeUpgradeSound;
    }

    void PlayMineDamagedSound()
    {
        audioSource.PlayOneShot(MineDamaged);
    }

    void PlayMineNotDamageableSound()
    {
        audioSource.PlayOneShot(NotDamageable);
    }

    void PlayMineDestroySound()
    {

    }

    void PlayPickaxeUpgradeSound()
    {
        audioSource.PlayOneShot(PickaxeUpgrade);
    }

    void PlayPickaxeSwingSound()
    {

    }

    private void OnDisable()
    {
        MineManager.mineDamaged -= PlayMineDamagedSound;
        MineManager.mineNotDamageable -= PlayMineNotDamageableSound;

        Anvil.Pickaxeupgrade -= PlayPickaxeUpgradeSound;
    }
}
