using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainStart : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Invoke("StartAnim", 2);
    }
    void StartAnim()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
