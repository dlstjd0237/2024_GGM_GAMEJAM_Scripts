using BIS.Animators;
using UnityEngine;

namespace BIS.FSM
{
    [CreateAssetMenu(fileName = "StateSO", menuName = "SO/BIS/FSM/State")]
    public class StateSO : ScriptableObject
    {
        public string stateName;
        public string className;
        public AnimParamSO stateParam;
    }
}