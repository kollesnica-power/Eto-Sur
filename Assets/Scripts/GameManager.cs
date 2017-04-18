using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private float m_StartDelay = 3f;                       // The delay between the start of RoundStarting and RoundPlaying phases.
    [SerializeField] private float m_EndDelay = 3f;                         // The delay between the end of RoundPlaying and RoundEnding phases.
    [SerializeField] private CameraController m_CameraControl;              // Reference to the CameraControl script for control during different phases.
    [SerializeField] private Text m_RoundText;                              // Reference to the overlay Text to display round number.
    [SerializeField] private Text m_TimerText;                              // Reference to the overlay Text to display time left.
    [SerializeField] private GameObject m_PlayerPrefab;                     // Reference to the prefab the players will control.
    [SerializeField] public PlayerManager[] m_Players;                      // A collection of managers for enabling and disabling different aspects of the players.

    [Header("Pizdec")]
    [Space]
    [SerializeField] private GameObject m_Laser;
    [SerializeField] private GameObject m_Bomber;
    [SerializeField] private GameObject m_Freezer;
    [SerializeField] private GameObject m_Healer;

    [Space]
    [SerializeField] private float m_TimeToSurvive;


    private int m_RoundNumber;                  // Which round the game is currently on.
    private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
    private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
    private PlayerManager m_RoundWinner;        // Reference to the winner of the current round.  Used to make an announcement of who won.
    private PlayerManager m_GameWinner;         // Reference to the winner of the game.  Used to make an announcement of who won.

    private float m_Timer;                      // The timer of the round.
    private GameState m_CurGameState;           // Game state info.

    private enum GameState { RoundStarting, RoundPlaying, RoundEnding }



    private void Start() {
        // Create the delays so they only have to be made once.
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        m_Timer = m_TimeToSurvive;

        SpawnAllPlayers();
        SetCameraTargets();

        // Once the players have been created and the camera is using them as targets, start the game.
        StartCoroutine(GameLoop());
    }

    private void Update() {

        if (m_CurGameState == GameState.RoundPlaying) {
            m_Timer -= Time.deltaTime;
            m_TimerText.text = "TIMER: " + (int)m_Timer; 
        }

    }


    private void SpawnAllPlayers() {

        // For all the players...
        for (int i = 0; i < m_Players.Length; i++) {
            // ... create them, set their player number and references needed for control.
            m_Players[i].m_Instance =
                Instantiate(m_PlayerPrefab, m_Players[i].m_SpawnPoint.position, m_Players[i].m_SpawnPoint.rotation) as GameObject;
            m_Players[i].m_PlayerNumber = i + 1;
            m_Players[i].Setup();
        }

    }


    private void SetCameraTargets() {

        // Create a collection of transforms the same size as the number of players.
        Transform[] targets = new Transform[m_Players.Length];

        // For each of these transforms...
        for (int i = 0; i < targets.Length; i++) {
            // ... set it to the appropriate player transform.
            targets[i] = m_Players[i].m_Instance.transform;
        }

        // These are the targets the camera should follow.
        m_CameraControl.targets = targets;

    }


    // This is called from start and will run each phase of the game one after another.
    private IEnumerator GameLoop() {

        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundStarting());

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundPlaying());

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        yield return StartCoroutine(RoundEnding());

        StartCoroutine(GameLoop());
        
    }


    private IEnumerator RoundStarting() {
        // As soon as the round starts reset the tanks and make sure they can't move.
        ResetAllPlayers();
        DisablePlayerControl();
        m_CurGameState = GameState.RoundStarting;

        // Snap the camera's zoom and position to something appropriate for the reset tanks.
        m_CameraControl.SetStartPositionAndSize();

        // Increment the round number and display text showing the players what round it is.
        m_RoundNumber++;
        m_RoundText.text = "ROUND: " + m_RoundNumber;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying() {

        // As soon as the round begins playing let the players control the tanks.
        EnablePlayerControl();
        EnablePizdec();
        m_CurGameState = GameState.RoundPlaying;

        // While has time left and at least one player alive.
        while (m_Timer > 0 && !IsGameOver()) {
            // ... return on the next frame.
            yield return null;
        }

    }


    private IEnumerator RoundEnding() {

        // Stop tanks from moving.
        DisablePlayerControl();
        DisablePizdec();
        m_CurGameState = GameState.RoundEnding;

        // Check neither time's left or both players are dead.
        if (IsGameOver()) {
            m_RoundNumber = 0;
        }

        // Reset timer.
        m_Timer = m_TimeToSurvive;
        m_TimerText.text = "TIMER: " + (int)m_Timer;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return m_EndWait;

    }


    // This is used to check if there is no one alive and thus the round should end.
    private bool IsGameOver() {
        // Start the count of tanks left at zero.
        int numPlayersLeft = 0;

        // Go through all the tanks...
        for (int i = 0; i < m_Players.Length; i++) {
            // ... and if they are active, increment the counter.
            if (m_Players[i].m_Instance.activeSelf)
                numPlayersLeft++;
        }

        // If there are no one return true, otherwise return false.
        return numPlayersLeft == 0;
    }



    // This function is used to turn all the players back on and reset their positions and properties.
    private void ResetAllPlayers() {

        for (int i = 0; i < m_Players.Length; i++) {
            m_Players[i].Reset();
        }

    }


    private void EnablePlayerControl() {

        for (int i = 0; i < m_Players.Length; i++) {
            m_Players[i].EnableControl();
        }

    }


    private void DisablePlayerControl() {

        for (int i = 0; i < m_Players.Length; i++) {
            m_Players[i].DisableControl();
        }

    }

    private void EnablePizdec() {

        m_Laser.SetActive(true);
        m_Bomber.SetActive(true);
        m_Freezer.SetActive(true);
        m_Healer.SetActive(true);

    }

    private void DisablePizdec() {

        m_Laser.SetActive(false);
        m_Bomber.SetActive(false);
        m_Freezer.SetActive(false);
        m_Healer.SetActive(false);

    }

}