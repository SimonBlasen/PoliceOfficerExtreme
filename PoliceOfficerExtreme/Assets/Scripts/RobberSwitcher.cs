using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberSwitcher : MonoBehaviour
{
    [SerializeField]
    private Transform robbyRobberMain = null;
    [SerializeField]
    private Transform robbyRobberMainRig = null;


    private GameObject instModel = null;
    private GameObject instModelNonRig = null;

    private RigAgentManager rigAgentManager = null;

    // Start is called before the first frame update
    void Start()
    {
        rigAgentManager = GetComponent<RigAgentManager>();
        rigAgentManager.NonRigidMeshes = new MeshRenderer[0];

        if (robbyRobberMain.childCount > 0)
        {
            instModel = robbyRobberMain.GetChild(0).gameObject;
        }
        if (robbyRobberMainRig.childCount > 0)
        {
            instModelNonRig = robbyRobberMainRig.GetChild(0).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRobberModels(GameObject modelNonRig, GameObject modelRig)
    {
        if (instModel != null)
        {
            Destroy(instModel);
        }
        if (instModelNonRig != null)
        {
            Destroy(instModelNonRig);
        }

        instModel = Instantiate(modelNonRig, robbyRobberMain);
        instModel.transform.localPosition = Vector3.zero;
        instModel.transform.localRotation = Quaternion.identity;
        instModelNonRig = Instantiate(modelRig, robbyRobberMainRig);
        instModelNonRig.transform.localPosition = Vector3.zero;
        instModelNonRig.transform.localRotation = Quaternion.identity;


        MeshRenderer[] allChildMRs = instModel.GetComponentsInChildren<MeshRenderer>();
        rigAgentManager.NonRigidMeshes = allChildMRs;
    }

    public void DestroyInstances()
    {
        if (instModel != null)
        {
            Destroy(instModel);
        }
        if (instModelNonRig != null)
        {
            Destroy(instModelNonRig);
        }
    }
}
