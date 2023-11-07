using System;
using UnityEngine;
using UnityEngine.Pool;

public class LevelObjectSpawner : MonoBehaviour
{
    public Action<Transform> OnLevelObjectGetAction;
    public Action<Transform> OnLevelObjectReleaseAction;
    public static LevelObjectSpawner Instance { get; private set; }

    [SerializeField] private Transform[] _levelObjects;
    ObjectPool<Transform> _levelObjectsPool;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _levelObjectsPool = new ObjectPool<Transform>(CreateLevelObjectPool, OnGetLevelObjectPool, OnReleaseLevelObjectPool, OnDestroyLevelObjectPool, true, 60, 100);
    }

    #region Pool

    private Transform CreateLevelObjectPool()
    {
        Transform temporaryLevelObject = Instantiate(_levelObjects[UnityEngine.Random.Range(0, _levelObjects.Length)]);
        return temporaryLevelObject;
    }

    private void OnGetLevelObjectPool(Transform levelObject)
    {
        levelObject.gameObject.SetActive(true);
        OnLevelObjectGetAction?.Invoke(levelObject);
    }

    private void OnReleaseLevelObjectPool(Transform levelObject)
    {
        OnLevelObjectReleaseAction?.Invoke(levelObject);
        levelObject.gameObject.SetActive(false);
    }

    private void OnDestroyLevelObjectPool(Transform levelObject)
    {
        Destroy(levelObject.gameObject);
    }

    #endregion

    public Transform GetLevelObjectFromPool()
    {
        Transform levelObject = _levelObjectsPool.Get();
        return levelObject;
    }

    public void ReleaseLevelObject(Transform levelObject)
    {
        _levelObjectsPool.Release(levelObject);
    }
}