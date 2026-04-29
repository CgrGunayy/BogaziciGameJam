using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordMiniGame : MonoBehaviour
{
    private enum MiniGameState
    {
        Typing,
        WaitingForInput,
        Win,
        None
    }

    [SerializeField] private Sentence[] sentences;
    [SerializeField] private float wordFallSpeed;
    [SerializeField] private float typeDelay;
    [SerializeField] private float wordsStartY;
    [Space]
    [SerializeField] private RectTransform wordsParent;
    [SerializeField] private RectTransform sentencePanel;
    [SerializeField] private GameObject angryIcon;
    [Space]
    [SerializeField] private Text sentenceTextHolder;
    [SerializeField] private Text word01Holder;
    [SerializeField] private Text word02Holder;
    [SerializeField] private Text word03Holder;
    [Space]
    [Header("Ses")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;

    private MiniGameState currentState;
    private WaitForSeconds typeDelayer;
    private int currentSentenceIndex = 0;
    private SceneTransitionEffectHandler sceneTransitionEffectHandler;

    private void Awake()
    {
        sentenceTextHolder.text = "";
        typeDelayer = new WaitForSeconds(typeDelay);
        sceneTransitionEffectHandler = GameObject.FindObjectOfType<SceneTransitionEffectHandler>();

        GameManager.PlayerMovement?.SetCanWalk(false);
    }

    private void Start()
    {
        sceneTransitionEffectHandler.FadeOut(true);

        if(FindObjectOfType<AudioListener>() == null)
            gameObject.AddComponent<AudioListener>();
    }

    private void Update()
    {
        if(currentState == MiniGameState.WaitingForInput)
        {
            wordsParent.position -= Vector3.up * wordFallSpeed * Time.deltaTime;

            if(wordsParent.anchoredPosition.y <= -240)
            {
                RetrySentence();
            }
        }
    }

    public void StartMinigame()
    {
        currentSentenceIndex = 0;
        sentencePanel.gameObject.SetActive(true);
        WriteSentence();
    }

    public void OnWordInputEntered(string input)
    {
        if (input == "")
            return;

        if(input.ToLower() == sentences[currentSentenceIndex].blankWord.ToLower())
        {
            if(currentSentenceIndex < sentences.Length - 1)
            {
                NextSentence();
            }
            else
            {
                Win();
            }
        }
        else
        {
            RetrySentence();
        }
    }

    private void Win()
    {
        angryIcon.SetActive(false);
        SetState(MiniGameState.Win);
        GameManager.PlayerMovement?.SetCanWalk(true);
        GameManager.LoadGameScene();
    }

    private void WriteSentence()
    {
        sentenceTextHolder.text = "";
        string sentence = sentences[currentSentenceIndex].content;
        sentence = sentence.Replace(sentences[currentSentenceIndex].blankWord, "...");
        StartCoroutine(TypeEffect(sentence, StartWordFall));
    }

    private void NextSentence()
    {
        sfxSource.PlayOneShot(correctSound, 1);
        angryIcon.SetActive(false);
        wordsParent.anchoredPosition = new Vector3(0, wordsStartY, 0);
        currentSentenceIndex += 1;
        WriteSentence();
    }

    private void RetrySentence()
    {
        sfxSource.PlayOneShot(incorrectSound,1);
        angryIcon.SetActive(true);
        wordsParent.anchoredPosition = new Vector3(0, wordsStartY, 0);
        WriteSentence();
    }

    private void StartWordFall()
    {
        int random = Random.Range(0, 3);
        Sentence currentSentence = sentences[currentSentenceIndex];

        switch (random)
        {
            case 0:
                word01Holder.text = currentSentence.blankWord;
                word02Holder.text = currentSentence.possible01;
                word03Holder.text = currentSentence.possible02;
                break;
            case 1:
                word01Holder.text = currentSentence.possible01;
                word02Holder.text = currentSentence.blankWord;
                word03Holder.text = currentSentence.possible02;
                break;
            case 2:
                word01Holder.text = currentSentence.possible01;
                word02Holder.text = currentSentence.possible02;
                word03Holder.text = currentSentence.blankWord;
                break;
        }

        SetState(MiniGameState.WaitingForInput);
    }

    private IEnumerator TypeEffect(string text, System.Action afterTypeEffectAction = null)
    {
        if (currentState == MiniGameState.Typing)
            yield return null;

        SetState(MiniGameState.Typing);
        char[] chars = text.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            sentenceTextHolder.text += chars[i];
            yield return typeDelayer;
        }

        SetState(MiniGameState.None);
        afterTypeEffectAction?.Invoke();
    }

    private void SetState(MiniGameState state)
    {
        currentState = state;
        switch (currentState)
        {
            case MiniGameState.Win:
                wordsParent.anchoredPosition = new Vector3(0, wordsStartY, 0);
                sentenceTextHolder.enabled = false;
                sentencePanel.gameObject.SetActive(false);
                break;
        }
    }

}

[System.Serializable]
public class Sentence
{
    public string content;
    public string blankWord;
    public string possible01;
    public string possible02;
}