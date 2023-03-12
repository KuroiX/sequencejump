using System.Collections;
using System.Collections.Generic;
using Core.Actions;
using Core.Queue;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpEffect : MonoBehaviour
{
    public ParticleSystem ps;
    private ResettableQueue<ICharacterAction> _queue;

    // Start is called before the first frame update
    void Start()
    {
        _queue = GetComponentInParent<QueueHolder>().Queue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_queue.IsEmpty() && Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            ps.Play();
        }
    }
}
