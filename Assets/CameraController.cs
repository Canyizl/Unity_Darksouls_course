using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public PlayerInput pi;
    public float horizontalSpeed;
    public float verticalSpeed;
    public float reversalVert;
    public float cameraDampValue;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private float tempEulerX;
    private GameObject model;
    private GameObject cameraObj;
    // Start is called before the first frame update

    private Vector3 cameraDampVelocity;

    private void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;
        model = playerHandle.GetComponent<ActorController>().model;
        cameraObj = Camera.main.gameObject;
    }
    void Start()
    {
        
    }

    private void Update()
    {
        
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime * reversalVert;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

        model.transform.eulerAngles = tempModelEuler;

        cameraObj.transform.position = Vector3.SmoothDamp( cameraObj.transform.position, transform.position, ref cameraDampVelocity ,cameraDampValue);
        cameraObj.transform.eulerAngles = transform.eulerAngles;
    }
}
