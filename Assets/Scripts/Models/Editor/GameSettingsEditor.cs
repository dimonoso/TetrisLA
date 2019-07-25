using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tetris.Models.Editor
{
    [CustomEditor(typeof(GameSettingsScriptableObject))]
    public class GameSettingsEditor : UnityEditor.Editor
    {
        private GameSettingsScriptableObject _serializedObject;

        private static bool _showShapesEditor = true;

        private static int _selectedShapeIndex = -1;

        private Texture2D _shapeTexture2D;
        private GUIStyle _checkShapeStyle;
        private GUIStyle _uncheckShapeStyle;
        private GUIStyle _styleMarginLeft20;

        private void OnEnable()
        {
            _serializedObject = (GameSettingsScriptableObject)target;

            _shapeTexture2D = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Art/Block.psd", typeof(Texture2D));

            LoadStyles();
        }

        private void LoadStyles()
        {
            _styleMarginLeft20 = new GUIStyle();
            _styleMarginLeft20.margin.left = 20;

            _uncheckShapeStyle = new GUIStyle(EditorStyles.helpBox)
            {
                fixedHeight = 50,
                fixedWidth = 50
            };

            _checkShapeStyle = new GUIStyle(_uncheckShapeStyle);
            _checkShapeStyle.normal.background = _shapeTexture2D;
        }

        public override void OnInspectorGUI()
        {
            ShowTableSettings();
            ShowShapesSettings();

            EditorUtility.SetDirty(target);

            base.OnInspectorGUI();
        }

        private void ShowTableSettings()
        {
            HeaderLabel("Table Settings");

            _serializedObject.TableHeight = EditorGUILayout.IntField("Table Height", _serializedObject.TableHeight);
            _serializedObject.TableWidth = EditorGUILayout.IntField("Table Width", _serializedObject.TableWidth);

            Separator();
        }

        private void ShowShapesSettings()
        {
            HeaderLabel("Shapes Settings");

            _showShapesEditor = EditorGUILayout.Foldout(_showShapesEditor, "Shapes");

            if (_showShapesEditor)
            {
                EditorGUILayout.BeginVertical(_styleMarginLeft20);

                for (var i = 0; i < _serializedObject.Shapes.Count; i++)
                {
                    ShowShapeSettings(_serializedObject.Shapes[i], i);
                }

                AddShapeButton();
                EditorGUILayout.EndVertical();
            }

            Separator();
        }

        private void ShowShapeSettings(Shape shape, int index)
        {
            var isShow = EditorGUILayout.Foldout(index == _selectedShapeIndex, shape.ShapeName);
            if ((index == _selectedShapeIndex) != isShow)
            {
                _selectedShapeIndex = index == _selectedShapeIndex ? -1 : index;
            }

            if (!isShow)
            {
                return;
            }

            shape.ShapeName = EditorGUILayout.TextField("Shape Name", shape.ShapeName);
            for (var i = 0; i < shape.Height; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (var j = 0; j < shape.Width; j++)
                {
                    var style = _uncheckShapeStyle;
                    if (shape.At(i, j))
                    {
                        style = _checkShapeStyle;
                    }

                    if (GUILayout.Button("", style))
                    {
                        shape.SetAt(i, j, !shape.At(i, j));
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
            Separator();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Row"))
            {
                shape.AddRow();
            }
            if (GUILayout.Button("Add Colum"))
            {
                shape.AddColum();
            }
            EditorGUILayout.EndHorizontal();
            Separator();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Optimize Shape"))
            {
                shape.OptimizeShape();
            }
            if (GUILayout.Button("Delete Shape"))
            {
                _serializedObject.Shapes.RemoveAt(_selectedShapeIndex);
                _selectedShapeIndex = -1;
            }
            EditorGUILayout.EndHorizontal();

            Separator();
        }

        private void AddShapeButton()
        {
            if (GUILayout.Button("Add Shape"))
            {
                var shape = new Shape();
                _serializedObject.Shapes.Add(shape);
            }
        }

        private void HeaderLabel(string text)
        {
            EditorGUILayout.LabelField(text, EditorStyles.boldLabel);
        }

        private void Separator()
        {
            EditorGUILayout.Separator();
        }
    }
}