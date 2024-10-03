using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    enum CurrentPlayer{
        Player1,
        Player2
    }

    CurrentPlayer currentPlayer;
    bool isWinningShotForPlayer1 = false;
    bool isWinningShotForPlayer2 = false;
    int player1BallsRemaining = 7;
    int player2BallsRemaining = 7;

    [SerializeField] TextMeshProUGUI player1BallsText;
    [SerializeField] TextMeshProUGUI player2BallsText;
    [SerializeField] TextMeshProUGUI currentTurnText;
    [SerializeField] TextMeshProUGUI messageText;


    [SerializeField] GameObject restartButton;
    [SerializeField] Transform headPosition;

    // Start is called before the first frame update
    void Start()
    {
        
        currentPlayer = CurrentPlayer.Player1;
        restartButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartTheGame(){
        SceneManager.LoadScene(0);
    }

    bool Scratch(){
        if (currentPlayer == CurrentPlayer.Player1){
            if (isWinningShotForPlayer1)
            {
                ScratchOnWinningShot("Player 1");
                return true;
            }
        } 
        else
        {
            if (isWinningShotForPlayer2) {
                ScratchOnWinningShot("Player 2");
                return true;
            }
        }
        NextPlayerTurn();
        return false;
    }

    void EarlyEightBall(){
        if (currentPlayer == CurrentPlayer.Player1){
            Lose("Player 1 Hit in the Eight Ball Too Early and Has Lost!");
        } else {
            Lose("Player 2 Hit in the Eight Ball Too Early and Has Lost!");
        }

    }

    void ScratchOnWinningShot(string player){
        Lose(player + " Scratched on Their Final Shot an Has Lost");
    }

    void NoMoreBalls(CurrentPlayer player){
        if (player == CurrentPlayer.Player1)
        {
            isWinningShotForPlayer2 = true;
        }
        else
        {
            isWinningShotForPlayer2 = true;
        }
    }

    bool CheckBall(Ball ball){
        if (ball.isCueBall){

            if (Scratch()){
                return true;
            } else {
                return false;
            }

        }
        else if (ball.is8Ball){
            if (currentPlayer == CurrentPlayer.Player1){
                if (isWinningShotForPlayer1){
                    Win("Player 1");
                    return true;
                }
            }
            else 
            {
                if (isWinningShotForPlayer2) {
                    Win("Player 2");
                    return true;
                }
            }
            EarlyEightBall();
        } 
        else {
            if (ball.isBallRed()) {
                player1BallsRemaining--;
                player1BallsText.text = "Player 1 Balls Remaining: " + player1BallsRemaining;
                if (player1BallsRemaining <= 0)
                {
                    isWinningShotForPlayer1 = true;
                }
                if (currentPlayer != CurrentPlayer.Player1)
                {
                    NextPlayerTurn();
                }
            } else {
                player2BallsRemaining--;
                player2BallsText.text = "Player 2 Balls Remaining: " + player2BallsRemaining;
                if (player2BallsRemaining <= 0) {
                    isWinningShotForPlayer2 = true;
                }
                if (currentPlayer != CurrentPlayer.Player2)
                {
                    NextPlayerTurn();
                }
            }
        }
        return true;
    }

    void Lose(string message){
        messageText.gameObject.SetActive(true);
        messageText.text = player1BallsRemaining + message;
        restartButton.SetActive(true);
    }

    void Win(string player){
        messageText.gameObject.SetActive(true);
        messageText.text = player1BallsRemaining + " Has Won!";
        restartButton.SetActive(true);

    }

    void NextPlayerTurn(){
        if (currentPlayer == CurrentPlayer.Player1) {
            currentPlayer = CurrentPlayer.Player2;
            currentTurnText.text = "Current Turn: Player 2";
        } else
        {
            currentPlayer = CurrentPlayer.Player1;
            currentTurnText.text = "Current Turn: Player 1";
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Ball"){
            if (CheckBall(other.gameObject.GetComponent<Ball>())){
                Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.transform.position = headPosition.position;
                other.gameObject.GetComponent<Rigidbody>().velocity = UnityEngine.Vector3.zero;
                other.gameObject.GetComponent<Rigidbody>().angularVelocity = UnityEngine.Vector3.zero;
            }
        }
    }



}
