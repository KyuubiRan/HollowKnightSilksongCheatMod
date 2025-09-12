using HKSC.Managers;

namespace HKSC.Misc;

public class ConfigObject<T>(string key, T value = default)
{
    public string Key { get; private set; } = key;
    public T _value = value;

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

    public static implicit operator T(ConfigObject<T> config) => config.Value;
}