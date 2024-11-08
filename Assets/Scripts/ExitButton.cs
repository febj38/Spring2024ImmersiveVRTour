using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
        [SerializeField] private GameObject leftIndex;
        [SerializeField] private GameObject rightIndex;
        [SerializeField] private GameObject leftControllerIndex;
        [SerializeField] private GameObject rightControllerIndex;
        [SerializeField] private float lidistance;
        [SerializeField] private float ridistance;
        [SerializeField] private float licdistance;
        [SerializeField] private float ricdistance;
        [SerializeField] private float selectDistance;
        [SerializeField] private string sceneName;
        [SerializeField] private GameObject OVRCameraRig;
        [SerializeField] private AudioSource buzzSound;
        private bool click = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (click) return;
        lidistance = Vector3.Distance (this.transform.position, leftIndex.transform.position);
        ridistance = Vector3.Distance (this.transform.position, rightIndex.transform.position);
        licdistance = Vector3.Distance (this.transform.position, leftControllerIndex.transform.position);
        ricdistance = Vector3.Distance (this.transform.position, rightControllerIndex.transform.position);
        if (lidistance < selectDistance || ridistance < selectDistance || licdistance < selectDistance || ricdistance < selectDistance)
        {
            click = true;
            StartCoroutine(SoundAndSwitch());
        }  
    }
    private IEnumerator SoundAndSwitch() {
        if (buzzSound != null) {
            buzzSound.Play();
            AudioClip clip = buzzSound.GetComponent<AudioClip>();
            buzzSound.PlayOneShot(clip);
            yield return new WaitForSeconds(buzzSound.clip.length);
        }
        SceneManager.LoadScene(sceneName);
    }  
}
