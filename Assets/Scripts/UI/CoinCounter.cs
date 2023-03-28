using NTC.Global.Cache;
using TowerDefense.Entities;
using UnityEngine;
using TMPro;

namespace TowerDefense.UI
{
    public class CoinCounter : MonoCache
    {
        [SerializeField] private TMP_Text text;
        private PlayerData playerData;

        public void Construct(PlayerData playerData)
        {
            this.playerData = playerData;
            playerData.CoinsChanged += SetCount;
            SetCount(playerData.Coins);
        }

        protected override void OnDisabled()
        {
            if (playerData != null)
                playerData.CoinsChanged -= SetCount;
        }

        public void SetCount(int count)
            => text.text = $"{count}$";
    }
}