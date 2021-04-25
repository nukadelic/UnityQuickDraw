using UnityEngine;

namespace QuickDraw
{
    [ExecuteInEditMode]
    public class MonoDraw : MonoBehaviour
    {
        static MonoDraw global_instance;

        void OnEnable()
        {
            if(global_instance == null) global_instance = this;

            Camera.onPreCull -= DrawWithCamera;
            Camera.onPreCull += DrawWithCamera;
        }
        void OnDisable()
        {
            if(global_instance == this) global_instance = null;

            Camera.onPreCull -= DrawWithCamera;
        }

        void DrawWithCamera( Camera camera )
        {
            Common.camera = camera;

            OnDraw();

            Common.camera = null;
        }

        public virtual void OnDraw()
        {

        }

        #if UNITY_EDITOR
        
        protected static bool AutoRepaintSceneView;
        
        private void OnDrawGizmos()
        {
            // limit the RepaintAll calls to once per frame 
            if( global_instance == this && AutoRepaintSceneView ) 
                UnityEditor.SceneView.RepaintAll();
        }

        #endif
    }
}