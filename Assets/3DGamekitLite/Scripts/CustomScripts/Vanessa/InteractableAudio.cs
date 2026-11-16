using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu(fileName = "InteractableAudio", menuName = "Scriptable Objects/InteractableAudio")]
public class InteractableAudio : ScriptableObject
{
    //Declares a EventReference that holds a reference to our specific event 
    [SerializeField] private EventReference pickupHealthAudio, boxDestroyAudio, switchAudio, doorAudio;

    // Declares a method that shuld play the pickupHealthAudio sound without any 3D-positioning
    public void PickupHealthPlay()
    {
        RuntimeManager.PlayOneShot(pickupHealthAudio);
    }
}
