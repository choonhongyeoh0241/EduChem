using System.Collections;
using UnityEngine;
using System;
using Ink.Runtime;
using UnityEngine.UI;
using Pause;

public class DialogueChoiceManager : MonoBehaviour
{
    private static Action<TextAsset> OnDialogueChoiceRequested;
    public static void RequestChoiceDialog(TextAsset inkData) => OnDialogueChoiceRequested?.Invoke(inkData);
    [SerializeField] private GameObject dialogueChoicePanel;
    [SerializeField] private Image dialogueBG;
    [SerializeField] private GameObject dialogPrefab;
    [SerializeField] private GameObject responsePrefab;
    [SerializeField] private GameObject dialogHolder;
    [SerializeField] private GameObject choiceHolder;
    [SerializeField] private ScrollRect dialogScroll;
    private TextAsset currentValue;
    private Story myStory;

    private void OnEnable() => OnDialogueChoiceRequested += ActiveCanvas;
    private void OnDisable() => OnDialogueChoiceRequested -= ActiveCanvas;

    private void ActiveCanvas(TextAsset inkData)
    {
        if (inkData != null)
        {
            currentValue = inkData;

            dialogueChoicePanel.SetActive(true);
            dialogueBG.gameObject.SetActive(true);
            SetStory(currentValue);
            RefreshView();
        }
        else
        {
            CloseCanvas();
        }
    }

    private void SetStory(TextAsset value)
    {
        if(currentValue != null)
        {
            myStory = new Story(value.text);
        }
    }

    private void RefreshView()
    {
        while(myStory.canContinue)
        {
            OnInput(myStory.Continue());
        }

        if (myStory.currentChoices.Count > 0)
        {
            ChoiceInput();
        }
        else
        {
            CloseCanvas();
        }

        StartCoroutine(ScrollCo());
    }

    private IEnumerator ScrollCo()
    {
        yield return null;
        dialogScroll.verticalNormalizedPosition = 0f;
    }

    private void OnInput(string newDialog)
    {
        DialogObject newDialogObject = Instantiate(dialogPrefab, dialogHolder.transform).GetComponent<DialogObject>();
        newDialogObject.Setup(newDialog);
    }

    private void OnResponseInput(string newDialog, int choiceValue)
    {
        ResponseObject newResponseObject = Instantiate(responsePrefab, choiceHolder.transform).GetComponent<ResponseObject>();
        newResponseObject.Setup(newDialog, choiceValue);

        Button responseButton = newResponseObject.gameObject.GetComponent<Button>();

        // Dynamically have the buttons to listen to this event since no fixed button count declared
        if(responseButton)
        {
            responseButton.onClick.AddListener(delegate {OnChoiceInput(choiceValue);});
        }
    }
    private void ChoiceInput()
    {
        for(int i = 0; i < choiceHolder.transform.childCount; i++)
        {
            Destroy(choiceHolder.transform.GetChild(i).gameObject);
        }

        for(int i = 0; i < myStory.currentChoices.Count; i++)
        {
            OnResponseInput(myStory.currentChoices[i].text, i);
        }
    }

    private void OnChoiceInput(int choice)
    {
        myStory.ChooseChoiceIndex(choice);
        RefreshView();
    }

    private void CloseCanvas()
    {
        for(int i = 0; i < dialogHolder.transform.childCount; i++)
        {
            Destroy(dialogHolder.transform.GetChild(i).gameObject);
        }
        dialogueChoicePanel.SetActive(false);
        dialogueBG.gameObject.SetActive(false);
    }
}
