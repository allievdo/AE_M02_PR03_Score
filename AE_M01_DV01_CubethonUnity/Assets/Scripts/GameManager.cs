using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public GameObject completeLevelUI;

    public GameObject changeColorUI;

    GameObject player;

    public GameObject CheckpointUI;

    private PlayerMovement _player;

    public AudioSource crash;

    private void OnEnable()
    {
        PlayerCollision.OnHitObstacle += EndGame;
        Checkpoint.OnCheckpoint += Checkpoint_OnCheckpointEntered;
        ColorChange.OnHitColorChange += ColorChange_OnHitColorChange;
        PlaySound.OnHitSoundPlay += PlaySound_OnHitSoundPlay;
        ChangeSceneColor.OnChangeSceneColor += ChangeSceneColor_OnChangeSceneColorEntered;
    }


    private void OnDisable()
    {
        PlayerCollision.OnHitObstacle -= EndGame;
        Checkpoint.OnCheckpoint -= Checkpoint_OnCheckpointEntered;
        ColorChange.OnHitColorChange -= ColorChange_OnHitColorChange;
        PlaySound.OnHitSoundPlay -= PlaySound_OnHitSoundPlay;
        ChangeSceneColor.OnChangeSceneColor -= ChangeSceneColor_OnChangeSceneColorEntered;
    }
    void Start()
    {
        PlayerPrefs.DeleteAll();

        _player = FindObjectOfType<PlayerMovement>();
        player = _player.gameObject;

        //CHECKPOINT
        //Checkpoint.OnCheckpoint += Checkpoint_OnCheckpointEntered;
    }

    private void ChangeSceneColor_OnChangeSceneColorEntered (ChangeSceneColor change)
    {
        string checkpointAppear = "Checkpoint-" + change.PoiName2;

        if (change != null)
        {
            Debug.Log("Change Color");
            changeColorUI.SetActive(true);
        }
    }

    //PLAY SOUND
    private void PlaySound_OnHitSoundPlay (Collision playSound)
    {
        //player.GetComponent<PlayerMovement>().enabled = false;

        if (playSound != null)
        {
            crash.Play();
        }
    }

    //CHANGE COLOR ON COLLISION
    private void ColorChange_OnHitColorChange (Collision colorChange)
    {
        //ColorChange.OnHitColorChange -= ColorChange_OnHitColorChange;

        if (colorChange != null)
        {
            //Change color
            _player.ren.material.color = Color.white;
        }
    }
    private void Checkpoint_OnCheckpointEntered(Checkpoint checkpoint)
    {
        string checkpointAppear = "Checkpoint-" + checkpoint.PoiName;

        if (PlayerPrefs.GetInt(checkpointAppear) == 1)
        {
            return;
        }

        PlayerPrefs.SetInt(checkpointAppear, 1);
        Debug.Log("Unlocked " + checkpoint.PoiName);
        CheckpointUI.SetActive(true);
    }
    public void CompleteLevel()
    {
        Debug.Log("Level won");
        completeLevelUI.SetActive(true);
    }

    public void EndGame(Collision collisionInfo)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        PlayerCollision.OnHitObstacle -= EndGame;

        if (collisionInfo != null)
        {
            Debug.Log("Hit: " + collisionInfo.collider.name);
        }

        // this flag prevents responding to multiple hit events:
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}