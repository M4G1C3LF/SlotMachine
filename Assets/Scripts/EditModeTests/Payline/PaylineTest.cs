using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PaylineTest
{
    Payline payline;
    int maxTruesPerColum;
    [SetUp]
    public void SetUp(){
        payline = new Payline();
        maxTruesPerColum = 1;
    }

    [TearDown]
    public void TearDown(){

    }

    [Test]
    public void PaylineHavingSingleTrueOnColumSuccess()
    {
        //Arrange
        payline.row1 = new bool[] {true};
        payline.row2 = new bool[] {false};
        payline.row3 = new bool[] {false};

        //Act
        int truesFound = GetTruesFoundPerColum(payline, 0);

        //Asert
        Assert.AreEqual(maxTruesPerColum,truesFound);
    }
    [Test]
    public void PaylineHavingMoreThanATrueOnColumFails()
    {
        //Arrange
        payline.row1 = new bool[] {true};
        payline.row2 = new bool[] {false};
        payline.row3 = new bool[] {true};

        //Act
        int truesFound = GetTruesFoundPerColum(payline, 0);

        //Asert
        Assert.AreNotEqual(maxTruesPerColum,truesFound);
    }
    [Test]
    public void PaylineSingleLineTopPaylineSuccess()
    {
        //Arrange
        payline.row1 = new bool[] {true,    true,   true,   true,   true};
        payline.row2 = new bool[] {false,   false,  false,  false,  false};
        payline.row3 = new bool[] {false,   false,  false,  false,  false};

        //Act
        List<int> truesFoundPerColumn = new List<int>();
        for (int i = 0; i < payline.row1.Length; i++){
            truesFoundPerColumn.Add(GetTruesFoundPerColum(payline, i));
        }

        //Asert
        truesFoundPerColumn.ForEach(truesInColumn => {
            Assert.AreEqual(maxTruesPerColum,truesInColumn);
        });
    }
    [Test]
    public void PaylineSingleLineMidPaylineSuccess()
    {
        //Arrange
        payline.row1 = new bool[] {false,   false,  false,  false,   false};
        payline.row2 = new bool[] {true,    true,   true,   true,     true};
        payline.row3 = new bool[] {false,   false,  false,  false,   false};

        //Act
        List<int> truesFoundPerColumn = new List<int>();
        for (int i = 0; i < payline.row1.Length; i++){
            truesFoundPerColumn.Add(GetTruesFoundPerColum(payline, i));
        }

        //Asert
        truesFoundPerColumn.ForEach(truesInColumn => {
            Assert.AreEqual(maxTruesPerColum,truesInColumn);
        });
    }
    [Test]
    public void PaylineSingleLineBottomdPaylineSuccess()
    {
        //Arrange
        payline.row1 = new bool[] {false,   false,  false,  false,  false};
        payline.row2 = new bool[] {false,   false,  false,  false,  false};
        payline.row3 = new bool[] {true,    true,   true,   true,   true};

        //Act
        List<int> truesFoundPerColumn = new List<int>();
        for (int i = 0; i < payline.row1.Length; i++){
            truesFoundPerColumn.Add(GetTruesFoundPerColum(payline, i));
        }

        //Asert
        truesFoundPerColumn.ForEach(truesInColumn => {
            Assert.AreEqual(maxTruesPerColum,truesInColumn);
        });
    }
    [Test]
    public void PaylineWPaylineSuccess()
    {
        //Arrange
        payline.row1 = new bool[] {true,    false,  true,   false,  true};
        payline.row2 = new bool[] {false,   false,  false,  false,  false};
        payline.row3 = new bool[] {false,   true,   false,  true,   false};

        //Act
        List<int> truesFoundPerColumn = new List<int>();
        for (int i = 0; i < payline.row1.Length; i++){
            truesFoundPerColumn.Add(GetTruesFoundPerColum(payline, i));
        }

        //Asert
        truesFoundPerColumn.ForEach(truesInColumn => {
            Assert.AreEqual(maxTruesPerColum,truesInColumn);
        });
    }
    [Test]
    public void PaylineVPaylineSuccess()
    {
        //Arrange
        payline.row1 = new bool[] {true,    false,  false,  false,  true};
        payline.row2 = new bool[] {false,   true,   false,  true,   false};
        payline.row3 = new bool[] {false,   false,  true,   false,  false};

        //Act
        List<int> truesFoundPerColumn = new List<int>();
        for (int i = 0; i < payline.row1.Length; i++){
            truesFoundPerColumn.Add(GetTruesFoundPerColum(payline, i));
        }

        //Asert
        truesFoundPerColumn.ForEach(truesInColumn => {
            Assert.AreEqual(maxTruesPerColum,truesInColumn);
        });
    }
    [Test]
    public void PaylineNotPossiblePayline1aFail()
    {
        //Arrange
        payline.row1 = new bool[] {true,    false,  true,   false,  true};
        payline.row2 = new bool[] {false,   true,   false,  true,   false};
        payline.row3 = new bool[] {true,    false,  true,   true,   true};

        //Act
        List<int> truesFoundPerColumn = new List<int>();
        for (int i = 0; i < payline.row1.Length; i++){
            truesFoundPerColumn.Add(GetTruesFoundPerColum(payline, i));
        }

        //Asert
        truesFoundPerColumn.ForEach(truesInColumn => {
            if (truesInColumn > maxTruesPerColum)
                Assert.AreNotEqual(maxTruesPerColum,truesInColumn);
        });
    }
    public void PaylinePossiblePaylineSuccess()
    {
        //Arrange
        payline.row1 = new bool[] {true,    false,  true,   false,  false};
        payline.row2 = new bool[] {false,   true,   false,  true,   false};
        payline.row3 = new bool[] {false,   false,  false,  false,  true};

        //Act
        List<int> truesFoundPerColumn = new List<int>();
        for (int i = 0; i < payline.row1.Length; i++){
            truesFoundPerColumn.Add(GetTruesFoundPerColum(payline, i));
        }

        //Asert
        truesFoundPerColumn.ForEach(truesInColumn => {
            if (truesInColumn > maxTruesPerColum)
                Assert.AreNotEqual(maxTruesPerColum,truesInColumn);
        });
    }
    public static int GetTruesFoundPerColum(Payline payline, int columnIndex){
        int truesFound = 0;
        truesFound += payline.row1[columnIndex] ? 1 : 0; 
        truesFound += payline.row2[columnIndex] ? 1 : 0; 
        truesFound += payline.row3[columnIndex] ? 1 : 0;
        
        return truesFound;
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PaylineTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
