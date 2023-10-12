using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;
    public float walkspeed = 2.4f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 5.0f;
    public float rollVelocity = 3.0f;
    public float jabMultiplier = 1.2f;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;

    private bool lockPlanar = false;

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
        
        if(rigid.velocity.magnitude > 0.8f)
        {
            anim.SetTrigger("roll");
        }
        if (pi.Jump)
        {
            anim.SetTrigger("jump");
        }

        if (pi.Dmag > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            model.transform.forward = targetForward;
        }

        if(lockPlanar == false)
        {
            planarVec = pi.Dmag * model.transform.forward * walkspeed * ((pi.Run) ? runMultiplier : 1.0f);
        }
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }

    /// <summary>
    ///  Message processing block
    /// </summary>

    public void IsGround()
    {
        anim.SetBool("IsGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("IsGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnabled = true;
        lockPlanar = false;
    }

    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnFallEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnJabEnter()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }
    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity") * jabMultiplier;
    }
}
