using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnim : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) animator.SetBool("isKeyDown", true);
    }
}
