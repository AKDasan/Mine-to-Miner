using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeAnimController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider pickaxeCollider;
    private bool isSwing;
    private bool isCanvasOpen;

    private void Start()
    {
        isSwing = false;
        CanvasController.OnCanvasToggle += HandleCanvasToggle;
    }
    private void OnDisable()
    {
        CanvasController.OnCanvasToggle -= HandleCanvasToggle;
    }

    private void HandleCanvasToggle(bool isOpen)
    {
        isCanvasOpen = isOpen;
    }

    private void Update()
    {
        PickaxeSwing();
        pickaxeCollider.GetComponent<Collider>().enabled = isSwing;
    }

    void PickaxeSwing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isSwing == false && !isCanvasOpen)
        {
            StartCoroutine(SwingPickaxeAnimController());
        }
    }

    IEnumerator SwingPickaxeAnimController()
    {
        animator.SetTrigger("Swing");  
        isSwing = true;
        yield return new WaitForSeconds(1);
        isSwing = false;
        animator.ResetTrigger("Swing");
    }
}
