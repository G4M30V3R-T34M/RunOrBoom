using System.Collections;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    DialogScriptable dialog;

    [SerializeField]
    TextMeshProUGUI speaker, colon, content;

    [SerializeField]
    GameObject clickToContinue;

    Animator animator;

    bool writing = true;
    int dialogIndex = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        clickToContinue.SetActive(false);

        speaker.text = "";
        colon.text = "";
        content.text = "";
    }

    public void StartDialog()
    {
        colon.text = ":";
        WriteNext();
    }

    public void StartGame()
    {
        // TODO
        // SceneManager.Instance.LoadScene((int)Scenes.TutorialLevel);
    }

    private void Update()
    {
        if (!writing && Input.GetKeyDown(KeyCode.Mouse0))
        {
            WriteNext();
        }
    }

    private void WriteNext()
    {
        if (dialogIndex >= dialog.dialog.Length)
        {
            EndDialog();
        }
        else
        {
            writing = true;
            clickToContinue.SetActive(false);
            StartCoroutine(DoWriteNext(dialog.dialog[dialogIndex]));
            dialogIndex++;
        }
    }

    private IEnumerator DoWriteNext(DialogEntryScriptable entry)
    {
        string text = "";
        char[] contentArray = ReplaceInvestigatorName(entry.content).ToCharArray();
        int i = 0;

        this.speaker.text = ReplaceInvestigatorName(entry.speaker);
        this.content.text = text;

        while (i < contentArray.Length)
        {
            yield return new WaitForSeconds(dialog.waitTime);
            text += contentArray[i];
            this.content.text = text;
            i++;
        }
        writing = false;
        clickToContinue.SetActive(true);
    }

    private string ReplaceInvestigatorName(string text)
    {
        return text.Replace("NoName", "Protagonist");
        //return text.Replace("NoName", SaveDataManager.Instance.playerData.investigatorName);
    }

    private void EndDialog()
    {
        speaker.text = "";
        colon.text = "";
        content.text = "";
        clickToContinue.SetActive(false);
        animator.SetTrigger("Continue");
    }

}
