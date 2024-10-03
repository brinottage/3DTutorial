using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private bool isRed;
    public bool is8Ball = false;
    public bool isCueBall = false;
    

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
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
