using HKSC.Managers;

namespace HKSC.Misc;

public class ConfigObject<T>(string key, T value = default)
{
    public string Key { get; private set; } = key;
    public T _value = value;
    
    public delegate void OnChangedHandler(ConfigObject<T> config);
    public event OnChangedHandler OnChanged;

    public T Value
    {
        get => _value;
        set
        {
            if (Equals(_value, value)) return;
            _value = value;
            OnChanged?.Invoke(this);
            CfgManager.FireChanged(this);
        }
    }
    
    public ConfigObject<T> AddOnChangedListener(OnChangedHandler handler)
    {
        OnChanged += handler;
        return this;
    }

    public void FireChanged()
    {
        CfgManager.FireChanged(this);
    }

    public static implicit operator T(ConfigObject<T> config) => config.Value;
}