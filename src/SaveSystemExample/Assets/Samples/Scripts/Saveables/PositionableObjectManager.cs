using Newtonsoft.Json.Linq;
using PillowCoding.SaveManager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable enable

public class PositionableObjectManager : MonoBehaviour, ISaveable
{
    private SaveManager SaveManager => SaveManager.Instance;

    public string Key => "Moving objects";

    private List<GameObject> _spawnedObjects = new List<GameObject>();
    [SerializeField] private Transform _objectContainer;
    [SerializeField] private GameObject _objectBasePrefab;

    private void Awake()
    {
        this.SaveManager.Register(this);
    }

    private void OnDestroy()
    {
        this.SaveManager.UnRegister(this);
    }

    public void Spawn()
    {
        var position = new Vector2(Random.Range(-8, 8), Random.Range(-4, 4));
        var rotation = new Quaternion(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(0, 359), 0);
        this.Spawn(position, rotation);
    }

    public void Spawn(Vector2 position, Quaternion rotation)
    {
        var @object = Instantiate(this._objectBasePrefab, position, rotation, this._objectContainer);
        @object.SetActive(true);
        this._spawnedObjects.Add(@object);
    }

    public void FromSaveable(JContainer? @object)
    {
        if (@object == null)
        {
            Debug.Log("Skipping deserializing because there is no data.", this);
            return;
        }

        // Remove current objects in the scene.
        foreach (var spawnedObject in this._spawnedObjects)
        {
            Object.Destroy(spawnedObject);
        }

        var objectTransforms = @object.ToObject<ObjectTransform[]>();
        if (objectTransforms == null) {
            Debug.LogWarning("Object transforms could not be properly deserialized.", this);
            return;
        }

        foreach (var objectTransform in objectTransforms)
        {
            var position = new Vector2(objectTransform.positionX, objectTransform.positionY);
            var rotation = new Quaternion(objectTransform.rotationX, objectTransform.rotationY, objectTransform.rotationZ, 0);
            this.Spawn(position, rotation);
        }
    }

    public object ToSaveable()
    {
        return this._spawnedObjects.Select(@object =>
        {
            var objectPosition = @object.transform.position;
            var objectRotation = @object.transform.rotation;
            return new ObjectTransform(
                objectPosition.x,
                objectPosition.y,
                objectRotation.x,
                objectRotation.y,
                objectRotation.z);
        });
    }
}
