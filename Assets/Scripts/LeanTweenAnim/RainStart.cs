using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RainStart : MonoBehaviour
{
    [SerializeField] float delay;
    public Animator animator;

    private bool menuChanged;
    
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (menuChanged)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("StartAnim", delay);
            AudioManager.instance.ChangeMainMenuMusic();
            menuChanged = true;
        }
    }
    void StartAnim()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
