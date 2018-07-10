using UnityEngine;

public class BuildManager : MonoBehaviour {
    [SerializeField]
    private GameObject buildEffectPrefab;

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

    private TurretBlueprint turretBlueprint;

    public bool CanBuild { get { return turretBlueprint != null; } }
    public bool HasMoney { get { return Player.Money >= turretBlueprint.cost; } }

    public void SelectTurretToBuild(TurretBlueprint turretBlueprint)
    {
        this.turretBlueprint = turretBlueprint;
    }

    public void BuildTurretOn(Node node)
    {
        if (Player.Money >= turretBlueprint.cost)
        {
            Vector3 position = node.transform.position;
            position.y = 0.5f;
            node.turret = Instantiate(turretBlueprint.prefab, position, Quaternion.identity);
            Player.Money -= turretBlueprint.cost;
            Destroy(Instantiate(buildEffectPrefab, position, Quaternion.identity, node.turret.transform), 15f);
        }
    }
}
