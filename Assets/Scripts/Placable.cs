using UnityEngine;

[System.Flags]
public enum Products
{
    Fields = 1 << 0,
    Woods = 1 << 1,
    Rocks = 1 << 2,
    Windmill = 1 << 3,
    Mine = 1 << 4,
    Town = 1 << 5,
    Sawmill = 1 << 6,
}

public class Placable : MonoBehaviour
{
    public Products productType;
    public Products bonusForTileWithThisProductType; //1001
    public Products negativeForTileWithThisProductType;
    public int bonusValue = 1;
    public int negativeValue = -1;
}
