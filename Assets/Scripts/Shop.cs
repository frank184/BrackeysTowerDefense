using UnityEngine;

public class Shop : MonoBehaviour {
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;

    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.singleton;
    }

    public void SetStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SetMissileLauncher()
    {
        buildManager.SelectTurretToBuild(missileLauncher);
    }
}
