using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RainStart : MonoBehaviour
{
    [SerializeField] float delay;
    public Animator animator;
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Invoke("StartAnim", delay);
    }
    void StartAnim()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
