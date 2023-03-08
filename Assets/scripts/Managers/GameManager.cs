using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public UnityEvent<int> onLifeValueChange;
    private static GameManager _instance = null;

    public static GameManager instance
    {
        get => _instance;
    }



    public int maxLives = 5;
    public int _Lives = 3;

    public int Lives
    {
        get { return _Lives; }
        set
        {
            if (_Lives > value)
                Respawn();


            _Lives = value;

            if (_Lives > maxLives)
                _Lives = maxLives;

            if (_Lives <= 0)
                SceneManager.LoadScene(2);

            onLifeValueChange?.Invoke(_Lives);

            Debug.Log("lives have been set to: " + _Lives.ToString());
        }
    }

    public PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance = null;
    [HideInInspector] public Level currentLevel = null;
    [HideInInspector] public Transform currentSpawnPoint;

    private void Awake()
    {
        if(_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance= this;
        DontDestroyOnLoad(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
       
        Lives = maxLives;
    }

    public void SpawnPlayer(Transform spawnPoint)
    {
        playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        currentSpawnPoint= spawnPoint;
    }

    void Respawn()
    {
        if (playerInstance)
            playerInstance.transform.position = currentSpawnPoint.position;
    }
    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex== 0) 
                SceneManager.LoadScene(1);
            else
                SceneManager.LoadScene(0);

            if (SceneManager.GetActiveScene().buildIndex==2)
                SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.K))
            Lives--;

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(SceneManager.GetActiveScene().buildIndex==1) 
                SceneManager.LoadScene(1);
        }

    }

    public void UpdateCheckpoint(Transform spawnPoint)
    {
        currentSpawnPoint = spawnPoint;
    }

}
