using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ListScript : MonoBehaviour {

	public List<string> stringList1;
	public List<string> stringList2;
    public List<string> stringList3;

	public string[] test1;
	public string[] test2;
	public string[] test3;
	public string nabber;
	public int poopIndex;

	void Awake(){
		stringList1 = new List<string>{"fun","poop"};
		test1 = stringList1.ToArray ();
		test2 = new string[stringList1.Count];
		stringList1.CopyTo (test2);
		nabber = stringList1.ElementAt (0);
		poopIndex = stringList1.IndexOf ("poop");
		stringList2 = new List<string>(stringList1);
		stringList1.Remove ("fun");

		test3 = stringList2.ToArray ();
	}
}
