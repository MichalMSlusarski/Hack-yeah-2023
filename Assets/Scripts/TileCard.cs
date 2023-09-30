using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TileCard : MonoBehaviour
{
    public string defaultTag = "Deck";
    string selectedTag = "SelectedCard";
    float desiredSize = 1.15f;
    float lerpTime = 0.4f;
    float initialScale = 1f;
    bool isSelected = false;

    public GameObject prefab;
    [SerializeField] SO_Bool isBuildMode;
    [SerializeField] GameObject icons;
    [SerializeField] Material selectedMaterial;
    BuildingManager BuildingManager;
    Image image;
    Material material;

    string thisCardName;

    void Awake()
    {
        thisCardName = gameObject.name;
        gameObject.transform.tag = defaultTag;
        icons.SetActive(false);
        image = gameObject.GetComponent<Image>();
    }

    void Start()
    {
        material = image.material;
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        BuildingManager = player[0].GetComponent<BuildingManager>();
        BuildingManager.OnCard += Deselect;
    }
    
    public void Selected()
    {
        Debug.Log("Selected: " + thisCardName);
        ClearSelection(thisCardName);
        icons.SetActive(true);
        isSelected = true;
        Deck.activeCard = this;
        gameObject.transform.tag = selectedTag;
        gameObject.transform.DOScale(desiredSize, lerpTime);

        WaitManager.Wait(0.1f, () =>
        {
            isBuildMode.boolvalue = true;
        }); 
    }

    void ClearSelection(string name)
    {
        // if(Input.GetMouseButton(0)) // prevents from deselecting with other buttons
        // {
            GameObject[] selectedCards = GameObject.FindGameObjectsWithTag(selectedTag);

            for (int i = 0; i < selectedCards.Length; i++)
            {
                if(selectedCards[i].name != thisCardName)
                {
                    TileCard selectedTileCard = selectedCards[i].GetComponent<TileCard>();
                    selectedTileCard.Deselect();
                }
            }
        // }   
    }

    public void Deselect()
    {
        isSelected = false;
        WaitManager.Wait(0.1f, () => 
        {
            if (defaultTag == "Card")
            {
                gameObject.transform.tag = "Card";
                isBuildMode.boolvalue = false;
                Exit();
            }
        });
    }

    public void Enter()
    {
        image.material = selectedMaterial;
        // BuildingManager.DestroyMock();
        icons.SetActive(true);
        gameObject.transform.DOScale(desiredSize, lerpTime);
    }

    public void Exit()
    {
        if(isSelected == false) {gameObject.transform.DOScale(initialScale, lerpTime); icons.SetActive(false); image.material = material;}
    }

    public void DropDead()
    {
        Deselect();
        gameObject.transform.DOScale(0.1f, 0.10f);
        Destroy(gameObject, 0.21f); // this value cannot be smaller than any other lerp duration
    }

    void OnDestroy()
    {
        BuildingManager.OnCard -= Deselect;
        DOTween.KillAll();
    }
}
