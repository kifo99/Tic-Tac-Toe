using System.Collections;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _stateName;


    void Start()
    {
        StartCoroutine(GridDrawAnimationLoop());
    }

    IEnumerator GridDrawAnimationLoop()
    {
        while (true)
        {
            _animator.Play(_stateName, 0, 0f);

            yield return new WaitForSeconds(
                _animator.GetCurrentAnimatorStateInfo(0).length
            );
        }
    }
}
