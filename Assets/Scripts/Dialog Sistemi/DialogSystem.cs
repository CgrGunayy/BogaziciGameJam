using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public bool IsDialogOnGoing { get; private set; }

    [SerializeField] private DialogContainer dialogContainer;

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Image speakerImageHolder;
    [SerializeField] private Text speakerNameHolder;
    [SerializeField] private Text speechText;
    [SerializeField] private GameObject nextButtonVisual;

    private Dialog currentDialog;
    private int currentDialogIndex;
    private int currentSpeechIndex;

    private WaitForButton waitForContinueButton;
    private WaitForSeconds speechTransitionDelay;

    private void Awake()
    {
        waitForContinueButton = new WaitForButton("InteractionButton");
        speechTransitionDelay = new WaitForSeconds(0.1f);

        dialogContainer.spokenDialogs = new List<int>();
    }

    public void StartDialog(int index)
    {
        currentDialog = dialogContainer.dialogs[index];

        if (IsDialogOnGoing)
            return;

        if (currentDialog.onlyOneDialog && dialogContainer.spokenDialogs.Contains(index))
            return;

        GameManager.PlayerMovement.SetCanWalk(false);

        currentDialogIndex = index;
        currentSpeechIndex = 0;
        IsDialogOnGoing = true;

        StartCoroutine(ProcessCurrentDialog());
    }

    private IEnumerator ProcessCurrentDialog()
    {
        dialogPanel.SetActive(true);

        int dialogLastIndex = currentDialog.dialog.Length - 1;
        while(currentSpeechIndex <= dialogLastIndex)
        {
            if(currentSpeechIndex > 0)
                yield return speechTransitionDelay;

            Speech currentSpeech = currentDialog.dialog[currentSpeechIndex];

            speakerImageHolder.sprite = currentSpeech.speaker.personImage;
            speakerNameHolder.text = currentSpeech.speaker.personName;
            speechText.text = currentSpeech.content;
            nextButtonVisual.SetActive(currentSpeech.onAutomaticSkip ? false : true);

            if(currentSpeech.speechSound != null)
                GameManager.SoundManager.PlaySfx(currentSpeech.speechSound);

            if (currentSpeech.onAutomaticSkip) 
                yield return currentSpeech.AutomaticSkipDelayer; // Kendi kendine geçme
            else
                yield return waitForContinueButton; // Tuşa basınca geçme

            currentSpeechIndex += 1;
        }

        currentDialog.dialogFinishEvent?.Invoke();
        FinishCurrentDialog();
    }

    private void FinishCurrentDialog()
    {
        GameManager.PlayerMovement.SetCanWalk(true);
        dialogContainer.spokenDialogs.Add(currentDialogIndex);

        currentDialog = null;
        IsDialogOnGoing = false;
        dialogPanel.SetActive(false);
    }

    public bool CanStartDialog(int index)
    {
        currentDialog = dialogContainer.dialogs[index];
        return !(currentDialog.onlyOneDialog && dialogContainer.spokenDialogs.Contains(index));
    }
}
