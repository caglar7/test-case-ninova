using UnityEngine;

namespace Template.Scripts.DeveloperHelper
{
    public class ConvertMaterialShadersToBuiltIn : EditorApplyReset
    {
        protected override void _Apply()
        {
            foreach (var material in Resources.FindObjectsOfTypeAll<Material>())
            {
                if (material.shader.name.StartsWith("Universal Render Pipeline"))
                {
                    material.shader = Shader.Find("Standard");
                }
            }
        }

        protected override void _Reset()
        {
        }
    }
}