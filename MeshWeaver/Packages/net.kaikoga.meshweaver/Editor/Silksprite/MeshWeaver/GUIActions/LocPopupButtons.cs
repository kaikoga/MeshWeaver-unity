using Silksprite.MeshWeaver.Utils;
using UnityEditor;

namespace Silksprite.MeshWeaver.GUIActions
{
    public class LocPopupButtons : GUIAction
    {
        readonly LocalizedContent _label;
        readonly LocalizedContent _text;
        readonly LocMenuItem[] _menuItems;
        readonly string[] _menuOptions;
        string _lang;

        public LocPopupButtons(LocalizedContent label, LocalizedContent text, LocMenuItem[] menuItems)
        {
            _label = label;
            _text = text;
            _menuItems = menuItems;
            _menuOptions = new string[menuItems.Length + 2];
        }

        public override void OnGUI()
        {
            if (_lang != Localization.Lang)
            {
                _lang = Localization.Lang;
                _menuOptions[0] = _text.Tr;
                for (var i = 0; i < _menuItems.Length; i++)
                {
                    _menuOptions[i + 2] = _menuItems[i].Label.Tr;
                }
            }
                
            var index = EditorGUILayout.Popup(_label.Tr, 0, _menuOptions);
            if (index <= 0) return;

            _menuItems[index - 2].OnSelect?.Invoke();
        }
    }
}