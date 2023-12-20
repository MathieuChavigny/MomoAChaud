using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonhommeNeige : MonoBehaviour
{
    [SerializeField] private GameObject _bouleNeige;
    [SerializeField] private Transform _origine;
    [SerializeField] private int _nbBouleNeige;

    // Start is called before the first frame update
    void Start()
    {
        _nbBouleNeige = Random.Range(10, 30);
        Coroutine coroutineBonhommeNeige = StartCoroutine(CoroutLancerBouleNeige());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CoroutLancerBouleNeige()
    {

        while (_nbBouleNeige > 0)
        {
            yield return new WaitForSeconds(1);

            transform.Rotate(0, Random.Range(0, 361), 0);
            Instantiate(_bouleNeige, _origine.position, transform.rotation);
            _nbBouleNeige--;
        }
        Invoke("AutoDestruction", 1f);
        yield break;
    }

    private void AutoDestruction()
    {
        Destroy(gameObject);
    }
}
