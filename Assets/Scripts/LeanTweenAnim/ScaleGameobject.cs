using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleGameobject : MonoBehaviour
{
    public void ScaleUp()
    {
        gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    
    public void ScaleDown()
    {
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    
    public void ScaleUpMine()
    {
        gameObject.transform.localScale = new Vector3(250f, 250f, 250f);
    }
    
    public void ScaleDownMine()
    {
        gameObject.transform.localScale = new Vector3(200f, 200f, 200f);
    }
}
