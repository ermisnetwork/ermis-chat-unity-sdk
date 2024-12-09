using System.Collections.Generic;
using UnityEditor;

namespace ErmisChat.EditorTools.DefineSymbols
{
    public interface IUnityDefineSymbols
    {
        string[] GetScriptingDefineSymbols(BuildTarget buildTarget);

        void SetScriptingDefineSymbols(BuildTarget buildTarget, ICollection<string> defines);
    }
}