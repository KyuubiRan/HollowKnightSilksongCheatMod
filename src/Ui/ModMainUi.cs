#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HKSC.Ui;

public class ModMainUi : MonoBehaviour
{
    public class PageRender(ModPage page)
    {
        public ModPage RenderPage = page;

        public delegate void OnRender();

        public event OnRender? RenderEvent;
        public Vector2 ScrollPosition = Vector2.zero;

        public void Invoke() => RenderEvent?.Invoke();
    }

    public static ModMainUi Instance { get; private set; } = null!;

    public delegate void EventHandler();

    public delegate void OnToggleShowEventHandler(bool isVisible);

    public event OnToggleShowEventHandler? OnToggleShow;

    public event EventHandler? OnInitialize;

    public event EventHandler? OnUpdate;

    public ModPage CurrentPage { get; private set; } = ModPage.Player;
    private static readonly ModPage[] ModPages = (ModPage[])Enum.GetValues(typeof(ModPage));
    private static readonly Dictionary<ModPage, PageRender> PageRenders = ModPages.ToDictionary(x => x, x => new PageRender(x));

    private bool _isVisible = true;
    private bool _resizing;
    private Vector2 _resizeStartMouse;
    private Vector2 _resizeStartSize;
    private Rect _windowRect = new(100, 100, 450, 500);

    private const float ResizableSize = 28f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Initialize()
    {
        OnInitialize?.Invoke();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    public void AddRender(ModPage page, PageRender.OnRender render)
    {
        if (PageRenders.TryGetValue(page, out var pageRender))
        {
            pageRender.RenderEvent += render;
        }
    }

    private void SetFontSize()
    {
        GUI.skin.label.fontSize = 16;

        GUI.skin.window.fontSize = 16;

        GUI.skin.toggle.fontSize = 16;

        GUI.skin.textField.fontSize = 16;

        GUI.skin.button.fontSize = 16;

        GUI.skin.horizontalSlider.fontSize = 16;
        GUI.skin.verticalSlider.fontSize = 16;
    }

    private void OnGUI()
    {
        if (!_isVisible)
            return;

        _windowRect = GUI.Window(0, _windowRect, DrawWindow, "HKSC");
    }

    private void DrawWindow(int windowID)
    {
        SetFontSize();

        const int buttonsPerRow = 5;
        var total = ModPages.Length;
        GUILayout.BeginHorizontal();
        for (var i = 0; i < total; ++i)
        {
            var elem = ModPages[i];
            if (GUILayout.Button(elem.ToString()))
                CurrentPage = elem;

            if ((i + 1) % buttonsPerRow != 0 || i + 1 == total) continue;

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
        }

        GUILayout.EndHorizontal();

        foreach (var (page, renderer) in PageRenders)
        {
            if (page != CurrentPage) continue;
            renderer.ScrollPosition = GUILayout.BeginScrollView(renderer.ScrollPosition);
            renderer.Invoke();
            GUILayout.EndScrollView();
            break;
        }

        DrawResizable(ResizableSize);
        GUI.DragWindow();
    }

    private void DrawResizable(float handleSize)
    {
        var resizeRect = new Rect(_windowRect.width - handleSize, _windowRect.height - handleSize, handleSize,
                                  handleSize);

        var e = Event.current;
        if (e.type == EventType.MouseDown && resizeRect.Contains(e.mousePosition))
        {
            _resizing = true;
            _resizeStartMouse = e.mousePosition;
            _resizeStartSize = new Vector2(_windowRect.width, _windowRect.height);
            e.Use();
        }

        if (_resizing)
        {
            if (e.type is EventType.MouseDrag or EventType.MouseMove)
            {
                var delta = e.mousePosition - _resizeStartMouse;
                _windowRect.width = Mathf.Max(100, _resizeStartSize.x + delta.x);
                _windowRect.height = Mathf.Max(100, _resizeStartSize.y + delta.y);
                e.Use();
            }

            if (e.type == EventType.MouseUp)
            {
                _resizing = false;
                e.Use();
            }
        }
    }

    public void ToggleShow(bool? visible = null)
    {
        _isVisible = visible ?? !_isVisible;
        OnToggleShow?.Invoke(_isVisible);
    }
}