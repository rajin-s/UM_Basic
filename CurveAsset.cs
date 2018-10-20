using System;
using System.Collections;
using UnityEngine;

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
    /// <seealso cref="CurveAsset.Evaluate(float, float, float)" />
    /// <param name="t">Input time</param>
    /// <returns>scale * curve(t / duration)</returns>
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
    /// <seealso cref="CurveAsset.EvaluateUnclamped(float, float, float)" />
    /// <param name="t">Input time</param>
    /// <returns>scale * curve(t / duration)</returns>
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
    public IEnumerator Run(float duration, float scale, Action<float> callback) { return Run(duration, scale, callback, null, null); }
    /// <summary>
    /// Generate a coroutine that can be run on a MonoBehaviour to evaluate a callback with the curve's value over time
    /// </summary>
    /// <param name="callback">Callback action that takes an input float value</param>
    /// <returns>The generated routine</returns>
    public IEnumerator Run(Action<float> callback) { return Run(defaultDuration, defaultScale, callback, null, null); }
}