using HKSC.Managers;

namespace HKSC.Misc;

public class ConfigObject<T>
{
    public string Key { get; private set; }
    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            if (Equals(_value, value)) return;
            _value = value;
            CfgManager.FireChanged(this);
        }
    }

    public void FireChanged()
    {
        CfgManager.FireChanged(this);
    }
    
    public ConfigObject(string key, T value = default)
    {
        Key = key;
        Value = value;
    }

    public static implicit operator T(ConfigObject<T> config) => config.Value;
}