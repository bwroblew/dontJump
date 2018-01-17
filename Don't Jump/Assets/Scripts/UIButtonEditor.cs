#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(JumpButton))]
public class UIButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //UIButton t = (UIButton)target;
    }
}
#endif