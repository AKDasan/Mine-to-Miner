using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : MonoBehaviour
{
    [SerializeField] private MineIngotSO mineIngotSo;
    private PickUpItems pickUpItems;

    [SerializeField] private Transform outputArea;
    [SerializeField] private bool isFurnitureRun;

    private List<GameObject> mineParticlesInside = new List<GameObject>();

    private void Awake()
    {
        pickUpItems = FindObjectOfType<PickUpItems>();
        isFurnitureRun = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mineParticles"))
        {
            if (!mineParticlesInside.Contains(other.gameObject))
            {
                mineParticlesInside.Add(other.gameObject);
                
                processingQueue.Enqueue(other.gameObject);

                if (!isFurnitureRun)
                {             
                    StartCoroutine(MeltingProcess());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("mineParticles"))
        {
            mineParticlesInside.Remove(other.gameObject);
        }
    }

    private Queue<GameObject> processingQueue = new Queue<GameObject>();

    IEnumerator MeltingProcess()
    {
        isFurnitureRun = true;
        Debug.Log("Furniture Çalýþýyor...");

        while (processingQueue.Count > 0)
        {
            GameObject mineParticle = processingQueue.Dequeue();

            mineIngotSo = mineParticle.GetComponent<MineParticles>().mineIngotSO;

            bool processed = false;

            for (int i = 0; i < outputArea.childCount; i++)
            {
                Transform point = outputArea.GetChild(i);

                if (point.childCount == 0 && !pickUpItems.isPick)
                {
                    yield return new WaitForSeconds(10f);
                    Instantiate(mineIngotSo.resourceIngot, point.position, point.rotation, point);
                    Destroy(mineParticle);
                    processed = true;
                    break;
                }
            }

            if (processed)
            {
                Debug.Log("Furniture Ýþlemi Tamamlandý.");
            }
        }

        isFurnitureRun = false;
    }
}
