using UnityEngine;

namespace KJS._06.SO
{
    [CreateAssetMenu(menuName = "SO/Ailen" , fileName =  "AilenSO" , order = 0)]
    public class AilenSO : ScriptableObject
    {
        public string ailenName;
        public int age;
        public string race;
        public string address;
        public Sprite icon;
        public bool isReal;
        
        [Header("Script")]
        public string startScript;
        public string[] scripts;
    }
}
