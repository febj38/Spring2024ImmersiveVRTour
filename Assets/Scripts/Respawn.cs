using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Grab;
using Oculus.Interaction.GrabAPI;
using Oculus.Interaction.Input;
using UnityEngine;

namespace Oculus.Interaction.HandGrab {
    public class Respawn : MonoBehaviour {

        [SerializeField] private GameObject ramenBowl;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnValueY;
        [SerializeField] private Quaternion originalRotation;
        [SerializeField] private HandGrabInteractable handGrab;
        [SerializeField] private PhysicsGrabbable physicsGrab;
        [SerializeField] private Grabbable grab;
        // Start is called before the first frame update
        void Start()
        {
            // spawnPoint = ramenBowl.transform.position;
            // originalRotation = ramenBowl.transform.rotation;
        }

        void Awake() {
            handGrab = GetComponent<HandGrabInteractable>();
            physicsGrab = GetComponent<PhysicsGrabbable>();
            grab = GetComponent<Grabbable>();

        }

        // Update is called once per frame
        private void Update()
        {

            if (ramenBowl.transform.position.y < spawnValueY && (handGrab == null)) {
                RespawnObject();
            }
        }

        void RespawnObject() {
            transform.position = spawnPoint.position;
            transform.rotation = originalRotation;
        }
    }
}
