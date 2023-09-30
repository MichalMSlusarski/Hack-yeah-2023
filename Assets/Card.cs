using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Card : MonoBehaviour
{
    float initialScale = 1f;
    float desiredSize = 1.1f;
    float lerpTime = 0.3f;
    bool isSelected = false;

    public int cardIndex = 0;

    public BuildingManager BuildingManager;

    private void Start()
    {
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

}
