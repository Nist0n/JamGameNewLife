using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private FaderExample _faderExample;

    void Start()
    {
        _faderExample = FindObjectOfType<FaderExample>();
        StartCoroutine(Time());
    }

    IEnumerator Time()
    {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.Time.timeScale = 1;
        _faderExample.LoadScene("Base");
    }
}
