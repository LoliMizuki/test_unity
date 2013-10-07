
using UnityEngine;
using System.IO;
using System.Collections;

public class DBTest : MonoBehaviour {
    public GUISkin MyGUISkin = null;

#region - Private
    delegate DataTable ExecuteQuery(string query);

    string _databaseName = "mimiTestDb.db";
    string _tableName = "test_man";
    string _dataBaseContent = "";
    string _console = "";
    SqliteDatabase _database = null;

    string _documentsPath { get { return Application.persistentDataPath + "/" + _databaseName; } }

#endregion

    void Awake() {
        CheckAndCopyDatabaseToDocuments();
        InitAndOpenDatabase();
        RefreshDatabaseContent();
    }

    void CheckAndCopyDatabaseToDocuments() {
        string streamingAssetsPath = Application.streamingAssetsPath + "/" + _databaseName;

        if( File.Exists( _documentsPath ) == false ) {
            File.Copy( streamingAssetsPath, _documentsPath );
        }
    }

    void InitAndOpenDatabase() {
        try {
            _database = new SqliteDatabase();
            _database.Open( _documentsPath );

        } catch( SqliteException e ) {
            WriteConsole( e.Message );
        }
    }

    void OnDestory() {
        if( _database != null ) {
            _database.Close();
        }
    }

    void RefreshDatabaseContent() {
        _dataBaseContent = DatabaseContentString();
    }

    string DatabaseContentString() {
        DataTable tb = ExecuteQueryAndCatch( "select * from " + _tableName, (query) => {
            return _database.ExecuteQuery( query ); } );

        if( tb == null ) {
            WriteConsole( "Empty???" );
            return "<null>";
        }

        string contentString = "";
        foreach( DataRow row in tb.Rows ) {
            contentString += row[ 0 ] + ": " + row[ 1 ] + "\n";
        }

        return contentString;
    }

    DataTable ExecuteQueryAndCatch(string query, ExecuteQuery executeQuery) {
        if( executeQuery == null ) {
            return null;
        }

        try {
            return executeQuery( query );

        } catch( SqliteException e ) {
            WriteConsole( e.Message );

        }

        return null;
    }

    void WriteConsole(string message) {
        _console += message + "\n";
    }

    void OnGUI() {
        if( _console == null ) {
            return;
        }

        GUI.skin = MyGUISkin;
        GUI.Label( new Rect( 0, 0, 400, 400 ), _dataBaseContent );
        GUI.Label( new Rect( 320, 0, 400, 400 ), _console );

        Vector2 buttonSize = new Vector2( 100, 40 );

        if( GUI.Button( new Rect( Screen.width/2, Screen.height/2 + 100, buttonSize.x, buttonSize.y ), "Add" ) ) {
            ExecuteQueryAndCatch( "insert into " + _tableName + "( id, name ) values ( 99, 'Nekoko' )", (query) => {
                _database.ExecuteNonQuery( query );
                return null;
            } );
            RefreshDatabaseContent();
        }

        if( GUI.Button( new Rect( Screen.width/2, Screen.height/2 + 150, buttonSize.x, buttonSize.y ), "Delete" ) ) {
            ExecuteQueryAndCatch( "delete from " + _tableName + " where id = 99", (query) => {
                _database.ExecuteNonQuery( query );
                return null;
            } );
            RefreshDatabaseContent();
        }

        if( GUI.Button( new Rect( Screen.width/2, Screen.height/2 + 200, buttonSize.x, buttonSize.y ), "Refresh" ) ) {
            RefreshDatabaseContent();
        }
    }
}
