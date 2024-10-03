using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{

[SerializeField] float rotationSpeed;
[SerializeField] Vector3 offset;
[SerializeField] float downAngle;
[SerializeField] float power;
private float horizontalInput;

Transform cueBall;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball")) {
            if (ball.GetComponent<Ball>().isCueBall){
                cueBall = ball.transform;
                break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cueBall != null) {
            horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            ResetCamera();
        }

        if (Input.GetButtonDown("Fire1")){
            Vector3 hitDirection = transform.forward;
            hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;

            cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power, ForceMode.Impulse);
        }

    }

    public void ResetCamera(){
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new UnityEngine.Vector3(downAngle, transform.localEulerAngles.y, 0);



    }

}
