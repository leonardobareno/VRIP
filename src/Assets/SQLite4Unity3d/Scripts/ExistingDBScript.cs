using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

using SQLite4Unity3d;

public class ExistingDBScript : MonoBehaviour {

	//public Text DebugText;

    private DataService ds;

    // Use this for initialization
    void Start () {
        ds = new DataService ("wga_hu_bd.sqlite");

		//var people = ds.GetAll ();
		//ToConsole (people);

		/*var people = ds.GetResultSetTest();
		ToConsole("Searching for GetResultSetTest ...");
		ToConsole (people);

		var p = ds.GetRecordTest();
        ToConsole("Registro individual GetRecordTest ...");
        ToConsole(p.ToString());*/

	}

    /*private void ToConsole(IEnumerable<wga_hu> people){
		foreach (var person in people) {
			ToConsole(person.ToString());
		}
	}

	private void ToConsole(string msg){
		DebugText.text += System.Environment.NewLine + msg;
		Debug.Log (msg);
	}*/

    public TableQuery<wga_hu> query(List<criterio> criterios_set) {
        return ds.query(criterios_set);
    }

    public wga_hu findByFilename(string sFil) {
        return ds.findByFilename(sFil);
    }

    /*public IEnumerable<wga_hu> GetResultSetTest() {
        return ds.GetResultSetTest();
    }*/

}
