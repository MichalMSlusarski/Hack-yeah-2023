using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class BuildingManager : MonoBehaviour
{
    public delegate void OnCardSelected();
    public event OnCardSelected OnCard;
    
    public GameObject[] buildingBlocks;

    public LayerMask layerMask;

    public int selectedBuilding = 0;

    public bool isInBuildMode = false;

    public Material mockMaterial;

    List<GameObject> Buildings = new List<GameObject>();

    public int maxBuildingsLeft = 12;
    public TextMeshProUGUI buildingsLeftText;

    public void Redo()
    {
        // deactivate the last building from buildings list, 
    }

    private void Update()
    {
        MouseOverCheck();
    }

    private void MouseOverCheck()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        
        if (!isInBuildMode) { return; }

        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10000f, layerMask))
        {            
            if (hit.transform.CompareTag("Tile"))
            {
                PlaceBuilding(hit.transform.gameObject, .57f);
            }
            else if (hit.transform.CompareTag("Block"))
            {
                PlaceBuilding(hit.transform.gameObject, 1f);
            }
            else if (hit.transform.CompareTag("TallBlock"))
            {
                PlaceBuilding(hit.transform.gameObject, 2f);
            }
        }
    }

    private void PlaceBuilding(GameObject tile, float offset)
    {
        if (tile.transform.childCount > 0) { return; }

        Collider tileCollider = tile.GetComponent<Collider>();
        Collider buildingCollider = buildingBlocks[selectedBuilding].GetComponent<Collider>();

        Vector3 position = new Vector3(tile.transform.position.x, tile.transform.position.y + offset, tile.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject building = Instantiate(buildingBlocks[selectedBuilding], position, Quaternion.identity);
            building.GetComponent<Rigidbody>().isKinematic = true;
            Buildings.Add(building);
            maxBuildingsLeft--;
            buildingsLeftText.text = maxBuildingsLeft.ToString();
            OnCard?.Invoke();
        }
    }

    public void Play()
    {
        isInBuildMode = false;
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        GameObject[] tallBlocks = GameObject.FindGameObjectsWithTag("TallBlock");
        List<GameObject> combinedBlocks = new List<GameObject>(blocks);
        combinedBlocks.AddRange(tallBlocks);
        foreach (GameObject block in combinedBlocks)
        {
            block.GetComponent<Rigidbody>().isKinematic = false;
        }
        GameObject flag = GameObject.FindGameObjectsWithTag("Flag")[0];
        flag.GetComponent<Rigidbody>().isKinematic = false;
        Dinosaur dinosaur = GameObject.FindGameObjectsWithTag("Dinosaur")[0].GetComponent<Dinosaur>();
        dinosaur.Attack();
    }

    
}