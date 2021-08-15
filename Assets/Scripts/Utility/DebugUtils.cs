using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugUtils
{
    /// <summary>
    /// Displays an error in the console and halts the game if in editor. If not in editor, does nothing.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public static void CrashAndBurn(object message)
    {
#if UNITY_EDITOR
        Debug.LogError(message);
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
