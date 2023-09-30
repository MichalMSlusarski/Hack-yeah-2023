using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntToText : MonoBehaviour
{
    [SerializeField] SO_Integer integer;
    [SerializeField] bool padWithZeros = false;
    [SerializeField] bool addPlus = false;
    TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        TextChange();
    }

    void TextChange()
    {
        if(padWithZeros)
        {
            textMeshPro.text = integer.Integer.ToString("00");
        }
        else
        {
            if(addPlus && integer.Integer >= 0)
            {
                textMeshPro.text = "+" + integer.Integer.ToString();
            }
            else
            {
                textMeshPro.text = integer.Integer.ToString();
            }
        }
    }
}
