using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerManager {

    // This class is to manage various settings on a player.
    // It works with the GameManager class to control how the players behave
    // and whether or not players have control of their player in the 
    // different phases of the game.

    [SerializeField] private Color m_PlayerColor;                               // This is the color this player will be tinted.
    [SerializeField] public Transform m_SpawnPoint;                             // The position and direction the player will have when it spawns.

    public int m_PlayerNumber {set; get;}                                       // This specifies which player this the manager for.
    public string m_ColoredPlayerText {set; get;}                               // A string that represents the player with their number colored to match their player.
    public GameObject m_Instance {set; get;}                                    // A reference to the instance of the player when it is created.
    public int m_Wins {set; get;}                                               // The number of wins this player has so far.

    private PlayerMovement m_Movement;                                          // Reference to player's movement script, used to disable and enable control.
    private GameObject m_CanvasGameObject;                                      // Used to disable the world space UI during the Starting and Ending phases of each round.




    public void Setup() {

        // Get references to the components.
        m_Movement = m_Instance.GetComponent<PlayerMovement>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        // Set the player numbers to be consistent across the scripts.
        m_Movement.playerNumber = m_PlayerNumber;

        // Create a string using the correct color that says 'PLAYER 1' etc based on the player's color and the player's number.
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

        // Get all of the renderers of the player.
        SkinnedMeshRenderer[] renderers = m_Instance.GetComponentsInChildren<SkinnedMeshRenderer>();

        // Go through all the renderers and tint them
        for (int i = 0; i < renderers.Length; i++) {
            // ... set their material color to the color specific to this player.
            renderers[i].material.color = m_PlayerColor;
        }

    }

    // Used during the phases of the game where the player shouldn't be able to control their player.
    public void DisableControl() {

        m_Movement.StopPlayer();
        m_Movement.enabled = false;
        m_CanvasGameObject.SetActive(false);
        

    }


    // Used during the phases of the game where the player should be able to control their player.
    public void EnableControl() {
        m_Movement.enabled = true;
        m_CanvasGameObject.SetActive(true);
    }


    // Used at the start of each round to put the player into it's default state.
    public void Reset() {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

}
