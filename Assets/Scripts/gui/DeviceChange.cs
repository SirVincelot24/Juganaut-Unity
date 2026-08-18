using System;
using System.Collections;
using UnityEngine;

namespace gui
{
    public class DeviceChange : MonoBehaviour 
    {
        public static event Action<Vector2> OnResolutionChange;
        public static event Action<DeviceOrientation> OnOrientationChange;
        private const float CheckDelay = 0.5f; // How long to wait until we check again.

        private static Vector2 _resolution;                    // Current Resolution
        private static DeviceOrientation _orientation;        // Current Device Orientation
        private static bool _isAlive = true;                    // Keep this script running?

        private void Start() {
            StartCoroutine(CheckForChange());
        }

        private static IEnumerator CheckForChange(){
            _resolution = new Vector2(Screen.width, Screen.height);
            _orientation = Input.deviceOrientation;
            
            OnResolutionChange?.Invoke(_resolution);

            while (_isAlive) {
 
                // Check for a Resolution Change
                if (!Mathf.Approximately(_resolution.x, Screen.width) || !Mathf.Approximately(_resolution.y, Screen.height) )
                {
                    _resolution = new Vector2(Screen.width, Screen.height);
                    OnResolutionChange?.Invoke(_resolution);
                }
 
                // Check for an Orientation Change
                switch (Input.deviceOrientation) {
                    case DeviceOrientation.Unknown:            // Ignore
                    case DeviceOrientation.FaceUp:            // Ignore
                    case DeviceOrientation.FaceDown:        // Ignore
                        break;
                    default:
                        if (_orientation != Input.deviceOrientation) {
                            _orientation = Input.deviceOrientation;
                            if (OnOrientationChange != null) OnOrientationChange(_orientation);
                        }
                        break;
                }
 
                yield return new WaitForSeconds(CheckDelay);
            }
        }

        private void OnDestroy(){
            _isAlive = false;
        }
    }
}