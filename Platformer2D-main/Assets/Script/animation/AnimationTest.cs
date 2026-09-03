using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator animator;

    public KeyCode keyToTrigger = KeyCode.Z;
    public KeyCode keyToExit = KeyCode.X;
    public string triggerToPlay = "Ball";

    private void OnValidate()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(keyToTrigger))
        {
            animator.SetBool (triggerToPlay, true);
        }
        else if (Input.GetKeyDown(keyToExit))
        {
            animator.SetBool (triggerToPlay, false);
        }
    }
}
