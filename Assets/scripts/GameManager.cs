using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public carcontroller RPM;

    public GameObject needle;
    private float startPos = 220f,endPos = -41f;
    private float desiredPos;
    public float vehicleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vehicleSpeed = RPM.speed;
        updateNeedle();
    }

    public void updateNeedle(){
        desiredPos = startPos - endPos;
        float temp = vehicleSpeed / 180;
        //to rotate the needle while the speed increase
        needle.transform.eulerAngles = new Vector3(0,0,(startPos - temp * desiredPos));
    }
}
