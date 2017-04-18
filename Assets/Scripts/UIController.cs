using UnityEngine;

public class UIController : MonoBehaviour {

    // This class is used to make sure world space UI
    // elements such as the health bar face the correct direction.

    [SerializeField] private bool useRelativeRotation = true;       // Use relative rotation should be used for this gameobject?

    private Quaternion relativeRotation;                            // The local rotatation at the start of the scene.


    private void Start() {

        relativeRotation = transform.rotation;

    }


    private void Update() {

        if (useRelativeRotation) {
            transform.rotation = relativeRotation;
        }

    }

}