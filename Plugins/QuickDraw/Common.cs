
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace QuickDraw
{
    public static class Common
    {
        public static Vector3 normal => - camera.transform.forward;

        public static float AASmoothing = 1.5f;

        public static readonly Color NoColor = new Color( 0,0,0,0 );

        static bool _cameraOverride = false;
        static Camera _cameraCustom;
        static Camera _cameraRuntime;
        #if UNITY_EDITOR
        static Camera _cameraEditor;
        #endif
        public static Camera camera
        {
            get
            {
                if( _cameraOverride ) return _cameraCustom;

#if UNITY_EDITOR
                if( ! Application.isPlaying )
                {
                    if (_cameraEditor == null )
                        _cameraEditor = SceneView.lastActiveSceneView.camera;
                    return _cameraEditor;
                }
#endif
                if( _cameraRuntime == null ) _cameraRuntime = Camera.main ?? Camera.current;
                return _cameraRuntime;
            }

            set
            {
                if ( _cameraOverride = value != null ) _cameraCustom = value;
            }
        }


    }
}