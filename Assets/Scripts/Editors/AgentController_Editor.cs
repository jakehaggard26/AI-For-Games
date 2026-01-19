using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AgentController))]
public class AgentController_Editor : Editor
{
    #region Variables
    private AgentController agent;
    #endregion

    void OnEnable()
    {
        // target = Object being inspected
        agent = (AgentController)base.target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Custom editor code

        GUILayout.Space(20);

        // Draw a new text box
        string text = "";
        text += "Current Linear Velocity: " + agent.rb.linearVelocity + "\n";
        text += "Current Angular Velocity: " + agent.rb.angularVelocity + "\n";

        EditorGUILayout.TextArea(text);

        
    }
}
