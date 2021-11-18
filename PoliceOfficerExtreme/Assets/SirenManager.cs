using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenManager : MonoBehaviour
{
    public Animator animator;
    public TMPro.TextMeshProUGUI sirenTXT;
    public AudioSource audioSource;
    public AudioClip sirenClip;
    private bool isOn;
    public string[] sirenScreenTXTs;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Stop();
        audioSource.clip = sirenClip;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(isOn)
            {
                isOn = false;
                audioSource.Stop();
                animator.SetBool("isSiren", false);
                sirenTXT.text = sirenScreenTXTs[0];
            }
            else
            {
                isOn = true;
                audioSource.Play();
                animator.SetBool("isSiren", true);
                sirenTXT.text = sirenScreenTXTs[1];
            }
        }
    }
}
