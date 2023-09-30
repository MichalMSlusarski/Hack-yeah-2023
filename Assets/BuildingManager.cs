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
        ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Touch touch = Input.GetTouch(0); ray = Camera.main.ScreenPointToRay(touch.positon);  

        if (Physics.Raycast(ray, out hit, 10000f, layerMask))
        {
            if (hit.transform.CompareTag("Tile"))
            {
                PlaceMockBuilding(hit.transform.gameObject);
            }
        }
    }

    private void PlaceMockBuilding(GameObject tile)
    {
        if (tile.transform.childCount > 0) { return; }

        Vector3 position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.501f, tile.transform.position.z);

        GameObject mockBuilding = Instantiate(buildingBlocks[selectedBuilding], position, Quaternion.identity);
        mockBuilding.GetComponent<Rigidbody>().isKinematic = true;
        mockBuilding.transform.SetParent(tile.transform);
        mockBuilding.GetComponent<MeshRenderer>().material = mockMaterial;
        Destroy(mockBuilding, 0.15f);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Placed building");
            GameObject building = Instantiate(buildingBlocks[selectedBuilding], position, Quaternion.identity);
            building.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(mockBuilding);
            OnCard?.Invoke();
        }
    }

    public void Play()
    {
        isInBuildMode = false;
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            block.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    
}