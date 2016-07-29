using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DiningList
{
  public class DiningTest : IDisposable
  {
    public void Dispose()
    {
      Restaurant.DeleteAll();
      Cuisine.DeleteAll();
    }

//TESTS
    public DiningTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=fine_dining_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test1_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test2_Equal_ReturnsTrueIfRestaurantsAreTheSame()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Burger King", "Seattle", 1);
      Restaurant secondRestaurant = new Restaurant("Burger King", "Seattle", 1);

      //Assert
      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test3_Save_SavesToDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Burger King", "Seattle", 0);

      // Act
      testRestaurant.Save();
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test4_AssignedIDTOObjects()
    {
      //Arrange, Act
      Restaurant testRestaurant = new Restaurant("Burger King", "Seattle", 1);
      //Act
      testRestaurant.Save();
      Restaurant saveRestaurant = Restaurant.GetAll()[0];

      int result = saveRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.Equal(testId, result);
    }
    [Fact]
    public void Test5_Find_FindsRestaurantInDatabase()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Burger King", "Seattle", 2);
      testRestaurant.Save();
      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());
      //Assert
      Assert.Equal(testRestaurant, foundRestaurant);
    }


  }
}
