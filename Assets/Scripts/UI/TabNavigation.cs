using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabNavigation : MonoBehaviour
{
    // Array to store all the input fields in the order you want to navigate
    public TMP_InputField[] inputFields;

    void Update()
    {
        // Check for Shift+Tab key press
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKeyDown(KeyCode.Tab))
        {
            // Find the current focused input field
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                inputFields[inputFields.Length - 1].Select();
            }
            else
            {
                TMP_InputField currentField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();

                // Find the index of the current field in the array
                int currentIndex = System.Array.IndexOf(inputFields, currentField);
                // If the current field is found and is not the last one, select the next input field
                if (currentIndex != -1 && currentIndex > 0)
                {
                    inputFields[currentIndex - 1].Select();
                }

            }
        }
        // Check for Tab key press
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Find the current focused input field
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                inputFields[0].Select();
            }
            else
            {
                TMP_InputField currentField = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();

                // Find the index of the current field in the array
                int currentIndex = System.Array.IndexOf(inputFields, currentField);
                // If the current field is found and is not the last one, select the next input field
                if (currentIndex != -1 && currentIndex < inputFields.Length - 1)
                {
                    inputFields[currentIndex + 1].Select();
                }

            }

        }

    }
}