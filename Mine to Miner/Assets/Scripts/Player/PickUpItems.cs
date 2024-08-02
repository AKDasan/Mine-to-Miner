using System;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{
    [SerializeField] Transform leftHandRef;
    public float pickUpRange = 2.5f;
    [SerializeField] float forcePower = 5f;

    public bool isPick;
    private RaycastHit hit;
    private GameObject pickedUpItem;

    private void Update()
    {
        PickUpController();
    }

    void PickUpController()
    {
        if (!isPick)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, pickUpRange))
            {
                if (hit.transform != null && hit.transform.CompareTag("mineParticles") || hit.transform.CompareTag("mineIngots"))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        PickUpItem(hit.transform.gameObject);
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DropItem();
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        if (item.transform.parent != null)
        {
            item.transform.parent = null;
        }

        pickedUpItem = item;

        int leftHandLayer = LayerMask.NameToLayer("LeftHand");
        pickedUpItem.layer = leftHandLayer;

        pickedUpItem.transform.position = leftHandRef.position;
        pickedUpItem.transform.parent = leftHandRef;
        isPick = true;

        Rigidbody rb = pickedUpItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void DropItem()
    {
        if (pickedUpItem != null)
        {
            int leftHandLayer = LayerMask.NameToLayer("Default");
            pickedUpItem.layer = leftHandLayer;

            pickedUpItem.transform.parent = null;
            isPick = false;

            Rigidbody rb = pickedUpItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(Camera.main.transform.forward * forcePower, ForceMode.Impulse);
            }

            pickedUpItem = null;
        }
    }
}
