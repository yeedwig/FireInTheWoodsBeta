using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kindlings : MonoBehaviour
{
    public Dictionary<string,string> kindlings; //불 쏘시개 리스트
    
    // Start is called before the first frame update
    void Start()
    {
        kindlings=new Dictionary<string,string>();

        //공격 종류
        kindlings.Add("SeedsSeedsSeeds","normalAttack");
        kindlings.Add("BranchBranchWorm","longAttack");
        kindlings.Add("BranchBranchCarrot","swordAttack");
        kindlings.Add("BranchBranchBerries","largeAttack");
        kindlings.Add("BranchBranchFrog","gunAttack");
        kindlings.Add("BranchBranchSeeds","spinAttack");
        //불 관련
        kindlings.Add("BranchBranchBranch","addHealth");
        kindlings.Add("BerriesBerriesBerries","addMaxHealth");
        kindlings.Add("LeafLeafLeaf","executeDamage");
        kindlings.Add("VineVineHoneyComb","barrier");
        kindlings.Add("VineHoneyCombTurtleCarapace","invincible");

        //영혼 계약 휘귀 아이템들 만들어지면
        kindlings.Add("BranchFeatherTurtleCarapace","TurtleMode");
        kindlings.Add("BranchFeatherTigerTooth","TigerMode");
        kindlings.Add("BranchFeatherFalconFeather","FalconMode");
        kindlings.Add("BranchFeatherAntler","DeerMode");
        kindlings.Add("BranchFeatherHoneyComb","BearMode");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string FindFireType(string itemStream)
    {
        if(kindlings.ContainsKey(itemStream)){
            return kindlings[itemStream];
        }
        else
        {
            return "not found";
        }
    }
}
