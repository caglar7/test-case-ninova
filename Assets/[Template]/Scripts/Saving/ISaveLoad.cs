using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public interface ISaveLoad
    {
        public void Reset(SaveData saveData);
        public void Load(SaveData saveData);
        public void Save(SaveData saveData);
    }
}