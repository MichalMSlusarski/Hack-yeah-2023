using UnityEngine;
public class Block : MonoBehaviour
{
    public int x;
    public int y;
    public int z;

    public bool CanPlaceOnTopOf(Block otherBlock)
    {
        if (otherBlock == null) { return true; }
        if (otherBlock.y < y) { return false; }
        if (otherBlock.y > y) { return true; }
        if (otherBlock.x == x && otherBlock.z == z) { return false; }
        return true;
    }
}