using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

public class DataTable
{
    public DataTable()
    {
        Columns = new List<string>();
        Rows = new List<DataRow>();
    }

    public List<string> Columns { get; set; }
    public List<DataRow> Rows { get; set; }

    public DataRow this[int row]
    {
        get
        {
            return Rows[row];
        }
    }

    public void AddRow(params object[] values)
    {
        if (values.Length != Columns.Count)
        {
            throw new IndexOutOfRangeException("The number of values in the row must match the number of column");
        }

        var row = new DataRow();
        for (int i = 0; i < values.Length; i++)
        {
            row[Columns[i]] = values[i];
        }

        Rows.Add(row);
    }

}

public class DataRow : Dictionary<string, object>
{
    public new object this[string column]
    {
        get
        {
            if (ContainsKey(column))
            {

                return base[column];
            }

            return null;
        }
        set
        {
            if (ContainsKey(column))
            {
                base[column] = value;
            }
            else
            {
                Add(column, value);
            }
        }
    }

    public new object this[int columnindex]
    {
        get
        {
            if (this.ContainIndex(columnindex))
            {
                return base[this.BaseIndex(columnindex)];
            }

            return null;
        }
        set
        {
            if (this.ContainIndex(columnindex))
            {
                base[this.BaseIndex(columnindex)] = value;
            }
            else
            {
                Add(this.BaseIndex(columnindex), value);
            }
        }
    }
}

/// <summary>
/// 
/// 
/// </summary>
static public class SyntaxExtend
{

    public static bool ContainIndex(this Dictionary<string, object> Dict, int index)
    {
        bool IsContain = false;
        if (Dict.Count > index)
        {
            IsContain = true;
        }
        return IsContain;
    }
  
	public static string BaseIndex(this Dictionary<string, object> Dict, int index)
    {
        string KeyName = string.Empty;
        if (Dict.Count > index)
        {
            int i = 0;
            foreach (KeyValuePair<string, object> kvp in Dict)
            {
                if (i == index)
                {
                    KeyName = kvp.Key;
                    break;
                }
                i++;
            }
        }
        else
        {

        }
        return KeyName;
    }
	

}