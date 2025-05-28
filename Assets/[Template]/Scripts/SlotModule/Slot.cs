using UnityEngine;
using UnityEngine.Serialization;

namespace Template
{
    public class Slot : BaseMono
    {
        public Transform objectHolder;
        public BaseMono currentObject;

        public virtual void FillSlot(BaseMono obj)
        {
            obj.Transform.SetParent(objectHolder);
            currentObject = obj;
        }

        public virtual void ClearSlot()
        {
            currentObject = null;
        }

        public virtual bool IsFilled()
        {
            return currentObject != null;
        }

        public virtual bool IsEmpty()
        {
            return currentObject == null;
        }
    }
}