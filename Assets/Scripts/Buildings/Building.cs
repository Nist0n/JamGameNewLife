using System.Collections;
using TMPro;
using UnityEngine;

namespace Buildings
{
    public abstract class Building : MonoBehaviour
    {
        public abstract void ToggleControls();
        public abstract void Upgrade();
        protected abstract void UpdateValues();
    }
}