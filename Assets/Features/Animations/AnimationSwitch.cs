using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSwitch : MonoBehaviour
{
    public GameObject animation1;
    public GameObject animation2;
    public GameObject animation3;
    public GameObject animation4;

    [ContextMenu("Switch to Animation 1")]
    public void SwitchToAnimation1()
    {
        animation1.SetActive(true);
        animation2.SetActive(false);
        animation3.SetActive(false);
        animation4.SetActive(false);
    }
    
    [ContextMenu("Switch to Animation 2")]
    public void SwitchToAnimation2()
    {
        animation1.SetActive(false);
        animation2.SetActive(true);
        animation3.SetActive(false);
        animation4.SetActive(false);
    }
    
    [ContextMenu("Switch to Animation 3")]
    public void SwitchToAnimation3()
    {
        animation1.SetActive(false);
        animation2.SetActive(false);
        animation3.SetActive(true);
        animation4.SetActive(false);
    }
    
    [ContextMenu("Switch to Animation 4")]
    public void SwitchToAnimation4()
    {
        animation1.SetActive(false);
        animation2.SetActive(false);
        animation3.SetActive(false);
        animation4.SetActive(true);
    }
    
}
