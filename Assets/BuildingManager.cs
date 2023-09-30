using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public delegate void OnCardSelected();
    public event OnCardSelected OnCard;
    
    public GameObject[] buildingBlocks;

    public LayerMask layerMask;

    public int selectedBuilding = 0;

    public bool isInBuildMode = false;

    public Material mockMaterial;

    private void Redo()
    {
        // ... 
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
            Debug.Log("Mouse is over: " + hit.transform.name + " " + hit.transform.tag);
            
            // if (Input.GetMouseButtonDown(0))
            // {
            //     Debug.Log("Clicked on " + hit.transform.name);
            // }
            
            if (hit.transform.CompareTag("Tile"))
            {
                PlaceMockBuilding(hit.transform.gameObject);
            }
        }
    }

    private void PlaceMockBuilding(GameObject tile)
    {
        if (tile.transform.childCount > 0) { return; }

        Transform selectedBuildingTransform = buildingBlocks[selectedBuilding].transform;
        //float offest = selectedBuildingTransform.localScale.y / 2f;
        float offest = 1f;

        Vector3 position = new Vector3(tile.transform.position.x, tile.transform.position.y + offest, tile.transform.position.z);

        // GameObject mockBuilding = Instantiate(buildingBlocks[selectedBuilding], position, Quaternion.identity);
        // mockBuilding.GetComponent<Rigidbody>().isKinematic = true;
        // mockBuilding.transform.SetParent(tile.transform);
        // mockBuilding.GetComponent<MeshRenderer>().material = mockMaterial;
        // Destroy(mockBuilding, 0.2f);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject building = Instantiate(buildingBlocks[selectedBuilding], position, Quaternion.identity);
            Debug.Log("Building placed at " + position);
            building.GetComponent<Rigidbody>().isKinematic = true;
            //Destroy(mockBuilding);
            OnCard?.Invoke();
        }
    }

    public void Play()
    {
        isInBuildMode = false;
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject block in blocks)
        {
            block.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    
}