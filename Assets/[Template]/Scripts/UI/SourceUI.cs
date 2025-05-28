using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// listens for source events
/// </summary>

namespace Template
{
    public class SourceUI : BaseUI
    {
        [Header("References")]
        public Image icon;
        
        [Header("Fields")]
        [SerializeField] private Source _source;
        [SerializeField] private TextMeshProUGUI _txt;

        [Space]
        [Header("Edit Mode")]
        [SerializeField] private bool _applyEdit;

        private void OnEnable()
        {
            SetIcon();
            SetText();

            SourceEvents.OnUpdatedSource += CheckSourceState;
        }

        private void OnDisable()
        {
            SourceEvents.OnUpdatedSource -= CheckSourceState;
        }

        private void CheckSourceState(Source checkSource)
        {
            if (checkSource.name != _source.name) return;

            SetText();
        }

        private void SetIcon()
        {
            icon.sprite = _source.icon;
        }

        private void SetText()
        {
            _txt.text = _source.currentValue.ToString();
        }

        #region Editor Codes
#if UNITY_EDITOR

        private void OnValidate()
        {
            UnityEditor.EditorApplication.delayCall += _OnValidate;
        }
        private void _OnValidate()
        {
            if (_applyEdit == false) return;

            if (_source == null || icon == null || _txt == null) return;

            _applyEdit = false;
            
            SetIcon();
            SetText();
        }
#endif
        #endregion
    }
}