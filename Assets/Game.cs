using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    #region Singleton
    public static Game instance;
    private void Awake()
	{
        instance = this;
	}
	#endregion

	public GameObject PlayerGoal;
    public GameObject CPUGoal;

    public GameObject ballPrefab;

    public GameObject PlayerPaddle;
    public GameObject CPUPaddle;

    public List<GameObject> _currentBalls;

    [Header("UI references")]
    [SerializeField] Text playerScoreText;
    [SerializeField] Text cpuScoreText;

    [Header("Sound references")]
    [SerializeField] AudioSource winAudioSource;
    [SerializeField] AudioSource loseAudioSource;
    
    int playerScore = 0;
    int CPUScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        _currentBalls = new List<GameObject>();
        _currentBalls.Add(Instantiate(ballPrefab, new Vector2(0, 0), Quaternion.Euler(0,0,0)));
    }

    public void AddScore(bool isPlayerScore)
	{
        if (isPlayerScore)
            playerScore++;
        else
            CPUScore++;

        while(_currentBalls.Count > 0)
		{
            Destroy(_currentBalls[0]);
            _currentBalls.RemoveAt(0);
		}

        if (isPlayerScore)
            winAudioSource.Play();
        else
            loseAudioSource.Play();

        // Add a new ball after a short delay
        StartCoroutine(nameof(SpawnNewBall));

        // Update UI
        playerScoreText.text = playerScore.ToString();
        cpuScoreText.text    = CPUScore.ToString();

        if (playerScore >= 3 || CPUScore >= 3)
            SceneManager.LoadScene("EndGame");
	}

    public void SpawnNewBallImmediate(Vector2 position)
	{
        _currentBalls.Add(Instantiate(ballPrefab, position, Quaternion.Euler(0, 0, 0)));
    }

    IEnumerator SpawnNewBall()
	{
        yield return new WaitForSeconds(2f);
        _currentBalls.Add(Instantiate(ballPrefab, new Vector2(0, 0), Quaternion.Euler(0, 0, 0)));
    }
}
