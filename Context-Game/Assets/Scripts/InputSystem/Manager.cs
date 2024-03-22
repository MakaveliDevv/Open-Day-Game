using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    public GameObject character3;
    public InputSystemPlayerController playerController;

    void Start()
    {
        // Assign each character to a specific keyboard
        AssignCharactersToKeyboards();
    }

    void AssignCharactersToKeyboards()
    {
        // Get all connected keyboards
        var keyboards = GetConnectedKeyboards();

        // Assign characters to keyboards
        if (keyboards.Count >= 1)
        {
            playerController.AddKeyboardCharacterPair(keyboards[0], character1);
            // playerController.AddKeyboardCharacterPair(keyboards[1], character2);
            // playerController.AddKeyboardCharacterPair(keyboards[2], character3);
        }
        else
        {
            Debug.LogError("Not enough keyboards connected!");
        }
    }

    List<Keyboard> GetConnectedKeyboards()
    {
        List<Keyboard> keyboards = new List<Keyboard>();

        // Iterate through all input devices
        foreach (var device in InputSystem.devices)
        {
            if (device is Keyboard)
            {
                keyboards.Add((Keyboard)device);
            }
        }

        return keyboards;
    }
}
