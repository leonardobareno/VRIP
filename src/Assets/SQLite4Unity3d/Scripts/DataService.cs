using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;
using System.Collections;

using System;
using System.Linq.Expressions;

using Extensions;
using System.Text;
using LB_sw;

public class DataService  {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        if (bDebug) Debug.Log("Final PATH: " + dbPath);     

	}



	public IEnumerable<wga_hu> GetAll(){
		return _connection.Table<wga_hu>();
	}

    /*public IEnumerable<wga_hu> GetResultSetTest(){
        //return _connection.Table<wga_hu>().Where(x => x.school == "Spanish");
        return _connection.Table<wga_hu>().Where(x => x.author == "GOGH, Vincent van");
    }

	public wga_hu GetRecordTest(){
		return _connection.Table<wga_hu>().Where(x => x.author == "AACHEN, Hans von").FirstOrDefault();
	}*/

    private bool bDebug = false;

    public TableQuery<wga_hu> query(List<criterio> criterios_set) {

        if (bDebug) Debug.Log("****** public IEnumerable<wga_hu> query() criterios_set len=" + criterios_set.Count);

        Expression<Func<wga_hu, bool>> predExpr = null;

        //predExpr = x => x.school == "Spanish";
        //predExpr = ExpressionExtension<wga_hu>.AppendExpression(predExpr, x => x.school == "French", "OR");


        foreach (criterio crit_and_inst in criterios_set)
        {
            if (bDebug) Debug.Log("****** crit_and_inst.value len=" + crit_and_inst.value.Length);

            Expression<Func<wga_hu, bool>> predExpr_or = null;

            switch (crit_and_inst.label)
            {
                case constantes.WGAHU_FIELD_FORM:
                    foreach(string crit_value_inst in crit_and_inst.value) {
                        predExpr_or = ExpressionExtension<wga_hu>.AppendExpression(predExpr_or, x => x.form == crit_value_inst, "OR");
                    }
                    break;
                case constantes.WGAHU_FIELD_TYPE:
                    foreach(string crit_value_inst in crit_and_inst.value) {
                        predExpr_or = ExpressionExtension<wga_hu>.AppendExpression(predExpr_or, x => x.type == crit_value_inst, "OR");
                    }
                    break;
                case constantes.WGAHU_FIELD_SCHOOL:
                    foreach(string crit_value_inst in crit_and_inst.value) {
                        predExpr_or = ExpressionExtension<wga_hu>.AppendExpression(predExpr_or, x => x.school == crit_value_inst, "OR");
                    }
                    break;
                case constantes.WGAHU_FIELD_TIMEFRAME:
                    foreach(string crit_value_inst in crit_and_inst.value) {
                        predExpr_or = ExpressionExtension<wga_hu>.AppendExpression(predExpr_or, x => x.timeframe == crit_value_inst, "OR");
                    }
                    break;
                case constantes.WGAHU_FIELD_AUTHOR:
                    int j;
                    foreach (string crit_value_inst in crit_and_inst.value) {

                        if (bDebug) {
                            Debug.Log("****** FILA " + crit_and_inst.label + "=" + crit_value_inst + "|");

                            byte[] bytes_suc = Encoding.ASCII.GetBytes("CANALETTO");
                            byte[] bytes_mal = Encoding.ASCII.GetBytes(crit_value_inst);

                            for (j = 0; j < bytes_suc.Length; j++) Debug.Log("suc char[" + j + "]=" + (int)bytes_suc[j] + "|");
                            for (j = 0; j < bytes_mal.Length; j++) Debug.Log("mal char[" + j + "]=" + (int)bytes_mal[j] + "|");


                            Debug.Log("****** bytes_suc=" + bytes_suc + "|len=" + bytes_suc.Length);
                            Debug.Log("****** bytes_mal=" + bytes_mal + "|len=" + bytes_mal.Length);
                        }

                        predExpr_or = ExpressionExtension<wga_hu>.AppendExpression(predExpr_or, x => x.author == crit_value_inst, "OR");
                    }
                    break;
                case constantes.WGAHU_FIELD_DATE_NUM:
                    //MANEJO ESPECIAL DE PARAMS FROM Y UNTIL
                    //si "valores[0]", aparece error: NotSupportedException: Cannot get SQL for: ArrayIndex
                    int valores_from;
                    int valores_until;
                    valores_from  = Int32.Parse(crit_and_inst.value[0]);
                    valores_until = Int32.Parse(crit_and_inst.value[1]);

                    if (valores_from != -1)
                        predExpr_or = ExpressionExtension<wga_hu>.AppendExpression(predExpr_or, x => x.date_num >= valores_from, "AND");
                    if (valores_until != -1)
                        predExpr_or = ExpressionExtension<wga_hu>.AppendExpression(predExpr_or, x => x.date_num <= valores_until, "AND");

                    break;
            }

            if (true) Debug.Log("****** predExpr_or=" + predExpr_or.ToString() + "|");

            if (predExpr_or != null) {
                predExpr = ExpressionExtension<wga_hu>.AppendExpression(predExpr, predExpr_or, "AND");
            }
        }

        if (true) Debug.Log("****** predExpr=" + predExpr.ToString() + "|");

        return _connection.Table<wga_hu>().Where(predExpr);
    }

    public wga_hu findByFilename(string sFil) {
        return _connection.Table<wga_hu>().Where(x => x.filename == sFil).FirstOrDefault();
    }
}
