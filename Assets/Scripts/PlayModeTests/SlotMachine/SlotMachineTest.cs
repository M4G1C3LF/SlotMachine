using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class SlotMachineTest
{
    [SetUp]
    public void SetUp(){
    }

    [TearDown]
    public void TearDown(){

    }

    public List<Payline> GetPaylines(){
        List<Payline> paylines = new List<Payline>();
        Payline payline1 = new Payline();
        payline1.row1 = new bool[] {true,true,true,true,true};
        payline1.row2 = new bool[] {false,false,false,false,false};
        payline1.row3 = new bool[] {false,false,false,false,false};
        paylines.Add(payline1);

        Payline payline2 = new Payline();
        payline2.row1 = new bool[] {false,false,false,false,false};
        payline2.row2 = new bool[] {true,true,true,true,true};
        payline2.row3 = new bool[] {false,false,false,false,false};
        paylines.Add(payline2);

        Payline payline3 = new Payline();
        payline2.row1 = new bool[] {false,false,false,false,false};
        payline2.row2 = new bool[] {false,false,false,false,false};
        payline2.row3 = new bool[] {true,true,true,true,true};
        paylines.Add(payline2);

        return paylines;
    }
    
    
    // A Test behaves as an ordinary method
    [Test]
    public void SlotMachineAdd50CreditsSuccess()
    {
        //Arrange
        int expectedCredits = 50;
        GameObject slotMachinePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Paths.PREFABS_PATH_SLOT_MACHINE);
        SlotMachine slotMachine = GameObject.Instantiate(slotMachinePrefab,Vector3.zero,new Quaternion()).GetComponent<SlotMachine>();
        //Act
        slotMachine.Add50Credits();

        //Asert
        Assert.AreEqual(expectedCredits,slotMachine.GetCredits());
    }
    [Test]
    public void SlotMachineSpinReelSucess()
    {
        //Arrange
        bool expectedResult = true;
        GameObject slotMachinePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Paths.PREFABS_PATH_SLOT_MACHINE);
        SlotMachine slotMachine = GameObject.Instantiate(slotMachinePrefab,Vector3.zero,new Quaternion()).GetComponent<SlotMachine>();
        //Act
        slotMachine.Add50Credits();
        slotMachine.SpinReels();

        //Asert
        Assert.AreEqual(expectedResult,slotMachine.IsSpinning());
    }
    [Test]
    public void SlotMachineSpinReelFail()
    {
        //Arrange
        bool expectedResult = false;
        GameObject slotMachinePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Paths.PREFABS_PATH_SLOT_MACHINE);
        SlotMachine slotMachine = GameObject.Instantiate(slotMachinePrefab,Vector3.zero,new Quaternion()).GetComponent<SlotMachine>();
        //Act
        slotMachine.SpinReels();

        //Asert
        Assert.AreEqual(expectedResult,slotMachine.IsSpinning());
    }
    [Test]
    public void SlotMachineCheckPaylineSuccess()
    {
        //Arrange
        int expectedCredits = 20;
        GameObject slotMachinePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(Paths.PREFABS_PATH_SLOT_MACHINE);
        SlotMachine slotMachine = GameObject.Instantiate(slotMachinePrefab,Vector3.zero,new Quaternion()).GetComponent<SlotMachine>();

        //Act
        slotMachine.CheckPaylines(slotMachine.GetPaylinesInChildren(),slotMachine.GetReelsInChildren());

        //Asert
        Assert.AreEqual(expectedCredits,slotMachine.GetCredits());
    }
}
