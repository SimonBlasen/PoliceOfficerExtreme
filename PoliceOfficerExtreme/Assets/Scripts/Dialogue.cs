using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMesh = null;
    [SerializeField]
    private float secondsPerLetter = 0.1f;


    private float counter = 0f;

    private string textRem = "";


    // Start is called before the first frame update
    void Start()
    {
        textMesh.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 0f)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            if (textRem.Length > 0)
            {
                textMesh.text += textRem.Substring(0, 1);
                textRem = textRem.Substring(1);

                counter = secondsPerLetter;
            }
        }
    }

    public void ShowDialogueText(string text)
    {
        textRem = text;
        textMesh.text = "";
        counter = 0f;
    }

    public void StopShowingText()
    {
        textRem = "";
        textMesh.text = "";
    }
}
