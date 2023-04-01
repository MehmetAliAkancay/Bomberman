using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public GameObject[] players;
    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            DestroyImmediate(gameObject);
    }
    public void CheckWinState(){
        int aliveCount = 0;
        foreach(GameObject player in players)
        {
            if(player.activeSelf){
                aliveCount++;
            }
        }
        if(aliveCount <= 1){
            Invoke(nameof(NewRound),3f);
        }
    }
    public void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
