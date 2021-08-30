using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public delegate void OnCoinDestroyed(Coin coin);
public delegate void OnPlayerScoreChanged(bool deduct, int score);

public class GameController : MonoBehaviour
{
    public int countdownTime;
    public int gameTime;

    public TMP_Text countdownText;
    public TMP_Text timerText;
    public SpawnCoin coinGenerator;
    public SpawnPlayer playerGenerator;
    public Canvas mathDialogCanvas;
    public GameObject scoreController;

    private MathDialog mathDialog;
    private GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        mathDialog = mathDialogCanvas.GetComponent<MathDialog>();
        mathDialog.Close();

        playerObj = playerGenerator.SpawnNewPlayer();
        Player player = playerObj.GetComponent<Player>();

        PhotonNetwork.LocalPlayer.SetScore(5);
        scoreController.GetComponent<ScoreController>().Init();

        OnCoinDestroyed coinDestroyedListener = CoinDestroyedListener;
        player.SetOnCoinDestroyedListener(coinDestroyedListener);

        OnPlayerScoreChanged onPlayerScoreChangedListener = PlayerScoreChangedListener;
        mathDialog.SetOnPlayerScoreChangedListener(onPlayerScoreChangedListener);

        AudioController.getInstance().PlaySong(AudioController.GameState.Game);
        timerText.SetText(secToString(gameTime));
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        coinGenerator.StartSpawnCoin();
        for (int i = gameTime; i >= 0; i--)
        {
            timerText.SetText(secToString(i));
            timerText.color = i <= 10 ? Color.red : Color.white;

            PhotonView photonView = PhotonView.Get(scoreController.GetComponent<ScoreController>());
            photonView.RPC("UpdateScore", RpcTarget.All);
            yield return new WaitForSeconds(1f);
        }

        timerText.SetText("");
        coinGenerator.StopSpawnCoin();

        countdownText.gameObject.SetActive(true);
        countdownText.text = "Time Up";

        new WaitForSeconds(3);
        SceneManager.LoadScene("leaderbroad");
    }

    private string secToString(int sec)
    {
        int minute = Mathf.FloorToInt(sec / 60);//get minute
        int second = Mathf.FloorToInt(sec % 60);//get second
        return string.Format("Time: {0:00}:{1:00}", minute, second); //display timer
    }

    public void CoinDestroyedListener(Coin coin)
    {
        playerObj.GetComponent<Player>().FreezePlayer();
        if (coin.IsPenalty())
            mathDialog.SetQuestion(PhotonNetwork.PlayerListOthers[0].GetScore(), coin.Op, coin.Operand, coin.IsPenalty());
        else
            mathDialog.SetQuestion(PhotonNetwork.LocalPlayer.GetScore(), coin.Op, coin.Operand, coin.IsPenalty());

        mathDialog.Exec();
    }

    public void PlayerScoreChangedListener(bool isPenalty, int score)
    {
        if (isPenalty)
            PhotonNetwork.PlayerListOthers[0].SetScore(score);
        else
            PhotonNetwork.LocalPlayer.SetScore(score);

        playerObj.GetComponent<Player>().UnfreezePlayer();
    }

}
