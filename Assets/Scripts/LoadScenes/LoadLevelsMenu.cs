using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;  
using MySql.Data;
using MySql.Data.MySqlClient; 

public class LoadLevelsMenu : MonoBehaviour {

    public InputField userName;
    public InputField userPassword;
    string username;
    string password;

    private void Start()
    {
        username = null;
        password = null;
    }
	public void  get_password_from_database() {
		string constr2 = "server=160.39.193.170;Database=mydb;User Id=root;password=onionst";
		MySqlConnection mycon2 = new MySqlConnection(constr2);
		mycon2.Open();
		string query2 = "select password from Player where Username ="+ "'"+ userName.text +"';";
		MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query2, mycon2);
		DataSet ds = new DataSet();
		dataAdapter.Fill(ds);
		if ((ds.Tables [0].ToString ()).Length == 0) {
			Debug.Log ("No user exists!");
			LoadLevelMenu ();
		
		}


		else {
			string db_password = (ds.Tables[0].Rows[0][0]).ToString();
			if (db_password == userPassword.text)
				Debug.Log ("Log in success!");
			else
				Debug.Log ("Password error!");
		}
	}
    // Update is called once per frame
    public void LoadLevelMenu()
    {

        if (userName.text != null)// && userPassword.text != null)
        {
            SceneManager.LoadScene(1);
        }
    }
    /*
        public void Quit()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
        */
}
