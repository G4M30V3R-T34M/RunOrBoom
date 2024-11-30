using FeTo.Singleton;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    Animator animator;
    int scene;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public void LoadScene(int scene)
    {
        Time.timeScale = 0;
        this.scene = scene;
        animator.SetTrigger("End");
    }

    public void DoChangeScene()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}

