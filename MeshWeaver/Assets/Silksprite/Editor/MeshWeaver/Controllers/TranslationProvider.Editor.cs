using UnityEditor;
using UnityEngine;

namespace Silksprite.MeshWeaver.Controllers
{
    [CustomEditor(typeof(TranslationProvider))]
    public class TranslationProviderEditor : Editor
    {
        Axis _focusAxis;
        int _controlIdX;
        int _controlIdY;
        int _controlIdZ;

        void OnEnable()
        {
            if (_controlIdX != default) return;
            _focusAxis = Axis.None;

            _controlIdX = GUIUtility.GetControlID(FocusType.Passive);
            _controlIdY = GUIUtility.GetControlID(FocusType.Passive);
            _controlIdZ = GUIUtility.GetControlID(FocusType.Passive);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Reset"))
            {
                var provider = (TranslationProvider)target;
                Undo.RecordObject(provider, "Reset TranslationProvider");
                provider.oneX = Vector3.right;
                provider.oneY = Vector3.up;
                provider.oneZ = Vector3.forward;
                EditorUtility.SetDirty(provider);
            }
        }

        void OnSceneGUI()
        {
            var provider = (TranslationProvider)target;
            var globalPosition = provider.transform.position;

            var eventType = Event.current.type;
            switch (eventType)
            {
                case EventType.Repaint:
                case EventType.Layout:
                    var handleSize = HandleUtility.GetHandleSize(globalPosition) * 0.125f;
                    Handles.color = Handles.xAxisColor;
                    Handles.CubeHandleCap(_controlIdX, globalPosition + provider.oneX, Quaternion.identity, handleSize, eventType);
                    Handles.DrawLine(globalPosition, globalPosition + provider.oneX);
                    Handles.color = Handles.yAxisColor;
                    Handles.CubeHandleCap(_controlIdY, globalPosition + provider.oneY, Quaternion.identity, handleSize, eventType);
                    Handles.DrawLine(globalPosition, globalPosition + provider.oneY);
                    Handles.color = Handles.zAxisColor;
                    Handles.CubeHandleCap(_controlIdZ, globalPosition + provider.oneZ, Quaternion.identity, handleSize, eventType);
                    Handles.DrawLine(globalPosition, globalPosition + provider.oneZ);
                    break;
                case EventType.MouseDown:
                    var nearestControl = HandleUtility.nearestControl;
                    if (nearestControl == _controlIdX)
                    {
                        _focusAxis = Axis.X;
                    }
                    else if (nearestControl == _controlIdY)
                    {
                        _focusAxis = Axis.Y;
                    }
                    else if (nearestControl == _controlIdZ)
                    {
                        _focusAxis = Axis.Z;
                    }
                    break;
            }

            switch (_focusAxis)
            {
                case Axis.X:
                {
                    EditorGUI.BeginChangeCheck();
                    var oneXGlobal = Handles.PositionHandle(provider.oneX + globalPosition, Quaternion.identity);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(provider, "Change OneX TranslationProvider");
                        provider.oneX = oneXGlobal - globalPosition;
                        EditorUtility.SetDirty(provider);
                    }
                    break;
                }
                case Axis.Y:
                    EditorGUI.BeginChangeCheck();
                    var oneYGlobal = Handles.PositionHandle(provider.oneY + globalPosition, Quaternion.identity);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(provider, "Change OneY TranslationProvider");
                        provider.oneY = oneYGlobal - globalPosition;
                        EditorUtility.SetDirty(provider);
                    }
                    break;
                case Axis.Z:
                    EditorGUI.BeginChangeCheck();
                    var oneZGlobal = Handles.PositionHandle(provider.oneZ + globalPosition, Quaternion.identity);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(provider, "Change OneZ TranslationProvider");
                        provider.oneZ = oneZGlobal - globalPosition;
                        EditorUtility.SetDirty(provider);
                    }
                    break;
            }
        }
        
        enum Axis
        {
            None,
            X,
            Y,
            Z
        }
    }
}