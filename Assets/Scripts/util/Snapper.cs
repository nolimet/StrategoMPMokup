using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

namespace util
{
    public class Snapper : MonoBehaviour
    {
        public float xStep = 3f;
        public float yStep = 3f;
        public float zStep = 3f;


        void Start()
        {
            if (Application.isPlaying) Destroy(this);
        }

        public void _Update()
        {
            if (!gameObject.activeInHierarchy || Input.GetMouseButton(0)) return;

#if UNITY_EDITOR
            if ((Selection.activeTransform != null) && (Selection.activeTransform != transform) && (transform.IsChildOf(Selection.activeTransform)))
            {
                return;
            }

            Vector3 pos = transform.position;
            int gridSteps = Mathf.RoundToInt(pos.x / xStep);
            pos.x = ((float)gridSteps) * xStep;

            gridSteps = Mathf.RoundToInt(pos.y / yStep);
            pos.y = ((float)gridSteps) * yStep;

            gridSteps = Mathf.RoundToInt(pos.z / zStep);
            pos.z = ((float)gridSteps) * zStep;

            transform.position = pos;
#endif
        }
    }
}