using NTC.Global.Cache;
using System;
using TowerDefense.Entities;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense.UI
{
    public class UpgradeButton : MonoCache
    {
        [field: SerializeField] public UpgradeType Type { get; private set; }
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private TMP_Text upgradeText;
        [SerializeField] private Button button;
        public bool HasNextUpgrade { get; private set; } = true;
        public event Action<UpgradeButton> OnClick;

        protected override void OnEnabled()
        {
            button.onClick.AddListener(() => Raise());
        }

        protected override void OnDisabled()
        {
            button.onClick.RemoveAllListeners();
        }

        public void SetInteractable(bool state, bool hasNextUpgrade)
        {
            button.interactable = state;
            if (!hasNextUpgrade)
            {
                SetDescription("", "");
                HasNextUpgrade = hasNextUpgrade;
            }
        }

        public void SetDescription(string price, string upgradeValue)
        {
            priceText.text = price;
            upgradeText.text = upgradeValue;
        }

        private void Raise()
        {
            OnClick.Invoke(this);
        }
    }
}