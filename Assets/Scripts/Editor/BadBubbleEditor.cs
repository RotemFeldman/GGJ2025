using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DefaultNamespace.Editor
{
    [CustomEditor(typeof(BadBubble))]
    public class BadBubbleEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            BadBubble bubble = (BadBubble)target;
            
            if (bubble == null || bubble.patrolPoints == null )
                return;

            if (!bubble.isPatrolling)
                return;
            
            for (int i = 0; i < bubble.patrolPoints.Count; i++)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Handles.PositionHandle(bubble.patrolPoints[i], Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(bubble, "Move Patrol Point");
                    bubble.patrolPoints[i] = newPosition; // Update the world-space position
                    EditorUtility.SetDirty(bubble);
                }

                Handles.Label(bubble.patrolPoints[i], $"Point {i + 1}", new GUIStyle
                {
                    fontStyle = FontStyle.Bold,
                    normal = new GUIStyleState { textColor = Color.white }
                });
            }

            Handles.color = Color.cyan;
            for (int i = 0; i < bubble.patrolPoints.Count - 1; i++)
            {
                Handles.DrawLine(bubble.patrolPoints[i], bubble.patrolPoints[i + 1]);
            }
            Handles.DrawLine(bubble.patrolPoints[bubble.patrolPoints.Count - 1], bubble.patrolPoints[0]);
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BadBubble bubble = (BadBubble)target;
            
            if (bubble == null || bubble.patrolPoints == null)
                return;

            if (!bubble.isPatrolling)
                return;

            if (GUILayout.Button("Add Patrol Point"))
            {
                Undo.RecordObject(bubble, "Add Patrol Point");
                bubble.patrolPoints.Add(bubble.transform.position);
                EditorUtility.SetDirty(bubble);
            }

            if (GUILayout.Button("Clear Patrol Points"))
            {
                Undo.RecordObject(bubble, "Clear Patrol Points");
                bubble.patrolPoints.Clear();
                EditorUtility.SetDirty(bubble);
            }
        }
    }
}