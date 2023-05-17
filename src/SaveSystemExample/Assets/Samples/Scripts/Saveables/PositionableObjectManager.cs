using Newtonsoft.Json.Linq;
using PillowCoding.SaveManager;
using UnityEngine;

#nullable enable

public class PositionableObjectManager : MonoBehaviour, ISaveable
{
    private SaveManager SaveManager => SaveManager.Instance;

    public string Key => "Moving objects";

    private void Awake()
    {
        this.SaveManager.Register(this);
    }

    private void OnDestroy()
    {
        this.SaveManager.UnRegister(this);
    }

    public void FromSaveable(JObject @object)
    {
        throw new System.NotImplementedException();
    }

    public object ToSaveable()
    {
        throw new System.NotImplementedException();
    }
}
