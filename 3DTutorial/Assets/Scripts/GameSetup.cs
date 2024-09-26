using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GameSetup : MonoBehaviour
{
    
    int redBallsRemaining = 7;
    int blueBallsRemaining = 7;
    float ballRadius;
    float ballDiameter;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform headBallPosition;

    // Start is called before the first frame update
    void Start()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 100f;
        ballDiameter = ballRadius + 2f;
        PlaceAllBalls();
    }

    void PlaceAllBalls(){
        PlaceCueBall();
        PlaceRandomBalls();
    }

    void PlaceCueBall(){
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);

    }

    void PlaceEightBall(UnityEngine.Vector3 position){
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent <Ball>().MakeEightBall();
    }

    void PlaceRandomBalls(){
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPosition.position;
        Vector3 currentPosition = firstInRowPosition;

        void PlaceRedBall(Vector3 position) {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;
        }

        void PlaceBlueBall(Vector3 position) {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            blueBallsRemaining--;
        }
    }
}
