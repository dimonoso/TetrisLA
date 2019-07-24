using UnityEngine;

namespace Tetris.Models
{
    [CreateAssetMenu(menuName = "Game/SettingsData")]
    public class GameSettingsScriptableObject : ScriptableObject
    {
        [SerializeField]
        private int _tableWidth;

        [SerializeField]
        private int _tableHeight;

        public int TableWidth
        {
            get { return _tableWidth; }
        }

        public int TableHeight
        {
            get { return _tableHeight; }
        }
    }
}