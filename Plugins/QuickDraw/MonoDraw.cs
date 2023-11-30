using UnityEngine;

namespace QuickDraw
{
    [ExecuteInEditMode]
    [DefaultExecutionOrder(1000)]
    public class MonoDraw : MonoBehaviour
    {
        public Camera filterCamera;

        public virtual void OnDraw()
        {
            // override 
        }

        static MonoDraw global_instance;
        public enum DrawAt { Update, StaticPreCull, StaticPostRender, PreCull, PreRender, PostRender, RenderObject }

        public DrawAt drawAt = DrawAt.Update;

        void OnEnable()
        {
            if(global_instance == null) global_instance = this;

            Camera.onPreCull -= StaticPreCull;
            Camera.onPreCull += StaticPreCull;

            Camera.onPostRender -= StaticPostRender;
            Camera.onPostRender += StaticPostRender;
        }
        void OnDisable()
        {
            if(global_instance == this) global_instance = null;

            Camera.onPostRender -= StaticPostRender;
            Camera.onPreCull -= StaticPreCull;
        }

        void StaticDrawWithCamera( Camera camera = null )
        {
            var _camera = Common.camera;

            Common.camera = camera;

            OnDraw();

            Common.camera = _camera;
        }


        void StaticPostRender( Camera camera )
        {
            if( drawAt != DrawAt.StaticPostRender ) return;

            StaticDrawWithCamera( camera );
        }

        void StaticPreCull( Camera camera )
        {
            if( drawAt != DrawAt.StaticPreCull ) return;
            
            StaticDrawWithCamera( camera );
        }

        void CatchCameraAndDraw()
        {
            Common.camera = filterCamera;

            // Common.camera = Common.camera == null ? ( Camera.main ?? Camera.current ) : Common.camera;

            OnDraw();
        }

        private void Update()
        {
            if( drawAt != DrawAt.Update ) return;

            CatchCameraAndDraw();
        }

        private void OnPreRender()
        {
            if( drawAt != DrawAt.PreRender ) return;

            CatchCameraAndDraw();
        }

        private void OnPostRender()
        {
            if( drawAt != DrawAt.PostRender ) return;
            
            CatchCameraAndDraw();
        }

        private void OnPreCull()
        {
            if( drawAt != DrawAt.PreCull ) return;

            CatchCameraAndDraw();
        }

        private void OnRenderObject()
        {
            if( drawAt != DrawAt.RenderObject ) return;

            CatchCameraAndDraw();
        }

        #if UNITY_EDITOR
        
        // protected static bool AutoRepaintSceneView;
        
        // private void OnDrawGizmos()
        // {
        //     CatchCameraAndDraw();
        // 
        //     // limit the RepaintAll calls to once per frame 
        //     if ( global_instance == this && ( AutoRepaintSceneView || ! Application.isPlaying ) ) 
        // 
        //         UnityEditor.SceneView.RepaintAll();
        // }

        // void OnSceneGUI( Camera scene_camera ) => StaticDrawWithCamera( scene_camera );

        // [UnityEditor.CustomEditor(typeof(MonoDraw))]
        // class Inspector : UnityEditor.Editor
        // {
        //     private void OnSceneGUI()
        //     {
        //         var scene_camera = UnityEditor.SceneView.currentDrawingSceneView.camera;
        // 
        //         foreach ( var t in targets )
        //         {
        //             if( t == null || ! ( t is MonoDraw ) ) continue;
        // 
        //             var script = ( MonoDraw ) t;
        // 
        //             script.OnSceneGUI( scene_camera );
        //         }
        //     }
        // }

        #endif
    }
}