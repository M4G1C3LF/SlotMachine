using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CombinationTest
{
    
    [SetUp]
    public void SetUp(){
    }

    [TearDown]
    public void TearDown(){

    }
    // A Test behaves as an ordinary method
    [Test]
    public void CombinationSimpleCreationSuccess()
    {
        //Arrange
        FigureType figureTest = FigureType.BELL;
        List<Figure> figures = new List<Figure>();
        figures.Add(new Figure(figureTest));
        figures.Add(new Figure(figureTest));
        
        //Act
        Combination combination = new Combination(figures);

        //Asert
        combination.GetFigures().ForEach( figure => {
            Assert.AreEqual(figureTest,figure.GetFigureType());
        });
    }
    [Test]
    public void CombinationAllFiguresHaveTheSameTypeSuccess()
    {
        //Arrange
        FigureType figureTest = FigureType.BELL;
        List<Figure> figures = new List<Figure>();
        figures.Add(new Figure(figureTest));
        figures.Add(new Figure(figureTest));
        figures.Add(new Figure(figureTest));
        figures.Add(new Figure(figureTest));
        
        //Act
        Combination combination = new Combination(figures);

        //Asert
        Figure lastFigureFoundOnCombination = null;
        combination.GetFigures().ForEach( figure => {
            if (lastFigureFoundOnCombination == null){
                lastFigureFoundOnCombination = figure;
            } else {
                Assert.AreEqual(figureTest,lastFigureFoundOnCombination.GetFigureType());
                lastFigureFoundOnCombination = figure;
            }
        });
    }
    [Test]
    public void CombinationIncrementOccurrencesOnAddFiguresSuccess()
    {
        //Arrange
        FigureType figureTest = FigureType.BELL;
        List<Figure> figures = new List<Figure>();
        figures.Add(new Figure(figureTest));
        figures.Add(new Figure(figureTest));
        Combination combination = new Combination(figures);
        int combinationOccurences = combination.GetOccurrences();
        
        //Act
        combination.AddFigure(new Figure(figureTest));

        //Asert
        Assert.AreEqual(combinationOccurences+1,combination.GetOccurrences());
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CombinationTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
