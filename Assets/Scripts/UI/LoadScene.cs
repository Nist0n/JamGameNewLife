using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Time());
    }

    IEnumerator Time()
    {
        yield return new WaitForSeconds(3f);
        UnityEngine.Time.timeScale = 1;
        SceneManager.LoadScene("Base");
    }
}
