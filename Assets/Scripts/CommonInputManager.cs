using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
using XInputDotNetPure;
#endif

public class CommonInputManager : MonoBehaviour
{
	public static CommonInputManager instance;

	// X360 controller index used by XInput
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
	private PlayerIndex playerIndex;
	private bool playerIndexSet;
	private GamePadState state;
	private GamePadState previousState;
#endif

    // Controller state for OS X gamepads
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
    private Dictionary<string, bool> OSXJoystickPreviousState;
    private Dictionary<string, bool> OSXJoystickState;
#endif

    void Awake()
	{
		// Singleton
		if (instance == null) instance = this;
		else if (!instance.Equals(this)) Destroy(gameObject);
		DontDestroyOnLoad(gameObject);

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

        // Initialize OS X controller states
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
        OSXJoystickPreviousState = new Dictionary<string, bool>();
        OSXJoystickState = new Dictionary<string, bool>();
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

    public float HorizontalCameraInput
    {
        get
        {
            var inputValue = 0f;

            // Keyboard input
            inputValue += Input.GetAxis("Mouse X");

            // Gamepad input
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            inputValue += state.ThumbSticks.Right.X;
#endif
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
            inputValue += Input.GetAxis("Horizontal_joystick_right_osx");
#endif

            return inputValue;
        }
    }

    public float VerticalCameraInput
    {
        get
        {
            var inputValue = 0f;

            // Keyboard input
            inputValue += Input.GetAxis("Mouse Y");

            // Gamepad input
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
            inputValue += state.ThumbSticks.Right.Y;
#endif
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
            inputValue += Input.GetAxis("Vertical_joystick_right_osx");
#endif

            return inputValue;
        }
    }

    public bool SwapInput
	{
		get
		{
            var inputValue = false;

			// Keyboard input
			inputValue |= Input.GetKeyDown("space");

			// Gamepad input
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
			inputValue |= (previousState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed);
#endif
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
            inputValue |= (!OSXJoystickPreviousState["Swap_joystick_osx"] && OSXJoystickState["Swap_joystick_osx"]);
#endif

            return inputValue;
		}
	}


	void Update()
	{
		// Update the state for XInput
#if (UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN)
		previousState = state;
		state = GamePad.GetState(playerIndex);
#endif

        // Update the state for OS X controllers
#if (UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX)
        foreach(string key in OSXJoystickState.Keys)
        {
            OSXJoystickPreviousState[key] = OSXJoystickState[key];
        }

        OSXJoystickState["Swap_joystick_osx"] = Input.GetAxis("Swap_joystick_osx") > 0.5f;
#endif
    }

}
