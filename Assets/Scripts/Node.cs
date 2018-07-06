using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
    private BuildManager buildManager;
    public Color defaultColor;
    public Color hoverColor = Color.green;
    public Color errorColor = Color.red;
    public GameObject turret;

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.color;
        buildManager = BuildManager.singleton;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!buildManager.CanBuild) return;
        if (turret != null) return;
        buildManager.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!buildManager.CanBuild) return;
        if (turret != null) return;
        if (buildManager.HasMoney)
            rend.material.color = hoverColor;
        else
            rend.material.color = errorColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = defaultColor;
    }
}
