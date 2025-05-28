using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

namespace Template
{
    public class InputComponentManager : BaseMono
    {
        public List<BaseInputComponent> inputComponents = new List<BaseInputComponent>();

        [Button]
        public void ActivateComponentInputs()
        {
            foreach (BaseInputComponent inputComponent in inputComponents)
            {
                inputComponent.isInputActive = true;
            }
        }

        [Button]
        public void DeActivateComponentInputs()
        {
            foreach (BaseInputComponent inputComponent in inputComponents)
            {
                inputComponent.isInputActive = false;
            }
        }
    }
}