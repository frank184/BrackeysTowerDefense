using UnityEngine;

public class BuildManager : MonoBehaviour {

    #region Singleton
    public static BuildManager singleton;

    private void Awake()
    {
        if (singleton != null)
        {
            Debug.LogError("More than one BuildManager singleton");
            Destroy(this);
            return;
        }
        singleton = this;
    }
    #endregion

    private GameObject turretPrefab;

    public GameObject standardTurretPrefab;
    public GameObject anotherTurrfetPrefab;

    public void SetTurretPrefab(GameObject turret)
    {
        turretPrefab = turret;
    }

    public GameObject GetTurretPrefab()
    {
        return turretPrefab;
    }
}
