using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InputSystemPlayerController : MonoBehaviour
{
    // Dictionary<Keyboard, GameObject> keyboardCharacterMap = new Dictionary<Keyboard, GameObject>();

    // public void AddKeyboardCharacterPair(Keyboard keyboard, GameObject character)
    // {
    //     keyboardCharacterMap[keyboard] = character;
    // }

    // public void RemoveKeyboardCharacterPair(Keyboard keyboard)
    // {
    //     keyboardCharacterMap.Remove(keyboard);
    // }

    // void Update()
    // {
    //     foreach (var keyboard in keyboardCharacterMap.Keys)
    //     {
    //         if (keyboard.wasUpdatedThisFrame)
    //         {
    //             GameObject character = keyboardCharacterMap[keyboard];
    //             HandleCharacterInput(character);
    //         }
    //     }
    // }

    // void HandleCharacterInput(GameObject character)
    // {
    //     Vector2 moveInput = character.GetComponent<PlayerInput>().actions["Movement"].ReadValue<Vector2>();
    //     character.GetComponent<CharacterMovement>().Move(moveInput);
    // }


      // Define a mapping between keyboards and characters
    Dictionary<Keyboard, GameObject> keyboardCharacterMap = new();

    // Method to add a keyboard-character pair to the mapping
    public void AddKeyboardCharacterPair(Keyboard keyboard, GameObject character)
    {
        keyboardCharacterMap[keyboard] = character;
    }

    // Method to remove a keyboard-character pair from the mapping
    public void RemoveKeyboardCharacterPair(Keyboard keyboard)
    {
        keyboardCharacterMap.Remove(keyboard);
    }

    void Update()
    {
        // Iterate through all input events
        foreach (var device in InputSystem.devices)
        {
            // Check if the device is a keyboard
            if (device is Keyboard)
            {
                // Check if the keyboard is mapped to a character
                if (keyboardCharacterMap.ContainsKey((Keyboard)device))
                {
                    // Get the character associated with this keyboard
                    GameObject character = keyboardCharacterMap[(Keyboard)device];

                    // Handle input for this character
                    HandleCharacterInput(character);
                }
            }
        }
    }

    void HandleCharacterInput(GameObject character)
    {
        // Example: Read movement input for the character and apply it
        Vector2 moveInput = character.GetComponent<PlayerInput>().actions["Movement"].ReadValue<Vector2>();
        character.GetComponent<CharacterMovement>().Move(moveInput);
    }
}
