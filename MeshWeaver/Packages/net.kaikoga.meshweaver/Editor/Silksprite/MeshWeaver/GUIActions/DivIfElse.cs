namespace Silksprite.MeshWeaver.GUIActions
{
    public class DivIfElse : GUIAction
    {
        readonly GUIAction _ifContent;
        readonly GUIAction _elseContent;
        public bool value;

        public DivIfElse(GUIAction ifContent) : this(ifContent, new GUIContainer()) { }

        public DivIfElse(GUIAction ifContent, GUIAction elseContent)
        {
            _ifContent = ifContent;
            _elseContent = elseContent;
        }

        public override void OnGUI()
        {
            if (value)
            {
                _ifContent.OnGUI();
            }
            else
            {
                _elseContent.OnGUI();
            }
        }
    }
}