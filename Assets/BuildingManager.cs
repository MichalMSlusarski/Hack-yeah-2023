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
            
            if (hit.transform.CompareTag("Tile") || hit.transform.CompareTag("Block"))
            {
                PlaceBuilding(hit.transform.gameObject);
            }
        }
    }

    private void PlaceBuilding(GameObject tile)
    {
        if (tile.transform.childCount > 0) { return; }

        Collider tileCollider = tile.GetComponent<Collider>();
        Collider buildingCollider = buildingBlocks[selectedBuilding].GetComponent<Collider>();
        float offset = tileCollider.bounds.extents.y + buildingCollider.bounds.extents.y;

        Vector3 position = new Vector3(tile.transform.position.x, tile.transform.position.y + offset, tile.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject building = Instantiate(buildingBlocks[selectedBuilding], position, Quaternion.identity);
            Debug.Log("Building placed at " + position);
            building.GetComponent<Rigidbody>().isKinematic = true;
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