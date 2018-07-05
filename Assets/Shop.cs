using UnityEngine;

public class Shop : MonoBehaviour {
    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.singleton;
    }

    public void SetStandardTurret()
    {
        buildManager.SetTurretPrefab(buildManager.standardTurretPrefab);
    }

    public void SetAnotherTurret()
    {
        buildManager.SetTurretPrefab(buildManager.anotherTurrfetPrefab);
    }
}
