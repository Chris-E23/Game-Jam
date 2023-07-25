using UnityEngine;

namespace CorruptElementary
{
    public static class ExtensionMethods
    {
        public static void DoAfter(this MonoBehaviour monoBehaviour, float seconds, System.Action action)
        {
            monoBehaviour.StartCoroutine(DoAfterCoroutine(seconds, action));
        }
        
        private static System.Collections.IEnumerator DoAfterCoroutine(float seconds, System.Action action)
        {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }
    }
}