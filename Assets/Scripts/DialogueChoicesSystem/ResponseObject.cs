using UnityEngine;
using TMPro;

public class ResponseObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText;
    private int choiceValue;
    public void Setup(string newDialog, int myChoice)
    {
        myText.text = newDialog;
        choiceValue = myChoice;
    }
}
