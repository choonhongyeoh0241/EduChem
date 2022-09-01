using UnityEngine;
using TMPro;

public class DialogObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText;
    public void Setup(string newDialog)
    {
        myText.text = newDialog;
    }
}
