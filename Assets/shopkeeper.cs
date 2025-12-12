using System;
using UnityEngine;
using System.Collections.Generic;

using UnityEngine.UI;

public class shopkeeper : MonoBehaviour
{
    public bool playerInRange;
    public bool isTalkingwithPlayer;

    public GameObject shopkepperDialogUI;
    public Button buyBTN;
    public Button exitBTN;

    public GameObject buyPanelUI;

    public void Start()
    {
        shopkepperDialogUI.SetActive(false);
        buyBTN.onClick.AddListener(BuyMode);
        exitBTN.onClick.AddListener(StopTalking);
    }
    private void BuyMode()
    {
        buyPanelUI.SetActive(true);
    }

    private void DialogMode()
    {
        DisplayDialogUI();
        buyPanelUI.SetActive(false);
    }
    public void Talk()
    {
        isTalkingwithPlayer = true;
        DisplayDialogUI();
    }
    private void DisplayDialogUI()
    {
        shopkepperDialogUI.SetActive(true);
    }
    public void StopTalking()
    {
        isTalkingwithPlayer = false;
        HideDialogUI();
    }
    private void HideDialogUI()
    {
        shopkepperDialogUI.SetActive(false);
    }




    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange=false;
        }
    }
}
