using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class ClearInput : MonoBehaviour
{
    private InputSystemUIInputModule _inputModule;
    private void Start()
    {
        _inputModule = GetComponent<InputSystemUIInputModule>();
    }

    public void DeactivateMethod()
    {
        _inputModule.DeactivateModule();
    }

    public void DeactivateComponent()
    {
        _inputModule.enabled = false;
        StartCoroutine(EnableAgain());
    }
    
    public void ActivateMethod()
    {
        _inputModule.ActivateModule();
    }

    public void ActivateComponent()
    {
        _inputModule.enabled = true;
    }

    private IEnumerator EnableAgain()
    {
        yield return new WaitForSeconds(3);

        ActivateComponent();
    }
    
    
}
