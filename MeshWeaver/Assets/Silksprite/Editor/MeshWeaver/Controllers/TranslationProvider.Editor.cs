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
            var transform = provider.transform;
            var worldRotation = transform.rotation;

            var eventType = Event.current.type;
            switch (eventType)
            {
                case EventType.Repaint:
                case EventType.Layout:
                    var globalPosition = transform.position;
                    var handleSize = HandleUtility.GetHandleSize(globalPosition) * 0.125f;
                    Handles.color = Handles.xAxisColor;
                    var globalOneX = transform.TransformPoint(provider.oneX);
                    Handles.CubeHandleCap(_controlIdX, globalOneX, worldRotation, handleSize, eventType);
                    Handles.DrawLine(globalPosition, globalOneX);
                    Handles.color = Handles.yAxisColor;
                    var globalOneY = transform.TransformPoint(provider.oneY);
                    Handles.CubeHandleCap(_controlIdY, globalOneY, worldRotation, handleSize, eventType);
                    Handles.DrawLine(globalPosition, globalOneY);
                    Handles.color = Handles.zAxisColor;
                    var globalOneZ = transform.TransformPoint(provider.oneZ);
                    Handles.CubeHandleCap(_controlIdZ, globalOneZ, worldRotation, handleSize, eventType);
                    Handles.DrawLine(globalPosition, globalOneZ);
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
                    var oneXGlobal = Handles.PositionHandle(transform.TransformPoint(provider.oneX), worldRotation);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(provider, "Change OneX TranslationProvider");
                        provider.oneX = transform.InverseTransformPoint(oneXGlobal);
                        EditorUtility.SetDirty(provider);
                    }
                    break;
                }
                case Axis.Y:
                    EditorGUI.BeginChangeCheck();
                    var oneYGlobal = Handles.PositionHandle(transform.TransformPoint(provider.oneY), worldRotation);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(provider, "Change OneY TranslationProvider");
                        provider.oneY = transform.InverseTransformPoint(oneYGlobal);
                        EditorUtility.SetDirty(provider);
                    }
                    break;
                case Axis.Z:
                    EditorGUI.BeginChangeCheck();
                    var oneZGlobal = Handles.PositionHandle(transform.TransformPoint(provider.oneZ), worldRotation);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(provider, "Change OneZ TranslationProvider");
                        provider.oneZ = transform.InverseTransformPoint(oneZGlobal);
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