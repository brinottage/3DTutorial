using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private bool isRed;
    public bool is8Ball = false;
    public bool isCueBall = false;
    
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FiredUpdate(){
        if (rb.velocity.y > 0){
            Vector3 newvelocity = rb.velocity;
            newvelocity.y = 0f;
            rb.velocity = newvelocity;
        }
    }

    public bool isBallRed(){
        return isRed;
    }

    public void MakeCueBall(){
        isCueBall = true;
    }

    public void MakeEightBall()
    {
        is8Ball = true;
        GetComponent<Renderer>().material.color = Color.black;
    }

    public void BallSetup(bool red){
        isRed = red;
        if (isRed)
        {
            GetComponent<Renderer>().material.color = Color.red;
        } else {

            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

}
