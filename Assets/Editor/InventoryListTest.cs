using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InventoryListTest {

private InventoryList invList;

	[SetUp]
	public void SetUp(){
		invList = GameObject.FindObjectOfType<InventoryList>();
	}

    [Test]
	public void T00_PassingTest () {
		Assert.AreEqual (1, 1);
	}

    [Test]
	public void T01_Add_An_Item () {
		//arrange
		string item = "Baseball Bat";
		int index = 0;
		//act
		invList.AddItem(item);
		//assert
		Assert.AreEqual ("Baseball Bat", invList.ReturnItemAtIndex(index));
	}

	[Test]
	public void T02_Add_Two_Items(){
		string item1 = "Yo-Yo";
		string item2 = "Frying Pan";

		invList.AddItem(item1);
		invList.AddItem(item2);

		Assert.AreEqual ("Yo-Yo", invList.ReturnItemAtIndex(1));
		Assert.AreEqual ("Frying Pan", invList.ReturnItemAtIndex(2));
	}

	[Test]
	public void T03_Remove_An_Item(){
		invList.DeleteItem("Yo-Yo");

		Assert.AreEqual ("Baseball Bat", invList.ReturnItemAtIndex(0));
		Assert.AreEqual ("Frying Pan", invList.ReturnItemAtIndex(1));
	}

	[Test]
	public void T04_Return_Number_Of_Items_In_List(){
		string breadRoll = "Bread Roll";
		string ketchup = "Ketchup";
		string teddyBear = "Teddy Bear";
		string backstagePass = "Backstage Pass";
		string atmCard = "ATM Card";

		invList.AddItem(breadRoll);
		invList.AddItem(ketchup);
		invList.AddItem(teddyBear);
		invList.AddItem(backstagePass);
		invList.AddItem(atmCard);

		Assert.AreEqual (7, invList.ReturnLengthOfList());
	}
}
