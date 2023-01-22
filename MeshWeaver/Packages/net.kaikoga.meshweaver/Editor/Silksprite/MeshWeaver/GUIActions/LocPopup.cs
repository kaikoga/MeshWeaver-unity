using Silksprite.MeshWeaver.GUIActions.Events;
using Silksprite.MeshWeaver.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class LocPopup : GUIAction
    {
        public int value;

        readonly LocalizedContent _label;
        readonly LocalizedContent[] _options;
        readonly string[] _menuOptions;
        string _lang;

        public readonly Dispatcher<ChangeEvent<int>, int> onChanged = new Dispatcher<ChangeEvent<int>, int>();

        public LocPopup(LocalizedContent label, int selectedIndex, LocalizedContent[] options)
        {
            _label = label;
            value = selectedIndex;
            _options = options;
            _menuOptions = new string[options.Length];
        }

        public override void OnGUI()
        {
            if (_lang != Localization.Lang)
            {
                _lang = Localization.Lang;
                for (var i = 0; i < _options.Length; i++)
                {
                    _menuOptions[i] = _options[i].Tr;
                }
            }
                
            var index = EditorGUILayout.Popup(_label.Tr, value, _menuOptions);
            if (index < 0) return;
            value = index;
            onChanged.Invoke(value);
        }
    }
}