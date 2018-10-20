/*
    UModules::CurveAsset
    UModules::CurveAssetExtensions

    by: Rajin Shankar
    part of: UM_Basic

    available to use according to UM_Basic/LICENSE
 */

using System;
using System.Collections;
using UnityEngine;

namespace UModules
{
    /// <summary>
    /// Reusable object to hold spline curve data using a Unity AnimationCurve
    /// </summary>
    [CreateAssetMenu(fileName = "Curve_", menuName = "UModule/Data/CurveAsset", order = 0)]
    public class CurveAsset : ScriptableObject
    {
        /// <summary>
        /// Unity AnimationCurve data
        /// </summary>
        [SerializeField] private AnimationCurve curve = new AnimationCurve();
        /// <summary>
        /// Default scale value used when evaluating
        /// </summary>
        [SerializeField] private float defaultScale = 1;
        /// <summary>
        /// Default duration value used when evaluating
        /// </summary>
        [SerializeField] private float defaultDuration = 1;
        /// <summary>
        /// Public readonly property for default scale
        /// </summary>
        public float Scale { get { return defaultScale; } }
        /// <summary>
        /// Public readonly property for default duration
        /// </summary>
        public float Duration { get { return defaultDuration; } }

        /// <summary>
        /// Evaluate the curve at the given time with respect to the given scale and duration
        /// Time is clamped between 0 and duration
        /// </summary>
        /// <param name="t">Input time</param>
        /// <param name="duration">Inverse scale factor for input time</param>
        /// <param name="scale">Scale factor for curve value</param>
        /// <returns>scale * curve(t / duration)</returns>
        public float Evaluate(float t, float duration, float scale)
        {
            // Clamp time value in the range [0, duration]
            if (t < 0) t = 0;
            else if (t > duration) t = duration;
            // Evaluate curve in range [0, 1] with respect to given duration
            return curve.Evaluate(t / duration) * scale;
        }
        /// <summary>
        /// Call Evaluate using the default scale and duration values
        /// </summary>
        /// <param name="t">Input time</param>
        /// <returns>scale * curve(t / duration)</returns>
        /// <seealso cref="CurveAsset.Evaluate(float, float, float)" />
        public float Evaluate(float t) { return Evaluate(t, defaultScale, defaultDuration); }
        /// <summary>
        /// Evaluate the curve at the given time with respect to the given scale and duration
        /// Time is not clamped between 0 and duration
        /// </summary>
        /// <param name="t">Input time</param>
        /// <param name="duration">Inverse scale factor for input time</param>
        /// <param name="scale">Scale factor for curve value</param>
        /// <returns>scale * curve(t / duration)</returns>
        public float EvaluateUnclamped(float t, float duration, float scale)
        {
            // Evaluate curve in range [0, 1] with respect to given duration
            return curve.Evaluate(t / duration) * scale;
        }
        /// <summary>
        /// Call EvaluateUnclamped using the default scale and duration values
        /// </summary>
        /// <param name="t">Input time</param>
        /// <returns>scale * curve(t / duration)</returns>
        /// <seealso cref="CurveAsset.EvaluateUnclamped(float, float, float)" />
        public float EvaluateUnclamped(float t) { return EvaluateUnclamped(t, defaultScale, defaultDuration); }

        /// <summary>
        /// Generate a coroutine that can be run on a MonoBehaviour to evaluate a callback with the curve's value over time, with added actions called before and after evaluation
        /// </summary>
        /// <param name="duration">Inverse scale factor for input time</param>
        /// <param name="scale">Scale factor for curve value</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <param name="onStart">Action run before curve evaluation begins</param>
        /// <param name="onEnd">Action run after curve evaluation finishes</param>
        /// <returns>The generated routine</returns>
        public IEnumerator Run(float duration, float scale, Action<float> callback, Action onStart, Action onEnd)
        {
            // Call start action
            if (onStart != null) onStart();
            float t = 0;
            while (t < duration)
            {
                // Clamp input time
                t += Time.deltaTime;
                if (t > duration) t = duration;
                // Call callback action with curve value at current time
                callback(EvaluateUnclamped(t, duration, scale));
                // Wait for next frame
                yield return null;
            }
            // Call end action
            if (onEnd != null) onEnd();
        }
        /// <summary>
        /// Generate a coroutine that can be run on a MonoBehaviour to evaluate a callback with the curve's value over time
        /// </summary>
        /// <param name="duration">Inverse scale factor for input time</param>
        /// <param name="scale">Scale factor for curve value</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <returns>The generated routine</returns>
        /// <seealso cref="CurveAsset.Run(float, float, Action{float}, Action, Action)" />
        public IEnumerator Run(float duration, float scale, Action<float> callback) { return Run(duration, scale, callback, null, null); }
        /// <summary>
        /// Generate a coroutine that can be run on a MonoBehaviour to evaluate a callback with the curve's value over time, with added actions called before and after evaluation
        /// </summary>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <param name="onStart">Action run before curve evaluation begins</param>
        /// <param name="onEnd">Action run after curve evaluation finishes</param>
        /// <returns>The generated routine</returns>
        /// <seealso cref="CurveAsset.Run(float, float, Action{float}, Action, Action)" />
        public IEnumerator Run(Action<float> callback, Action onStart, Action onEnd) { return Run(defaultDuration, defaultScale, callback, onStart, onEnd); }
        /// <summary>
        /// Generate a coroutine that can be run on a MonoBehaviour to evaluate a callback with the curve's value over time
        /// </summary>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <returns>The generated routine</returns>
        /// <seealso cref="CurveAsset.Run(float, float, Action{float}, Action, Action)" />
        public IEnumerator Run(Action<float> callback) { return Run(defaultDuration, defaultScale, callback, null, null); }
    }

    /// <summary>
    /// Extension method container for existing classes to work nicely with CurveAssets
    /// </summary>
    /// <seealso cref="CurveAsset" />
    public static class CurveAssetExtensions
    {
        /// <summary>
        /// Extension method to start a curve's Run routine on a given MonoBehaviour
        /// </summary>
        /// <param name="behaviour">The behaviour to start the routine on</param>
        /// <param name="curve">The curve to get the routine from</param>
        /// <param name="duration">Inverse scale factor for input time</param>
        /// <param name="scale">Scale factor for curve value</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <param name="onStart">Action run before curve evaluation begins</param>
        /// <param name="onEnd">Action run after curve evaluation finishes</param>
        /// <seealso cref="CurveAsset.Run(float, float, Action{float}, Action, Action)" />
        public static void RunCurve(this MonoBehaviour behaviour, CurveAsset curve, float duration, float scale, Action<float> callback, Action onStart, Action onEnd)
        {
            behaviour.StartCoroutine(curve.Run(duration, scale, callback, onStart, onEnd));
        }
        /// <summary>
        /// Extension method to start a curve's Run routine on a given MonoBehaviour
        /// </summary>
        /// <param name="behaviour">The behaviour to start the routine on</param>
        /// <param name="curve">The curve to get the routine from</param>
        /// <param name="duration">Inverse scale factor for input time</param>
        /// <param name="scale">Scale factor for curve value</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <seealso cref="CurveAsset.Run(float, float, Action{float})" />
        public static void RunCurve(this MonoBehaviour behaviour, CurveAsset curve, float duration, float scale, Action<float> callback)
        {
            behaviour.StartCoroutine(curve.Run(duration, scale, callback));
        }
        /// <summary>
        /// Extension method to start a curve's Run routine on a given MonoBehaviour
        /// </summary>
        /// <param name="behaviour">The behaviour to start the routine on</param>
        /// <param name="curve">The curve to get the routine from</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <param name="onStart">Action run before curve evaluation begins</param>
        /// <param name="onEnd">Action run after curve evaluation finishes</param>
        /// <seealso cref="CurveAsset.Run(Action{float}, Action, Action)" />
        public static void RunCurve(this MonoBehaviour behaviour, CurveAsset curve, Action<float> callback, Action onStart, Action onEnd)
        {
            behaviour.StartCoroutine(curve.Run(callback, onStart, onEnd));
        }
        /// <summary>
        /// Extension method to start a curve's Run routine on a given MonoBehaviour
        /// </summary>
        /// <param name="behaviour">The behaviour to start the routine on</param>
        /// <param name="curve">The curve to get the routine from</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <seealso cref="CurveAsset.Run(Action{float})" />
        public static void RunCurve(this MonoBehaviour behaviour, CurveAsset curve, Action<float> callback)
        {
            behaviour.StartCoroutine(curve.Run(callback));
        }

        /// <summary>
        /// Extension method to start a curve's Run routine on a given MonoBehaviour if it is not already active
        /// isActive is set true before calling onStart and set false after calling onEnd
        /// </summary>
        /// <param name="behaviour">The behaviour to start the routine on</param>
        /// <param name="curve">The curve to get the routine from</param>
        /// <param name="duration">Inverse scale factor for input time</param>
        /// <param name="scale">Scale factor for curve value</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <param name="onStart">Action run before curve evaluation begins</param>
        /// <param name="onEnd">Action run after curve evaluation finishes</param>
        /// <param name="isActive">Is the curve already being run?</param>
        /// <seealso cref="CurveAsset.Run(float, float, Action{float}, Action, Action)" />
        public static void RunCurve(this MonoBehaviour behaviour, CurveAsset curve, float duration, float scale, Action<float> callback, Action onStart, Action onEnd, Reference<bool> isActive)
        {
            if (isActive) return;
            behaviour.StartCoroutine(curve.Run(duration, scale, callback, () => { isActive.Set(true); onStart(); }, () => { onEnd(); isActive.Set(false); }));
        }
        /// <summary>
        /// Extension method to start a curve's Run routine on a given MonoBehaviour if it is not already active
        /// isActive is set true before evaluation and set false afterward
        /// </summary>
        /// <param name="behaviour">The behaviour to start the routine on</param>
        /// <param name="curve">The curve to get the routine from</param>
        /// <param name="duration">Inverse scale factor for input time</param>
        /// <param name="scale">Scale factor for curve value</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <param name="isActive">Is the curve already being run?</param>
        /// <seealso cref="CurveAsset.Run(float, float, Action{float})" />
        public static void RunCurve(this MonoBehaviour behaviour, CurveAsset curve, float duration, float scale, Action<float> callback, Reference<bool> isActive)
        {
            if (isActive) return;
            behaviour.StartCoroutine(curve.Run(duration, scale, callback, () => isActive.Set(true), () => isActive.Set(false)));
        }
        /// <summary>
        /// Extension method to start a curve's Run routine on a given MonoBehaviour if it is not already active
        /// isActive is set true before calling onStart and set false after calling onEnd
        /// </summary>
        /// <param name="behaviour">The behaviour to start the routine on</param>
        /// <param name="curve">The curve to get the routine from</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <param name="onStart">Action run before curve evaluation begins</param>
        /// <param name="onEnd">Action run after curve evaluation finishes</param>
        /// <param name="isActive">Is the curve already being run?</param>
        /// <seealso cref="CurveAsset.Run(Action{float}, Action, Action)" />
        public static void RunCurve(this MonoBehaviour behaviour, CurveAsset curve, Action<float> callback, Action onStart, Action onEnd, Reference<bool> isActive)
        {
            if (isActive) return;
            behaviour.StartCoroutine(curve.Run(callback, () => { isActive.Set(true); onStart(); }, () => { onEnd(); isActive.Set(false); }));
        }
        /// <summary>
        /// Extension method to start a curve's Run routine on a given MonoBehaviour if it is not already active
        /// isActive is set true before evaluation and set false afterward
        /// </summary>
        /// <param name="behaviour">The behaviour to start the routine on</param>
        /// <param name="curve">The curve to get the routine from</param>
        /// <param name="callback">Callback action that takes an input float value</param>
        /// <param name="isActive">Is the curve already being run?</param>
        /// <seealso cref="CurveAsset.Run(Action{float})" />
        public static void RunCurve(this MonoBehaviour behaviour, CurveAsset curve, Action<float> callback, Reference<bool> isActive)
        {
            if (isActive) return;
            behaviour.StartCoroutine(curve.Run(callback, () => isActive.Set(true), () => isActive.Set(false)));
        }
    }
}
