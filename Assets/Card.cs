using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class Card : MonoBehaviour
{
    float initialScale = 1f;
    float desiredSize = 1.1f;
    float lerpTime = 0.3f;
    bool isSelected = false;

    public GameObject grayOut;

    public int cardIndex = 0;
    public BuildingManager BuildingManager;

    private Selectable selectable;

    public SO_String selectedCard;

    private void Start()
    {
        selectable = gameObject.GetComponent<Selectable>();
        BuildingManager.OnCard += Deselect;
    }
    
    public void Selected()
    {
        isSelected = true;
        BuildingManager.isInBuildMode = true;
        BuildingManager.selectedBuilding = cardIndex;
        gameObject.transform.DOScale(desiredSize, lerpTime);
    }

    public void Deselect()
    {
        isSelected = false;
        gameObject.transform.DOScale(initialScale, lerpTime);
    }

    public void Deselector()
    {
        BuildingManager.isInBuildMode = false;
    }

    private void OnDisable()
    {
        BuildingManager.OnCard -= Deselect;
    }

}
