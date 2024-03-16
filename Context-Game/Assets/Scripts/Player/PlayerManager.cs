using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    [HideInInspector] public static PlayerManager instance;

    void Awake() 
    {
        if(instance != null) 
            Destroy(this);
        
        else
            instance = this;

        DontDestroyOnLoad(instance);    
    }
    #endregion

    // public GameObject player; 

    public enum PlayerType 
    {
        ARTIST,
        DESIGNER,
        DEVELOPER
    }

    public PlayerType playerType;
}