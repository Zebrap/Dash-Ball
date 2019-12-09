using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public Animator aniamtor;

    void Update()
    {
        
    }

    public void FadeToNextScane()
    {

    }

    public void FadeToScane(int levelIndex)
    {
        aniamtor.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {

    }
}
