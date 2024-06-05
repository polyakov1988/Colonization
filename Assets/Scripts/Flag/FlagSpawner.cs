using UnityEngine;

namespace Flag
{
    public class FlagSpawner : MonoBehaviour
    {
        [SerializeField] private Flag _flagPrefab;
        
        public Flag Get()
        {
            return Instantiate(_flagPrefab);
        }
    }
}