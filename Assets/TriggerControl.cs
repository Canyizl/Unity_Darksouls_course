using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{

    private Animator anim;
    public void ResetTrigger(string triggerName)
    {
        anim.ResetTrigger(triggerName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
