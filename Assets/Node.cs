using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {
    private BuildManager buildManager;
    public Color defaultColor;
    public Color hoverColor;

    private Renderer rend;

    private GameObject turret;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        buildManager = BuildManager.singleton;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (buildManager.GetTurretPrefab() == null) return;
        if (turret != null) return;

        GameObject turretPrefab = BuildManager.singleton.GetTurretPrefab();
        Vector3 position = new Vector3(transform.position.x, turretPrefab.transform.position.y, transform.position.z);
        turret = Instantiate(turretPrefab, position, transform.rotation);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (buildManager.GetTurretPrefab() == null) return;
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = defaultColor;
    }
}
