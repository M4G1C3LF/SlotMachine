using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class ReelTest
{    
    

    [SetUp]
    public void SetUp(){
      
    }

    [TearDown]
    public void TearDown(){

    }
    // A Test behaves as an ordinary method
    [Test]
    public void ReelSortByDescendingPositionSuccess()
    {
        //Arrange
        List<Figure> figures = new List<Figure>();
        for (int i = 0; i< 10; i++){
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(Paths.PREFAB_PATH_BELL);
            GameObject figure = GameObject.Instantiate(prefab,new Vector3(0f,Random.Range(-200f,0f),0f), new Quaternion());
            figures.Add(figure.GetComponent<Figure>());
        }
        Reel reel = new Reel(0f,ReelDirection.DOWNWARDS, 0f, figures);
        //Act
        figures = reel.SortFigureByDescendingPosition(figures);

        //Asert
        float lastYPositionFound = float.MaxValue;
        figures.ForEach( figure => {
            if (lastYPositionFound != float.MaxValue){
                Assert.LessOrEqual(figure.transform.localPosition.y,lastYPositionFound);
            }
            lastYPositionFound = figure.transform.localPosition.y;
        });
        
    }
    [Test]
    public void ReelSetFiguresToCorrectPositionSuccess()
    {
        //Arrange
        float separationBetweenFigures = 2f;
        List<float> expectedFigureYPositions = new List<float>();

        List<Figure> figures = new List<Figure>();
        for (int i = 0; i< 10; i++){
            expectedFigureYPositions.Add(-separationBetweenFigures*i);
            
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(Paths.PREFAB_PATH_BELL);
            GameObject figure = GameObject.Instantiate(prefab,new Vector3(0f,Random.Range(-200f,0f),0f), new Quaternion());
            figures.Add(figure.GetComponent<Figure>());
        }
        Reel reel = new Reel(0f,ReelDirection.DOWNWARDS, separationBetweenFigures, figures);

        //Act
        figures = reel.SortFigureByDescendingPosition(figures);
        figures = reel.SetFiguresToCorrectPosition(figures);

        //Asert
        for (int j=0; j<figures.ToArray().Length; j++){
            Assert.AreEqual(figures[j].transform.localPosition.y, expectedFigureYPositions[j]);
        }
        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ReelTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
