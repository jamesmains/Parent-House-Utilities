using System.Collections;
using UnityEngine;

namespace ParentHouse.Utilities {
    public class SelfDestruct : MonoBehaviour {
        private float SelfDestructTime = 20f;
        private bool DestroyOnDestruct;

        private void OnEnable() {
            StopAllCoroutines();
            StartCoroutine(DestructSelf());
        }

        private IEnumerator DestructSelf() {
            yield return new WaitForSeconds(SelfDestructTime);
            if (DestroyOnDestruct)
                Destroy(gameObject);
            else gameObject.SetActive(false);
        }
    }
}