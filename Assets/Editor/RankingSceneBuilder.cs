// Put this file in Assets/Editor/ (create the folder if needed).
// Then use the menu: Tools > Build Ranking Scene
using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public static class RankingSceneBuilder
{
    const string ScenePath = "Assets/Scenes/RankingScene.unity";

    [MenuItem("Tools/Build Ranking Scene")]
    static void Build()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Camera
        var camGO = new GameObject("Main Camera");
        camGO.tag = "MainCamera";
        camGO.transform.position = new Vector3(0, 0, -10);
        var cam = camGO.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.12f, 0.14f, 0.18f);
        cam.orthographic = true;
        camGO.AddComponent<AudioListener>();

        // Canvas
        var canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvasGO.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGO.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;
        Transform canvas = canvasGO.transform;

        // EventSystem (new Input System, like your other scenes)
        var esGO = new GameObject("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
        esGO.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();

        // Title
        var title = MakeText(canvas, "Title", "Ranking", 64, TextAlignmentOptions.Center,
            new Vector2(0.5f, 1f), new Vector2(0, -90), new Vector2(1000, 100));

        // List panel
        var panel = new GameObject("ListPanel", typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(canvas, false);
        panel.GetComponent<Image>().color = new Color(0, 0, 0, 0.45f);
        Place(panel.GetComponent<RectTransform>(), new Vector2(0.5f, 0.5f), new Vector2(0, -40), new Vector2(1100, 560));

        var list = MakeText(panel.transform, "ListText", "", 38, TextAlignmentOptions.TopLeft,
            new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(1040, 500));

        // RankingUI controller
        var uiGO = new GameObject("RankingUI");
        var ui = uiGO.AddComponent<RankingUI>();
        ui.titleText = title;
        ui.listText = list;

        // Difficulty buttons
        MakeDifficultyButton(canvas, ui, "Easy", "PrincipalGameSceneEasy", -360);
        MakeDifficultyButton(canvas, ui, "Normal", "PrincipalGameSceneNormal", 0);
        MakeDifficultyButton(canvas, ui, "Hard", "PrincipalGameSceneHard", 360);

        // Back button
        var back = MakeButton(canvas, "Back", "Back", new Vector2(0.5f, 0f), new Vector2(0, 80), new Vector2(300, 80));
        UnityEventTools.AddPersistentListener(back.onClick, ui.Back);

        // Save + add to Build Profiles scene list
        Directory.CreateDirectory("Assets/Scenes");
        EditorSceneManager.SaveScene(scene, ScenePath);
        AddToBuildSettings(ScenePath);

        Debug.Log("Ranking scene created at " + ScenePath);
    }

    static void MakeDifficultyButton(Transform parent, RankingUI ui, string label, string sceneName, float x)
    {
        var b = MakeButton(parent, label, label, new Vector2(0.5f, 1f), new Vector2(x, -210), new Vector2(300, 80));
        UnityEventTools.AddStringPersistentListener(b.onClick, ui.Show, sceneName);
    }

    static void Place(RectTransform rt, Vector2 anchor, Vector2 pos, Vector2 size)
    {
        rt.anchorMin = rt.anchorMax = rt.pivot = anchor;
        rt.anchoredPosition = pos;
        rt.sizeDelta = size;
    }

    static TextMeshProUGUI MakeText(Transform parent, string name, string text, int size,
        TextAlignmentOptions align, Vector2 anchor, Vector2 pos, Vector2 sizeDelta)
    {
        var go = new GameObject(name, typeof(RectTransform));
        go.transform.SetParent(parent, false);
        var t = go.AddComponent<TextMeshProUGUI>();
        t.text = text;
        t.fontSize = size;
        t.alignment = align;
        t.color = Color.white;
        t.raycastTarget = false;
        Place(go.GetComponent<RectTransform>(), anchor, pos, sizeDelta);
        return t;
    }

    static Button MakeButton(Transform parent, string name, string label, Vector2 anchor, Vector2 pos, Vector2 size)
    {
        var go = new GameObject("Button" + name, typeof(RectTransform), typeof(Image), typeof(Button));
        go.transform.SetParent(parent, false);
        var img = go.GetComponent<Image>();
        img.color = new Color(0.22f, 0.22f, 0.22f, 0.86f);
        Place(go.GetComponent<RectTransform>(), anchor, pos, size);

        var btn = go.GetComponent<Button>();
        btn.targetGraphic = img;

        MakeText(go.transform, "Text", label, 40, TextAlignmentOptions.Center,
            new Vector2(0.5f, 0.5f), Vector2.zero, size);
        return btn;
    }

    static void AddToBuildSettings(string path)
    {
        var scenes = EditorBuildSettings.scenes;
        foreach (var s in scenes)
            if (s.path == path) return;

        var list = new System.Collections.Generic.List<EditorBuildSettingsScene>(scenes)
        {
            new EditorBuildSettingsScene(path, true)
        };
        EditorBuildSettings.scenes = list.ToArray();
    }
}
