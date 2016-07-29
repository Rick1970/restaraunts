using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DiningList
{
  public class CuisineTest : IDisposable
  {
    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }

    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=fine_dining_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_CuisineEmptyFirst()
    {
      int result = Cuisine.GetAll().Count;
      Assert.Equal(0, result);
    }
    [Fact]
    public void Test2_Equal_ReturnsTrueForSameName()
    {
      //arange,act
      Cuisine firstCuisine = new Cuisine("Sushi", 1);
      Cuisine secondCuisine = new Cuisine("Sushi", 1);
     //Assert
     Assert.Equal(firstCuisine, secondCuisine);
    }
    [Fact]
    public void Test3_Save_SavesCuisineToDatabase()
    {
      //arrange
      Cuisine testCuisine = new Cuisine("Sushi", 1);
      testCuisine.Save();
      //act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};
      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test4_Save_AssignsIdToCuisineObject()
    {
      //arrange
      Cuisine testCuisine = new Cuisine("Sushi", 1);
      testCuisine.Save();
      //act
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.GetId();
      int testId = testCuisine.GetId();
      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]

    public void Test5_Find_FindsCuisineInDatabase()
    {
      //arrange
      Cuisine testCuisine = new Cuisine("Sushi", 1);
      testCuisine.Save();
      //act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());
      //Assert
      Assert.Equal(testCuisine, foundCuisine);
    }
    [Fact]
    public void Test6_EqualOverrideTrueForSameDescription()
    {
      //arrange, act
      Cuisine firstCuisine = new Cuisine("Sushi", 1);
      Cuisine secondCuisine = new Cuisine("Sushi", 1);
      //Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }
    [Fact]
    public void Test7_Test_Save()
    {
      //arrange
      Cuisine testCuisine = new Cuisine("Sushi");
      testCuisine.Save();

      //act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test8_GetRestaurants_RetrievesAllRestaurantsWithCategory()
    {
      Cuisine testCuisine = new Cuisine("Sushi");
      testCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("Burger King", "Seattle", testCuisine.GetId());
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Trappers", "Bonney Lake", testCuisine.GetId());
      secondRestaurant.Save();


      List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
      List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

      Assert.Equal(testRestaurantList, resultRestaurantList);
    }
}
}
