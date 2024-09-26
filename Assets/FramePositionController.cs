using UnityEngine;
using System.Collections;
using Oculus.Interaction;
using System;

/// <summary>
/// Controls the position and rotation of a frame object, allowing it to switch between an initial position and a floating position.
/// </summary>
public class FramePositionController : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _floatPosition;
    private Quaternion _floatRotation;
    private PointableUnityEventWrapper _pointableUnityEventWrapper;
    private bool isFloating;

    /// <summary>
    /// The transform of the frame object to be controlled.
    /// </summary>
    [SerializeField] private Transform frameTransform;

    /// <summary>
    /// The transform representing the floating position and rotation.
    /// </summary>
    [SerializeField] private Transform floatTransform;

    /// <summary>
    /// The duration of the reset animation in seconds.
    /// </summary>
    [SerializeField] private float resetDuration = 1f;

    /// <summary>
    /// The animation curve used for smoothing the movement and rotation transitions.
    /// </summary>
    [SerializeField] private AnimationCurve smoothCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    /// <summary>
    /// Initializes the component by saving initial and float positions/rotations and setting up event listeners.
    /// </summary>
    private void Start()
    {
        // Save the initial position of the object
        _initialPosition = frameTransform.position;
        _initialRotation = frameTransform.rotation;

        // Save the float position of the object
        _floatPosition = floatTransform.position;
        _floatRotation = floatTransform.rotation;

        _pointableUnityEventWrapper = frameTransform.GetComponent<PointableUnityEventWrapper>();
        _pointableUnityEventWrapper.WhenUnselect.AddListener(ResetPosition);
    }

    /// <summary>
    /// Resets the position of the frame to either its initial or floating position, depending on its current state.
    /// </summary>
    /// <param name="arg0">The PointerEvent argument (not used in this method).</param>
    public void ResetPosition(PointerEvent arg0)
    {
        StopAllCoroutines();
        if (isFloating)
        {
            StartCoroutine(SmoothMoveToCoroutine(_floatPosition, _floatRotation));
        }
        else
        {
            StartCoroutine(SmoothMoveToCoroutine(_initialPosition, _initialRotation));
        }
    }

    /// <summary>
    /// Toggles the frame between its initial and floating positions.
    /// </summary>
    public void ActivateFloat()
    {
        StopAllCoroutines();
        if (isFloating)
        {
            StartCoroutine(SmoothMoveToCoroutine(_initialPosition, _initialRotation));
            isFloating = false;
        }
        else
        {
            StartCoroutine(SmoothMoveToCoroutine(_floatPosition, _floatRotation));
            isFloating = true;
        }
    }

    /// <summary>
    /// Coroutine that smoothly moves and rotates the frame to a target position and rotation.
    /// </summary>
    /// <param name="targetPosition">The target position to move to.</param>
    /// <param name="targetRotation">The target rotation to rotate to.</param>
    /// <returns>An IEnumerator for coroutine execution.</returns>
    private IEnumerator SmoothMoveToCoroutine(Vector3 targetPosition, Quaternion targetRotation)
    {
        Vector3 startPosition = frameTransform.position;
        Quaternion startRotation = frameTransform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < resetDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / resetDuration;
            float smoothT = smoothCurve.Evaluate(t);

            // Interpolate position
            frameTransform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);

            // Interpolate rotation
            frameTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, smoothT);

            yield return null;
        }

        // Ensure the object is exactly at the target position and rotation
        frameTransform.position = targetPosition;
        frameTransform.rotation = targetRotation;
    }
}