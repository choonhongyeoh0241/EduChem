using UnityEngine;
using System;
using TMPro;

public class CrateManager : MonoBehaviour
{
    private static Action<string> OnRequestCrate;
    public static void RaiseSign(string text) => OnRequestCrate?.Invoke(text);
    [SerializeField] private GameObject crateSign;
    [SerializeField] private TextMeshProUGUI crateText;

    private void OnEnable() => OnRequestCrate += ActivateSign;
    private void OnDisable() => OnRequestCrate -= ActivateSign;
    private void ActivateSign(string signText)
    {
        if (signText == null) DeactivateSign();
        else
        {
            crateSign.SetActive(true);
            crateText.text = signText;
        }
       
    }

    private void DeactivateSign()
    {
        crateText.text = String.Empty;
        crateSign.SetActive(false);
    }
}
