using UnityEngine;
using System.Collections;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
using XInputDotNetPure;
#endif

public class CommonInputManager : MonoBehaviour
{
    // X360 controller index used by XInput
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
    private PlayerIndex playerIndex;
    private bool playerIndexSet;
    private GamePadState state;
    private GamePadState previousState;
#endif

    void Awake()
    {
        // Finds the first connected xbox 360 controller
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        playerIndexSet = false;
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                if (!playerIndexSet)
                {
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }
#endif
    }

    // Input properties

    /// <summary>
    /// Returns horizontal input from any input defined input source in range [-1f, 1f].
    /// </summary>
    public float HorizontalInput
    {
        get
        {
            var inputValue = 0f;

            // Keyboard input
            inputValue += Input.GetAxis("Horizontal_kb");

            // Gamepad input
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            inputValue += state.ThumbSticks.Left.X;
#endif
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
            inputValue += Input.GetAxis("Horizontal_joystick_osx");
#endif

            return Mathf.Clamp(inputValue, -1f, 1f);
        }
    }

    /// <summary>
    /// Returns vertical input from any input defined input source in range [-1f, 1f].
    /// </summary>
    public float VerticalInput
    {
        get
        {
            var inputValue = 0f;

            // Keyboard input
            inputValue += Input.GetAxis("Vertical_kb");

            // Gamepad input
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            inputValue += state.ThumbSticks.Left.Y;
#endif
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
            inputValue += Input.GetAxis("Vertical_joystick_osx");
#endif

            return Mathf.Clamp(inputValue, -1f, 1f);
        }
    }

    public float SwapInput
    {
        get
        {
            var inputValue = 0f;

            // Keyboard input
            inputValue += Input.GetAxis("Spacebar");

            // Gamepad input
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            inputValue += state.Buttons.A == ButtonState.Pressed ? 1f : 0f;
#endif
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
            inputValue += Input.GetAxis("Swap_joystick_osx");
#endif

            return Mathf.Clamp(inputValue, 0f, 1f);
        }
    }


    void Update()
    {
        // Update the state for XInput
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
        previousState = state;
        state = GamePad.GetState(playerIndex);
#endif
    }

}
