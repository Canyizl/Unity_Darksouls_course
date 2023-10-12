using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkspeed = 2.0f;
    public float runMultiplier = 2.0f;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;

    private void Awake()
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float targetRunMulti = (pi.Run) ? 2.0f : 1.0f;
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp( anim.GetFloat("forward"), targetRunMulti, 0.5f));

        if (pi.Dmag > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            model.transform.forward = targetForward;
        }
        movingVec = pi.Dmag * model.transform.forward * walkspeed * ((pi.Run) ? runMultiplier : 1.0f);
    }

    private void FixedUpdate()
    {
        rigid.position += movingVec * Time.fixedDeltaTime;
    }
}
