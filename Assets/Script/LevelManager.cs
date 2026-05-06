using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("LevelData")]
    [SerializeField] private LoadData[] levels;
    [SerializeField] private int currentLevelIndex = 0;

    [Header("Prefabs")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject portalExit;
    [SerializeField] private GameObject coinPrefabs;
    [SerializeField] private GameObject enemiesPrefabs;

    public static LevelManager  Instance;

    private GameObject loadedLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    



    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Length)
        {
            return;
        }
        currentLevelIndex = levelIndex;
        LoadData loadData = levels[currentLevelIndex];

        Clear();

        
        loadedLevel = Instantiate(loadData.levelPrefabs);
        player.transform.position = loadData.spawnPlayerPosition;


        portalExit.transform.position = loadData.exitPosition;

        

        foreach (Vector2 pos in loadData.coin)
        {
            Instantiate(coinPrefabs, pos, Quaternion.identity);
        }
        foreach (Vector2 pos in loadData.enemies)
        {
            Instantiate(enemiesPrefabs, pos, Quaternion.identity);
        }
    

    }

    public  void LoadNextLevel()
    {   
        int nextLevel = currentLevelIndex+ 1;
        if(nextLevel < levels.Length)
        {
            LoadLevel(nextLevel);

        }
        else
        {
            Debug.Log("No Levels");
        }

        
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }
    public void Clear()
    {
        if(loadedLevel != null)
        {
            Destroy(loadedLevel);
        }
        
    }


}
