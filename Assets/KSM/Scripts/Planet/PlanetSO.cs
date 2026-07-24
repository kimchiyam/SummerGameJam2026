using UnityEngine;

namespace KSM.Scripts.Planet
{
    [CreateAssetMenu(fileName = "PlanetSO", menuName = "PlanetSO", order = 0)]
    public class PlanetSO : ScriptableObject
    {
        public int sceneNumber;
        public string planetName;
        public string planetDescription;
        public Sprite planetIcon;
        public int planetIndex;
    }
}